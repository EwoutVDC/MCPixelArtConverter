using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCBlockVariant
    {
        MCBlockState blockState;
        MCBlockModel model;
        String name;
        
        JToken json;
        Dictionary<String, JToken> jsonProperties;

        /*
        Simple example:
        "normal": { "model": "black_wool" }
        */
        public MCBlockVariant(MCBlockState blockState, String variantName, JToken value)
        {
            this.blockState = blockState;
            this.name = variantName;
            json = value;

            IDictionary<string, JToken> variantsDict = (JObject)value;
            jsonProperties = variantsDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            LoadModelFile(jsonProperties["model"].ToString());
        }

        public override String ToString()
        {
            return json.ToString();
        }

        private void LoadModelFile(String fileName)
        {
            Console.WriteLine("Loading model file " + fileName);
            model = new MCBlockModel(fileName);
        }

        public Bitmap getTopView()
        {
            return model.getTopView();
        }
    }
}
