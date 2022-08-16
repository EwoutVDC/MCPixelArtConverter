using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Newtonsoft.Json.Linq;
using System.IO;
using System.IO.Compression;

namespace MCPixelArtConverter
{
    class MCBlockModel
    {
        const string ModelFolderPath = "models/block/";
        const string ModelFolderPath_1_13 = "models/"; //1.13 changed base folder for references
        const string TextureFolderPath = "textures/";

        //If a parent is set, that contains all elements. They can't be altered by children!
        MCBlockModel parent = null;
        public String Name { get; }
        //Element represent the 3d blocks that the block is assembled from.
        List<MCBlockElement> elements = new List<MCBlockElement>();

        Dictionary<string, Bitmap> textures = new Dictionary<string, Bitmap>();
        Dictionary<string, string> textureReferences = new Dictionary<string, string>();
        //common textures keys: all, top, bottom, north, south, east, west, side, end
        //other examples: wool (carpet_color.json)
                
        //TODO: P4 Is there a way to protect the constructor from being used from anywhere else than MCBlockModelCollection?
        //By extending the class and making the constructor protected????
        public MCBlockModel(ZipArchive jar, string modelFileName, MCBlockModelCollection blockModels)
        {
            Name = modelFileName;

            modelFileName = modelFileName.Replace("minecraft:", ""); //Since 1.16, references start with 'minecraft:'

            ZipArchiveEntry entry = jar.GetEntry(MCResourcePack.AssetFolderPath + ModelFolderPath + modelFileName + ".json");
            if (entry == null)
            {
                //1.13 changed block model location
                entry = jar.GetEntry(MCResourcePack.AssetFolderPath + ModelFolderPath_1_13 + modelFileName + ".json");
            }
            if (entry == null)
            {
                Console.WriteLine("Could not open entry for Name " + Name + " modelFileName " + modelFileName);
                throw new Exception("Could not open entry for Name " + Name + " modelFileName " + modelFileName) ;
            }
            using (Stream s = entry.Open())
            {
                using (StreamReader r = new StreamReader(s))
                {
                    JObject json = JObject.Parse(r.ReadToEnd());

                    //Load or reference parent model
                    JToken parentToken = json.GetValue("parent");
                    if (parentToken != null)
                    {
                        string parentFileName = parentToken.Value<string>();
                        parent = blockModels.FromFile(parentFileName);
                    }

                    //Load texture bitmaps
                    //TODO: P3 lazy loading of texture bitmaps? Not as bad since reuse of models 
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
                                textureRef = textureRef.Replace("minecraft:", ""); // Since 1.16 some references start with "minecraft:"
                                ZipArchiveEntry textureEntry = jar.GetEntry(MCResourcePack.AssetFolderPath + TextureFolderPath + textureRef + ".png");
                                using (Stream st = textureEntry.Open())
                                {
                                    using (MemoryStream mt = new MemoryStream())
                                    {
                                        st.CopyTo(mt);
                                        textures.Add(kv.Key, new Bitmap(Image.FromStream(mt)));
                                    }
                                }
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
                //TODO: P4 option to filter out blocks with transparent parts?
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
