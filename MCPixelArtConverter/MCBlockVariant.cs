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
        public MCBlockState BlockState { get; }
        public string Name { get; }
        public bool Selected { get; set; }
        //Only the first model of a variant is used
        List<MCBlockModel> models = new List<MCBlockModel>(); //see bedrock blockstate file
        //TODO: y, uvlock
        
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
            BlockState = blockState;
            Name = variantName;
            json = value;
            Selected = true; //TODO: save selected variants to config file and load here or restore in resourcepack

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
            return BlockState.FileName + "_" + Name;
        }

        public Bitmap GetSideImage(Sides side)
        {
            //TODO: Block variant rotation does not seem to be taken into account
            return models[0].GetSideImage(side);
        }

        
    }
}
