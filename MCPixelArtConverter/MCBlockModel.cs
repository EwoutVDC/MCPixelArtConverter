﻿using System;
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
        JObject json;

        public MCBlockModel(String modelFileName)
        {
            json = JObject.Parse(File.ReadAllText(Program.BaseFolderName + ModelFolderPath + modelFileName + ".json"));

            //Load parent model TODO

            //Load texture bitmaps
            IDictionary<string, JToken> texturesDict = (JObject)json["textures"];
            textures = texturesDict.ToDictionary(
                pair => pair.Key,
                pair => new Bitmap(Program.BaseFolderName + TextureFolderPath + pair.Value.ToString().Replace("/", "\\") + ".png"));

            //Load elements TODO
        }

        public Bitmap getTopView()
        {
            return textures["all"]; //TODO
        }

        public override String ToString()
        {
            return json.ToString();
        }
    }
}