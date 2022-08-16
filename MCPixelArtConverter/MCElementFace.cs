using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCElementFace
    {
        public string TextureName { get; }
        //uv coords represent area of the texture that is used (cut out) for this element face
        //(0,0) is topleft of texture. Default is (0,0) -> (16,16)
        //if Umax < Umin, the texture is rotated TODO continue here, this can crash for ie door_bottom from north
        public float UMin { get; }
        public float UMax { get; }
        public float VMin { get; }
        public float VMax { get; }
        public Int16 Rotation { get; }
        //cullface not used for this application

        public MCElementFace(JObject json)
        {
            TextureName = json["texture"].ToString();
            JArray uv = (JArray)json["uv"];
            if (uv != null)
            {
                UMin = float.Parse(uv[0].ToString());
                VMin = float.Parse(uv[1].ToString());
                UMax = float.Parse(uv[2].ToString());
                VMax = float.Parse(uv[3].ToString());
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

        public Bitmap GetBitmap(Dictionary<string, Bitmap> textures, Dictionary<string, string> textureReferences)
        {
            if (UMin == UMax || VMin == VMax)
                return null; // This is an empty slice. Width or heigth 0
            return FindBitmap(TextureName, textures, textureReferences);
        }

        Bitmap FindBitmap(string name, Dictionary<string, Bitmap> textures, Dictionary<string, string> textureReferences)
        {
            if (name.StartsWith("#"))
            {
                name = name.Substring(1);
            }

            if (textures.ContainsKey(name))
            {
                RotateFlipType flipType = RotateFlipType.RotateNoneFlipNone;
                RotateFlipType rotation = RotateFlipType.RotateNoneFlipNone;
                RectangleF rect = new RectangleF(Math.Min(UMin, UMax), Math.Min(VMin, VMax), Math.Abs(UMax - UMin), Math.Abs(VMax - VMin));
                if (UMax < UMin)
                {
                    if (VMax < VMin)
                        flipType = RotateFlipType.RotateNoneFlipXY;
                    else
                        flipType = RotateFlipType.RotateNoneFlipX;
                }
                else if (VMax < VMin)
                {
                    flipType = RotateFlipType.RotateNoneFlipY;
                }

                rotation = RotationMatrix.RotateFlipTypeFromDegrees(Rotation);

                Bitmap bm = textures[name].Clone(rect, textures[name].PixelFormat);
                bm.RotateFlip(flipType);
                bm.RotateFlip(rotation);
                return bm;
            }
            else if (textureReferences.ContainsKey(name))
            {
                return FindBitmap(textureReferences[name], textures, textureReferences);
            }
            else
            {
                return null;
            }
        }
    }
}

