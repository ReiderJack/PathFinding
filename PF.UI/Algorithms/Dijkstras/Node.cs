using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class Node
    {
        List<NodeConnection> _connections;

        public string NodeName { get; set; }

        public double DistanceFromStart { get; set; }

        public List<NodeConnection> Connections
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
            // if (targetNode == this) throw new ArgumentException("Node may not connect to itself.");
            // if (distance <= 0) throw new ArgumentException("Distance must be positive.");

            _connections.Add(new NodeConnection(targetNode, distance));
            if (twoWay) targetNode.AddConnection(this, distance, false);
        }

        public void RemoveConnection(Node targetNode)
        {
            if (targetNode == null) return;
            if (_connections.Any(c => c.Target == targetNode))
            {
                _connections.Remove(_connections.Find(c => c.Target == targetNode));
            }
        }

        public void RemoveConnection(NodeConnection connection)
        {
            if (connection == null) return;
            if (_connections.Contains(connection))
            {
                _connections.Remove(connection);
            }
        }
    }
}
