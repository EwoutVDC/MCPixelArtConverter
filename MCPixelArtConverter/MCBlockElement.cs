using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCBlockElement
    {
        MCPoint from, to;
        Dictionary<Sides, MCElementFace> faces = new Dictionary<Sides, MCElementFace>();
        //rotation needed?

        public MCBlockElement(JObject json)
        {
            from = new MCPoint((JArray)json["from"]);
            to = new MCPoint((JArray)json["to"]);

            IDictionary<string, JToken> facesDict = (JObject)json["faces"];
            foreach (KeyValuePair<string, JToken> kv in facesDict)
            {
                string sideText = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase((kv.Key.ToString()));
                Sides side = (Sides)Enum.Parse(typeof(Sides), sideText);
                faces.Add(side, new MCElementFace((JObject)kv.Value));
            }
        }

        //Adds the visible part of this element to the side view bitmap
        public void UpdateSideImage(Sides side, Bitmap image)
        {

        }
    }
}
