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
        String FileName;
        Dictionary<String, MCBlockVariant> Variants;
        //multipart blockstates todo

        public MCBlockState(String fileName)
        {
            this.FileName = fileName;
            JObject blockStateJson = JObject.Parse(File.ReadAllText(FileName));
            IDictionary<string, JToken> variantsDict = (JObject)blockStateJson["variants"];
            Variants = variantsDict.ToDictionary(pair => pair.Key, pair => new MCBlockVariant(this, pair.Key, pair.Value));
            
            foreach(KeyValuePair<String, MCBlockVariant> kv in Variants)
            {
                Console.WriteLine("key: " + kv.Key);
                Console.WriteLine("value: " + kv.Value);
            }

        }

        public Bitmap GetTopView()
        {
            return Variants["normal"].getTopView();
        }
    }
}
