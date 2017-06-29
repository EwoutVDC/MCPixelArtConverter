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
        public Double x { get; }
        public Double y { get; }
        public Double z { get; }

        public MCPoint(JArray json)
        {
            x = Double.Parse(json[0].ToString());
            y = Double.Parse(json[1].ToString());
            z = Double.Parse(json[2].ToString());
        }
    }
}
