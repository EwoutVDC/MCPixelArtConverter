using System;   
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCResourcePack
    {
        string baseFolderPath;
        Dictionary<string, MCBlockState> blockStates = new Dictionary<string, MCBlockState>();
        MCBlockModelCollection blockModels;

        public Sides SelectedSide { get; set; }

        //Cache for palettes for different sides
        Dictionary<Sides, Dictionary<MCBlockVariant, Bitmap>> cachedPalettes = new Dictionary<Sides, Dictionary<MCBlockVariant, Bitmap>>();

        public MCResourcePack(string baseFolder)
        {
            SelectedSide = Sides.Up; //default side TODO: save to config file and load instead of static default

            baseFolderPath = baseFolder;
            if (!Directory.Exists(baseFolderPath + "blockstates\\"))
            {
                throw new ArgumentException("Invalid baseFolder " + baseFolder);
            }

            blockModels = new MCBlockModelCollection(baseFolder);

            foreach (string filename in Directory.GetFiles(baseFolderPath + "blockstates\\", "*.json"))
            {
                MCBlockState blockstate = new MCBlockState(baseFolder, filename, blockModels);
                blockStates.Add(blockstate.FileName, blockstate);
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
