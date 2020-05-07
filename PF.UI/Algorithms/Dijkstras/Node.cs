using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class Node
    {
        IList<NodeConnection> _connections;

        public string NodeName { get; set; }

        public double DistanceFromStart { get; set; }

        public IEnumerable<NodeConnection> Connections
        {
            get { return _connections; }
        }

        public Node(string name)
        {
            NodeName = name;
            _connections = new List<NodeConnection>();
        }

        public void AddConnection(Node targetNode, double distance, bool twoWay)
        {
            if (targetNode == null) throw new ArgumentNullException("targetNode");
            if (targetNode == this) throw new ArgumentException("Node may not connect to itself.");
            if (distance <= 0) throw new ArgumentException("Distance must be positive.");

            _connections.Add(new NodeConnection(targetNode, distance));
            if (twoWay) targetNode.AddConnection(this, distance, false);
        }
    }
}
