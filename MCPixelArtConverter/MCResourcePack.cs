using System;   
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCResourcePack
    {
        public const string AssetFolderPath = "assets/minecraft/";
        string baseFolderPath;
        Dictionary<string, MCBlockState> blockStates = new Dictionary<string, MCBlockState>();
        MCBlockModelCollection blockModels;

        public Sides SelectedSide { get; set; }

        //Cache for palettes for different sides
        Dictionary<Sides, Dictionary<MCBlockVariant, Bitmap>> cachedPalettes = new Dictionary<Sides, Dictionary<MCBlockVariant, Bitmap>>();

        public MCResourcePack(string baseFolder)
        {
            SelectedSide = Sides.Up; //default side
            baseFolderPath = baseFolder;

            //TODO: P2 save to config file and load instead of static default
            using (ZipArchive jar = ZipFile.Open(baseFolderPath + "\\1.12.jar", ZipArchiveMode.Read))
            {
                Console.WriteLine("Opened jar file " + jar);
                //TODO: P1 use minecraft jar + resource pack folders instead of unzipped folders
                if (blockModels == null)
                    blockModels = new MCBlockModelCollection();
                blockModels.jar = jar;

                try
                {
                    foreach (ZipArchiveEntry entry in jar.Entries)
                    {
                        Console.WriteLine("Jar entry " + entry.FullName);
                        //TODO: P2: more efficient way to get files from subfolder in ziparchive??
                        if (!entry.FullName.StartsWith(AssetFolderPath + "blockstates"))
                            continue;

                        MCBlockState blockstate = new MCBlockState(entry, blockModels);
                        blockStates.Add(blockstate.FileName, blockstate);
                    }
                }
                finally
                {
                    //keep cached blockmodels (with textures), but remove the reference to the jar file we are about to close
                    blockModels.jar = null;
                }
            }            
        }

        public MCBlockState getBlockState(string blockStateName)
        {
            return blockStates[blockStateName];
        }

        public List<string> getBlockNames()
        {
            return blockStates.Keys.ToList();
        }

        public List<MCBlockState> getBlockStates()
        {
            return blockStates.Values.ToList();
        }

        private Dictionary<MCBlockVariant, Bitmap> ConstructPalette()
        {
            Dictionary<MCBlockVariant, Bitmap> palette = new Dictionary<MCBlockVariant, Bitmap>();
            foreach (MCBlockState blockState in blockStates.Values)
            {
                foreach (var kv in blockState.GetSideImages(SelectedSide))
                {
                    if (kv.Value != null)
                        palette.Add(kv.Key, kv.Value);
                }
            }
            return palette;
        }

        public void UnselectAll()
        {
            foreach (var kv in blockStates)
                kv.Value.SetSelected(false);
        }

        public Dictionary<MCBlockVariant, Bitmap> GetPalette()
        {
            Dictionary<MCBlockVariant, Bitmap> palette;
            if (!cachedPalettes.TryGetValue(SelectedSide, out palette))
            {
                palette = ConstructPalette();
                cachedPalettes.Add(SelectedSide, palette);
            }
            return palette;
        }

        public void SaveBlockSelection(string name)
        {
            StreamWriter selectionFile;
            using (selectionFile = new StreamWriter(name))
            {
                foreach (var blockState in blockStates)
                {
                    blockState.Value.SaveBlockSelection(selectionFile);
                }
            }
        }

        public void LoadBlockSelection(string name)
        {
            StreamReader selectionFile;
            Dictionary<string, List<string>> selectedVariantsForBlockState = new Dictionary<string, List<string>>();
            //TODO: P2 handle FileNotFoundException?
            using (selectionFile = new StreamReader(name))
            {
                string selectionLine;
                while ((selectionLine = selectionFile.ReadLine()) != null)
                {
                    string[] selectionLineSplit = selectionLine.Split('\t');
                    List<string> selectedVariants;
                    if (!selectedVariantsForBlockState.TryGetValue(selectionLineSplit[0], out selectedVariants))
                    {
                        selectedVariantsForBlockState[selectionLineSplit[0]] = new List<string>();
                        selectedVariants = selectedVariantsForBlockState[selectionLineSplit[0]];
                    }
                    //This does not take into account duplicates, but that is not needed since this can only enable a variant
                    selectedVariants.Add(selectionLineSplit[1]);
                }
            }

            UnselectAll();
            foreach (var kv in selectedVariantsForBlockState)
            {
                MCBlockState selectedBlockState;
                if (blockStates.TryGetValue(kv.Key, out selectedBlockState))
                {
                    selectedBlockState.LoadBlockSelection(kv.Value);
                }
            }
        }
    }
}
