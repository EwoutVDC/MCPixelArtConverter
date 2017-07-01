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
        public string BaseFolder { get; }
        public string FileName { get; }
        Dictionary<string, MCBlockVariant> Variants = new Dictionary<string, MCBlockVariant>();
        //multipart blockstates todo

        public MCBlockState(string baseFolder, string fileName, MCBlockModelCollection blockModels)
        {
            BaseFolder = baseFolder;
            FileName = Path.GetFileNameWithoutExtension(fileName);
            JObject blockStateJson = JObject.Parse(File.ReadAllText(fileName));
            IDictionary<string, JToken> variantsDict = (JObject)blockStateJson["variants"];
            if (variantsDict != null)
                Variants = variantsDict.ToDictionary(pair => pair.Key, pair => new MCBlockVariant(this, pair.Key, pair.Value, blockModels));
        }

        public Dictionary<MCBlockVariant, Bitmap> GetSideImages(Sides facing)
        {
            Dictionary<MCBlockVariant, Bitmap> textures = new Dictionary<MCBlockVariant, Bitmap>();

            foreach (MCBlockVariant variant in Variants.Values)
            {
                textures.Add(variant, variant.GetSideImage(facing));
            }

            return textures;
        }

        public override string ToString()
        {
            return FileName;
        }
    }
}
