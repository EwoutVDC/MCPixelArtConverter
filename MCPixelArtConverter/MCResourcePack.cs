using System;
using System.Collections.Generic;
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

        public MCResourcePack(String baseFolder)
        {
            baseFolderPath = baseFolder;

            foreach (String filename in Directory.GetFiles(baseFolderPath + "blockstates\\", "*.json"))
            {
                MCBlockState blockstate = new MCBlockState(baseFolder, filename);
                blockStates.Add(blockstate.FileName, blockstate);
            }
        }

        public void addState (MCBlockState state)
        {
            blockStates.Add(state.FileName, state);
        }

        public MCBlockState getBlockState(String blockStateName)
        {
            return blockStates[blockStateName];
        }

        public List<String> getBlockNames()
        {
            return blockStates.Keys.ToList();
        }
    }
}
