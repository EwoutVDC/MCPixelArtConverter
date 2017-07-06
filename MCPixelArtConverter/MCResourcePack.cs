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

        public Sides SelectedSide  {get; set;}

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
    }
}
