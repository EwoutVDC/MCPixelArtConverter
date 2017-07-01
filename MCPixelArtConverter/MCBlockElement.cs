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


        public float GetSortingValue(Sides side)
        {
            MCPoint sortingPoint = from;
            Int16 coef = -1; // Down, West, North = lowest coord value should be drawn last => have highest sorting value
            if (side == Sides.Up || side == Sides.East || side == Sides.South)
            {
                sortingPoint = to;
                coef = 1;
            }

            switch(side)
            {
                case Sides.Up:
                case Sides.Down:
                    return sortingPoint.y * coef;
                case Sides.East:
                case Sides.West:
                    return sortingPoint.x * coef;
                case Sides.North:
                case Sides.South:
                    return sortingPoint.z * coef;
                default:
                    return 0;
            }
        }

        //Adds the visible part of this element to the side view bitmap
        public void UpdateSideImage(Sides side, Bitmap image, Dictionary<string, Bitmap> textures, Dictionary<string, string> textureReferences)
        {
            if (!faces.ContainsKey(side))
                return;

            Bitmap sideFace = faces[side].GetBitmap(textures, textureReferences);
            if (sideFace == null)
                return;


            //TODO: this does not update the image, make it an out parameter?
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(sideFace, GetImageArea(side));
        }

        RectangleF GetImageArea(Sides side)
        {
            float uMin = 0, vMin = 0, uMax = 0, vMax = 0;

            switch(side)
            {
                case Sides.Up:
                case Sides.Down:
                    uMin = from.x;
                    uMax = to.x;
                    vMin = from.z;
                    vMax = to.z;
                    break;
                case Sides.East:
                case Sides.West:
                    uMin = from.z;
                    uMax = to.z;
                    vMin = 16 - to.y;
                    vMax = 16 - from.y;
                    break;
                case Sides.North:
                case Sides.South:
                    uMin = from.x;
                    uMax = to.x;
                    vMin = 16 - to.y;
                    vMax = 16 - from.y;
                    break;
            }

            return new RectangleF(uMin, vMin, (uMax - uMin), (vMax - vMin));
        }
    }
}
