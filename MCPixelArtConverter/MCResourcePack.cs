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
        String baseFolderPath;
        Dictionary<String, MCBlockState> blockStates = new Dictionary<string, MCBlockState>();
        MCBlockModelCollection blockModels;

        public MCResourcePack(String baseFolder)
        {
            baseFolderPath = baseFolder;
            if (!Directory.Exists(baseFolderPath + "blockstates\\"))
            {
                throw new ArgumentException("Invalid baseFolder " + baseFolder);
            }

            blockModels = new MCBlockModelCollection(baseFolder);

            foreach (String filename in Directory.GetFiles(baseFolderPath + "blockstates\\", "*.json"))
            {
                MCBlockState blockstate = new MCBlockState(baseFolder, filename, blockModels);
                blockStates.Add(blockstate.FileName, blockstate);
            }
        }

        public MCBlockState getBlockState(String blockStateName)
        {
            return blockStates[blockStateName];
        }

        public List<String> getBlockNames()
        {
            return blockStates.Keys.ToList();
        }

        public Dictionary<MCBlockVariant, Bitmap> GetPalette(Sides facing)
        {
            Dictionary<MCBlockVariant, Bitmap> textures = new Dictionary<MCBlockVariant, Bitmap>();
            foreach (MCBlockState blockState in blockStates.Values)
            {
                foreach (var kv in blockState.GetSideImages(facing))
                {
                    textures.Add(kv.Key, kv.Value);
                }
            }
            return textures;
        }
    }
}
