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

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Coordinate()
        {

        }

        /// <summary>
        /// Конструктор принимающей х и у
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
