using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCElementFace
    {
        public string TextureReference { get; }
        //uv coords represent area of the texture that is used for this element face
        //(0,0) is topleft of texture
        public Double UMin { get; }
        public Double UMax { get; }
        public Double VMin { get; }
        public Double VMax { get; }
        public Int16 Rotation { get; }
        //cullface not used for this application

        public MCElementFace(JObject json)
        {
            TextureReference = json["texture"].ToString();
            JArray uv = (JArray)json["uv"];
            if (uv != null)
            {
                UMin = Double.Parse(uv[0].ToString());
                VMin = Double.Parse(uv[1].ToString());
                UMax = Double.Parse(uv[2].ToString());
                VMax = Double.Parse(uv[3].ToString());
            }
            else
            {
                UMin = VMin = 0;
                UMax = VMax = 16;
            }

            JToken rotationJson;
            if (json.TryGetValue("rotation", out rotationJson))
            {
                Rotation = Int16.Parse(rotationJson.ToString());
            }
        }
    }
}

