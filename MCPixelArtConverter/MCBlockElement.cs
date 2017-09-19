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

        /*
        "elements": [
        {   "from": [ 0, 0, 0 ],
            "to": [ 16, 16, 16 ],
            "faces": {
                "down":  { "uv": [ 0,  0, 16, 16 ], "texture": "#pattern", "cullface": "down" },
                "up":    { "uv": [ 0,  0, 16, 16 ], "texture": "#pattern", "cullface": "up" },
                "north": { "uv": [ 0,  0, 16, 16 ], "texture": "#pattern", "cullface": "north", "rotation": 90 },
                "south": { "uv": [ 0,  0, 16, 16 ], "texture": "#pattern", "cullface": "south", "rotation": 270 },
                "west":  { "uv": [ 0,  0, 16, 16 ], "texture": "#pattern", "cullface": "west", "rotation": 0 },
                "east":  { "uv": [ 0,  0, 16, 16 ], "texture": "#pattern", "cullface": "east", "rotation": 180 }
            }
        }
        ]
        */

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
                    return sortingPoint.Y * coef;
                case Sides.East:
                case Sides.West:
                    return sortingPoint.X * coef;
                case Sides.North:
                case Sides.South:
                    return sortingPoint.Z * coef;
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

            Graphics g = Graphics.FromImage(image);
            g.DrawImageUnscaledAndClipped(sideFace, GetImageArea(side));
        }

        Rectangle GetImageArea(Sides side)
        {
            float uMin = 0, vMin = 0, uMax = 0, vMax = 0;

            switch(side)
            {
                case Sides.Up:
                case Sides.Down:
                    uMin = from.X;
                    uMax = to.X;
                    vMin = from.Z;
                    vMax = to.Z;
                    break;
                case Sides.East:
                case Sides.West:
                    uMin = from.Z;
                    uMax = to.Z;
                    vMin = 16 - to.Y;
                    vMax = 16 - from.Y;
                    break;
                case Sides.North:
                case Sides.South:
                    uMin = from.X;
                    uMax = to.X;
                    vMin = 16 - to.Y;
                    vMax = 16 - from.Y;
                    break;
            }

            return new Rectangle((int)uMin, (int)vMin, (int)(uMax - uMin), (int)(vMax - vMin));
        }

        public override string ToString()
        {
            return from + ">" + to;
        }
    }
}
