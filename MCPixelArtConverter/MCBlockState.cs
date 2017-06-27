using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Drawing;

namespace MCPixelArtConverter
{
    class MCBlockState
    {
        public String BaseFolder { get; }
        public String FileName { get; }
        Dictionary<String, MCBlockVariant> Variants = new Dictionary<string, MCBlockVariant>();
        //multipart blockstates todo

        public MCBlockState(String baseFolder, String fileName)
        {
            BaseFolder = baseFolder;
            FileName = Path.GetFileNameWithoutExtension(fileName);
            JObject blockStateJson = JObject.Parse(File.ReadAllText(fileName));
            IDictionary<string, JToken> variantsDict = (JObject)blockStateJson["variants"];
            if (variantsDict != null)
                Variants = variantsDict.ToDictionary(pair => pair.Key, pair => new MCBlockVariant(this, pair.Key, pair.Value));
        }

        public Bitmap GetTopView()
        {
            MCBlockVariant variant;
            if (Variants.TryGetValue("normal", out variant))
            {
                return variant.getTopView();
            }
            else
                return null;
        }

        
    }
}
