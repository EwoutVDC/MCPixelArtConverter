using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Drawing;
using System.IO.Compression;

namespace MCPixelArtConverter
{
    class MCBlockState
    {
        public string FileName { get; }
        Dictionary<string, MCBlockVariant> VariantsByName = new Dictionary<string, MCBlockVariant>();
        //TODO: P3 multipart blockstates. ie fences. these are fairly complex with apply conditions for certain parts

        public MCBlockState(ZipArchiveEntry entry, MCBlockModelCollection blockModels)
        {
            FileName = Path.GetFileNameWithoutExtension(entry.Name);
            using (Stream s = entry.Open())
            {
                using (StreamReader r = new StreamReader(s))
                {
                    JObject blockStateJson = JObject.Parse(r.ReadToEnd()); 
                    IDictionary<string, JToken> variantsDict = (JObject)blockStateJson["variants"];
                    if (variantsDict != null)
                        VariantsByName = variantsDict.ToDictionary(pair => pair.Key, pair => new MCBlockVariant(this, pair.Key, pair.Value, blockModels));
                }
            }
        }

        public Dictionary<MCBlockVariant, Bitmap> GetSideImages(Sides facing)
        {
            Dictionary<MCBlockVariant, Bitmap> textures = new Dictionary<MCBlockVariant, Bitmap>();

            foreach (MCBlockVariant variant in VariantsByName.Values)
            {
                Bitmap bitmap = variant.GetSideImage(facing);
                if (bitmap != null)
                    textures.Add(variant, bitmap);
            }

            return textures;
        }

        public List<MCBlockVariant> GetVariants()
        {
            return VariantsByName.Values.ToList();
        }

        public override string ToString()
        {
            return FileName;
        }

        public void SetSelected(bool selected)
        {
            foreach (MCBlockVariant variant in VariantsByName.Values)
            {
                variant.Selected = selected;
            }
        }

        public bool GetSelected()
        {
            foreach (MCBlockVariant variant in VariantsByName.Values)
            {
                if (variant.Selected)
                    return true;
            }
            return false;
        }

        public void SaveBlockSelection(StreamWriter selectionFile)
        {
            foreach (var blockVariant in VariantsByName)
            {
                if (blockVariant.Value.Selected)
                    selectionFile.WriteLine(this.FileName + "\t" + blockVariant.Value.Name);
            }
        }

        public void LoadBlockSelection(List<string> selectedVariantNames)
        {
            foreach (var selectedVariantName in selectedVariantNames)
            {
                MCBlockVariant selectedVariant;
                if (VariantsByName.TryGetValue(selectedVariantName, out selectedVariant))
                    selectedVariant.Selected = true;
            }
        }
    }
}
