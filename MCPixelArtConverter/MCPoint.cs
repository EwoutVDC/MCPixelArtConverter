using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCPixelArtConverter
{
    class MCPoint
    {
        public float x { get; }
        public float y { get; }
        public float z { get; }

        public MCPoint(JArray json)
        {
            x = float.Parse(json[0].ToString());
            y = float.Parse(json[1].ToString());
            z = float.Parse(json[2].ToString());
        }
    }
}
