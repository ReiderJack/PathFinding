using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Algorithms.Dijkstras
{
    public class NodeCoordinate
    {
        public Node Node { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public Thickness Offset { get; set; }

        public NodeCoordinate()
        {

        }
        public NodeCoordinate(Node newNode, float x, float y)
        {
            Node = newNode;
            X = x;
            Y = y;
            Offset = new Thickness(-30 / 2, -30 / 2, 0, 0);
        }
    }
}
