using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCBlockModelCollection
    {
        public ZipArchive jar { set; private get; }
        Dictionary<string, MCBlockModel> blockModels = new Dictionary<string, MCBlockModel>();

        public MCBlockModelCollection()
        {
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
                model = new MCBlockModel(jar, fileName, this);
                blockModels.Add(fileName, model);
                return model;
            }
            
        }
    }
}
