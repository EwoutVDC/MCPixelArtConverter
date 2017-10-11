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

        //If a parent is set, that contains all elements. They can't be altered by children!
        MCBlockModel parent = null;
        public String Name { get; }
        //Element represent the 3d blocks that the block is assembled from.
        List<MCBlockElement> elements = new List<MCBlockElement>();

        Dictionary<string, Bitmap> textures = new Dictionary<string, Bitmap>();
        Dictionary<string, string> textureReferences = new Dictionary<string, string>();
        //common textures keys: all, top, bottom, north, south, east, west, side, end
        //other examples: wool (carpet_color.json)
                
        //TODO: Is there a way to protect the constructor from being used from anywhere else than MCBlockModelCollection?
        //By extending the class and making the constructor protected????
        public MCBlockModel(string baseFolderName, string modelFileName, MCBlockModelCollection blockModels)
        {
            JObject json = JObject.Parse(File.ReadAllText(baseFolderName + ModelFolderPath + modelFileName + ".json"));
            Name = modelFileName;
            
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
            if (textures.Count == 0)
                return null;

            Bitmap bm = new Bitmap(16,16);

            UpdateSideImage(side, bm, textures, textureReferences);

            return bm;
        }

        void UpdateSideImage(Sides side, Bitmap bm, Dictionary<string, Bitmap> textures, Dictionary<string, string> textureReferences)
        {
            //merge texture maps, keeping properties from child
            foreach(KeyValuePair<string, Bitmap> kv in this.textures)
            {
                if (!textures.ContainsKey(kv.Key))
                    textures.Add(kv.Key, kv.Value);
            }
            foreach (KeyValuePair<string, string> kv in this.textureReferences)
            {
                if (!textureReferences.ContainsKey(kv.Key))
                    textureReferences.Add(kv.Key, kv.Value);
            }

            if (elements.Count == 0)
            {
                if (parent != null)
                    parent.UpdateSideImage(side, bm, textures, textureReferences);
            }
            else
            {
                //Need to order elements depending on chosen side
                elements.Sort((x, y) => x.GetSortingValue(side).CompareTo(y.GetSortingValue(side)));
                
                //Orderering is important here!!! this is not good. ie check cauldron block model
                //stairs/slabs model are good examples of not completely opaque blocks
                //TODO: option to filter out blocks with transparent parts?
                foreach (MCBlockElement element in elements)
                {
                    element.UpdateSideImage(side, bm, textures, textureReferences);
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
