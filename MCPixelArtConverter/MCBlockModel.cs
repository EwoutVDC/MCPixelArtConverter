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
        const string ModelFolderPath = "models\\block\\";
        const string TextureFolderPath = "textures\\";
        //TODO blockstate backpointer needed???

        MCBlockModel parent = null;

        Dictionary<string, Bitmap> textures = new Dictionary<string, Bitmap>();
        Dictionary<string, string> textureReferences = new Dictionary<string, string>();
        //common textures keys: all, top, bottom, north, south, east, west, side
        //other examples: wool (carpet_color.json)

        List<MCBlockElement> elements = new List<MCBlockElement>();


        JObject json; //TODO: this should not be needed in the end
        
        //TODO: Is there a way to protect the constructor from being used from anywhere else than MCBlockModelCollection?
        //By extending the class and making the constructor protected????
        public MCBlockModel(string baseFolderName, string modelFileName, MCBlockModelCollection blockModels)
        {
            json = JObject.Parse(File.ReadAllText(baseFolderName + ModelFolderPath + modelFileName + ".json"));
            
            //Load or reference parent model
            JToken parentToken = json.GetValue("parent");
            if (parentToken != null)
            {
                string parentFileName = parentToken.Value<string>().Replace("block/", "");
                parent = blockModels.FromFile(parentFileName);
            }

            //Load texture bitmaps - TODO lazy loading of texture bitmaps? Not as bad since reuse of models 
            IDictionary<string, JToken> texturesDict = (JObject)json["textures"];
            if (texturesDict != null)
            {
                foreach (KeyValuePair<string, JToken> kv in texturesDict)
                {
                    string textureRef = kv.Value.ToString();
                    if (textureRef.StartsWith("#"))
                    {
                        textureReferences.Add(kv.Key, textureRef);
                    }
                    else
                    {
                        textures.Add(kv.Key, new Bitmap(baseFolderName + TextureFolderPath + kv.Value.ToString().Replace("/", "\\") + ".png"));
                    }
                }
            }

            //Load elements
            JArray elementsJsonList = (JArray)json["elements"];
            if (elementsJsonList != null)
            {
                foreach (JObject elementJson in elementsJsonList)
                {
                    elements.Add(new MCBlockElement(elementJson));
                }
            }
        }

        public Bitmap GetSideImage(Sides side)
        {
            //TODO: verify if the final blockmodel always has the textures
            if (textures.Count == 0)
                return null;

            Bitmap bm;

            //TODO: implement multiple face textures, using elements etc
            if (!textures.TryGetValue("all", out bm))
            {
                //throw new ApplicationException("Could not compute texture");
                return null;
            }

            return bm;
        }

        public override string ToString()
        {
            return json.ToString();
        }
    }
}
