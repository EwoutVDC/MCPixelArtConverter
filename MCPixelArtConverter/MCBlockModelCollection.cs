using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCBlockModelCollection
    {
        string baseFolderPath;
        Dictionary<string, MCBlockModel> blockModels = new Dictionary<string, MCBlockModel>();

        public MCBlockModelCollection(string baseFolderPath)
        {
            this.baseFolderPath = baseFolderPath;
        }

        public MCBlockModel FromFile(string fileName)
        {
            MCBlockModel model;
            if (blockModels.TryGetValue(fileName, out model))
            {
                return model;
            }
            else
            {
                model = new MCBlockModel(baseFolderPath, fileName, this);
                blockModels.Add(fileName, model);
                return model;
            }
            
        }
    }
}
