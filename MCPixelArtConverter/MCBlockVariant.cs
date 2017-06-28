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
        public MCBlockState blockState { get; }
        public string Name { get; }
        //Only the first model of a variant is used
        List<MCBlockModel> models = new List<MCBlockModel>(); //see bedrock blockstate file
        
        JToken json;
        Dictionary<string, JToken> jsonProperties; //uvlock, y  etc... not good. These should be properties

        /*
        Simple example:
        "normal": { "model": "black_wool" }

        "variants": {
            "normal": [
                //array of models: equal chance of any model
                { "model": "bedrock" },
                { "model": "bedrock_mirrored" },
                { "model": "bedrock", "y": 180 },
                { "model": "bedrock_mirrored", "y": 180 }
            ]
        }

        "variants": {
            "facing=south": { "model": "black_glazed_terracotta" },
            "facing=west": { "model": "black_glazed_terracotta", "y": 90 },
            "facing=north": { "model": "black_glazed_terracotta", "y": 180 },
            "facing=east": { "model": "black_glazed_terracotta", "y": 270 }
        }
        */
        public MCBlockVariant(MCBlockState blockState, string variantName, JToken value, MCBlockModelCollection blockModels)
        {
            this.blockState = blockState;
            Name = variantName;
            json = value;

            switch (value.Type)
            {
                case JTokenType.Object:
                    models.Add(CreateModelFromJson(blockModels, value));
                    break;
                case JTokenType.Array:
                    foreach (JObject modelObject in (JArray) value)
                    {
                        models.Add(CreateModelFromJson(blockModels, modelObject));
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid Jtoken type " + value.Type);
            }
        }

        private MCBlockModel CreateModelFromJson(MCBlockModelCollection blockModels, JToken value)
        {
            IDictionary<string, JToken> variantsDict = (JObject)value;
            jsonProperties = variantsDict.ToDictionary(pair => pair.Key, pair => pair.Value);
            string fileName = jsonProperties["model"].ToString();
            return blockModels.FromFile(fileName);
        }

        public override string ToString()
        {
            return blockState.FileName + " " + Name;
        }

        public Bitmap GetSideImage(Sides side)
        {
            return models[0].GetSideImage(side);
        }

        
    }
}
