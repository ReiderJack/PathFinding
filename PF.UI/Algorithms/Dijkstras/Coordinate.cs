using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Coordinate()
        {

        }
        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
