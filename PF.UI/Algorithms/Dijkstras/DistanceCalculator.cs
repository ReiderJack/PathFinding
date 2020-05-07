using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class DistanceCalculator
    {
        public Dictionary<string, double> CalculateDistancesDijkstra(Graph graph, string startingNode)
        {
            if (!graph.Nodes.Any(n => n.Key == startingNode))
                throw new ArgumentException("Starting node must be in graph.");

            InitialiseGraph(graph, startingNode);
            ProcessGraph(graph, startingNode);
            return ExtractDistances(graph);
        }

        private void InitialiseGraph(Graph graph, string startingNode)
        {
            foreach (Node node in graph.Nodes.Values)
                node.DistanceFromStart = double.PositiveInfinity;
            graph.Nodes[startingNode].DistanceFromStart = 0;
        }

        private void ProcessGraph(Graph graph, string startingNode)
        {
            bool finished = false;
            var queue = graph.Nodes.Values.ToList();
            while (!finished)
            {
                Node nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(n => !double.IsPositiveInfinity(n.DistanceFromStart));
                if (nextNode != null)
                {
                    ProcessNode(nextNode, queue);
                    queue.Remove(nextNode);
                }
                else
                {
                    finished = true;
                }
            }
        }

        private void ProcessNode(Node node, List<Node> queue)
        {
            var connections = node.Connections.Where(c => queue.Contains(c.Target));
            foreach (var connection in connections)
            {
                double distance = node.DistanceFromStart + connection.Distance;
                if (distance < connection.Target.DistanceFromStart)
                    connection.Target.DistanceFromStart = distance;
            }
        }

        private Dictionary<string, double> ExtractDistances(Graph graph)
        {
            return graph.Nodes.ToDictionary(n => n.Key, n => n.Value.DistanceFromStart);
        }

        public List<Node> CalculateFloyad(Graph graph)
        {
            List<Node> newListOfNodes = new List<Node>();
            var graphListNodes = graph.Nodes.Values.ToList();
            foreach (var node in graphListNodes)
            {
                newListOfNodes.Add(FillNodeWithConnections(node, graphListNodes));
            }
            return new List<Node>();
        }

        public Node FillNodeWithConnections(Node newNode, List<Node> listOfNodes)
        {
            foreach (var node in listOfNodes)
            {
                if (!newNode.Connections.Any(n => n.Target == node))
                {
                    if (newNode == node)
                    {
                        newNode.AddConnection(node, 0, false);
                    }
                    else
                    {
                        newNode.AddConnection(node, double.PositiveInfinity, false);
                    }
                }
            }
            return newNode;
        }

        private List<Node> CalculateDistances(List<Node> listOfNodes)
        {
            for (int i = 0; i < listOfNodes.Count; i++)
            {
                foreach (var node in listOfNodes)
                {
                    foreach (var connection in node.Connections)
                    {
                        foreach (var con in connection.Target.Connections)
                        {
                            /*connection.Distance > con.Distance + connection.Distance*/
                        }
                    }
                }
            }
            return new List<Node>();
        }
    }
}
