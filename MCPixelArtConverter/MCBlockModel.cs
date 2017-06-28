using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MCPixelArtConverter
{
    class MCBlockModel
    {
        const String ModelFolderPath = "models\\block\\";
        const String TextureFolderPath = "textures\\";
        //TODO blockstate backpointer needed???

        MCBlockModel parent;

        Dictionary<String, Bitmap> textures;
        //possible textures keys: all, top, bottom, north, south, east, west, side

        List<MCBlockElement> elements;


        JObject json; //TODO: this should not be needed in the end

        public MCBlockModel(String baseFolderName, String modelFileName)
        {
            json = JObject.Parse(File.ReadAllText(baseFolderName + ModelFolderPath + modelFileName + ".json"));

            //Load parent model TODO

            //Load texture bitmaps - TODO lazy loading of texture bitmaps
            IDictionary<string, JToken> texturesDict = (JObject)json["textures"];
            textures = texturesDict.ToDictionary(
                pair => pair.Key,
                pair => new Bitmap(baseFolderName + TextureFolderPath + pair.Value.ToString().Replace("/", "\\") + ".png"));

            //Load elements TODO
        }

        public Bitmap getTexture(Sides facing)
        {
            Bitmap bm;

            //TODO: implement multiple face textures, using elements etc
            if (!textures.TryGetValue("all", out bm))
            {
                //throw new ApplicationException("Could not compute texture");
                return null;
            }

            return bm;
        }

        public override String ToString()
        {
            return json.ToString();
        }
    }
}
