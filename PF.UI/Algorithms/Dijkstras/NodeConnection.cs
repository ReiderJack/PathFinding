using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class NodeConnection
    {
        public Node Target { get; private set; }
        public double Distance { get; set; }

        public NodeConnection(Node target, double distance)
        {
            Target = target;
            Distance = distance;
        }
    }
}
