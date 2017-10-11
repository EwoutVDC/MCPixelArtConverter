using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Windows.Media.Media3D; TODO: this seems like a great library for 3d calculations. how to use??
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

        JToken json; //raw json used to construct the variant. only here for debugging purpose

        //If multiple models, equal chance of use in minecraft (ie bedrock)
        //We only use the first model at the moment
        //TODO: P4 use all possible models. This would mean replacing blocks until you get the right model for a very minor gain usually?
        MCBlockModel model;
        int x = 0, y = 0, z = 0;
        bool uvlock = false;

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

        {
            "variants": {
                "axis=y":  { "model": "acacia_log" },
                "axis=z":   { "model": "acacia_log", "x": 90 },
                "axis=x":   { "model": "acacia_log", "x": 90, "y": 90 },
                "axis=none": { "model": "acacia_bark" }
            }
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
            Selected = true;

            if (value.Type == JTokenType.Array)
                value = ((JArray)value)[0];

            IDictionary<string, JToken> variantsDict = (JObject)value;
            Dictionary<string, JToken> jsonProperties = variantsDict.ToDictionary(pair => pair.Key, pair => pair.Value);

            model = blockModels.FromFile(jsonProperties["model"].ToString());
            if (jsonProperties.ContainsKey("x"))
                x = (int)jsonProperties["x"];
            if (jsonProperties.ContainsKey("y"))
                y = (int)jsonProperties["y"];
            if (jsonProperties.ContainsKey("z"))
                z = (int)jsonProperties["z"];

            if (jsonProperties.ContainsKey("uvlock"))
                uvlock = (bool)jsonProperties["uvlock"];
        }

        public override string ToString()
        {
            return BlockState.FileName + "_" + Name;
        }        

        private static Sides RotateSide(Sides side, int x, int y, int z, out RotateFlipType rotation)
        {
            MCPoint sidePoint = MCPoint.FromSide(side);
            //rotate the block
            sidePoint.Rotate(new RotationMatrix(x, y, z));

            //rotate along the viewing axis
            int rotate;
            if (sidePoint.X != 0)
                rotate = x;
            else if (sidePoint.Y != 0)
                rotate = y;
            else
                rotate = z;
            rotation = RotationMatrix.RotateFlipTypeFromDegrees(rotate);

            return sidePoint.ToSide();
        }


        public Bitmap GetSideImage(Sides side)
        {
            RotateFlipType rotation;
            side = RotateSide(side, x, y, z, out rotation);
            Bitmap image = model.GetSideImage(side);
            image.RotateFlip(rotation);
            return image;
        }
    }
}
