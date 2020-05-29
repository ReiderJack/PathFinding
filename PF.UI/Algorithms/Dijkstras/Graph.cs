using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    /// <summary>
    /// Используется только для теста
    /// </summary>
    public class Graph
    {
            
        public Dictionary<string, Node> Nodes { get; private set; }

        public Graph()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string name)
        {
            var node = new Node(name);
            Nodes.Add(name, node);
        }

        public void AddConnection(string fromNode, string toNode, int distance, bool twoWay)
        {
            Nodes[fromNode].AddConnection(Nodes[toNode], distance, twoWay);
        }
    }
}
