using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class DistanceCalculator
    {
        public ICollection<Node> CalculateDistancesDijkstra(ICollection<Node> graph, string startingNode)
        {
            if (!graph.Any(n => n.NodeName == startingNode))
                throw new ArgumentException("Starting node must be in graph.");

            InitialiseGraph(graph, startingNode);
            ProcessGraph(graph, startingNode);
            return graph;
        }

        private void InitialiseGraph(ICollection<Node> graph, string startingNode)
        {
            var listNodes = graph.ToList();
            foreach (Node node in listNodes)
                node.DistanceFromStart = double.PositiveInfinity;
            listNodes.First(n => n.NodeName == startingNode).DistanceFromStart = 0;
        }

        private void ProcessGraph(ICollection<Node> graph, string startingNode)
        {
            bool finished = false;
            var queue = graph.ToList();
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

        private Dictionary<string, double> ExtractDistances(ICollection<Node> graph)
        {
            return graph.ToDictionary(n => n.NodeName, n => n.DistanceFromStart);
        }

        public List<Node> FloyadCalculate(ICollection<Node> graph)
        {
            List<Node> newListOfNodes = new List<Node>();
            var graphListNodes = graph.ToList();
            foreach (var node in graphListNodes)
            {
                newListOfNodes.Add(FloyadFillNodeWithConnections(node, graphListNodes));
            }
            graphListNodes = graphListNodes.OrderBy(n => n.NodeName).ToList();
            return FloyadCalculateDistances(graphListNodes);
        }

        public Node FloyadFillNodeWithConnections(Node newNode, List<Node> listOfNodes)
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
            newNode.Connections = newNode.Connections.OrderBy(c => c.Target.NodeName).ToList();
            return newNode;
        }

        private List<Node> FloyadCalculateDistances(List<Node> nodes)
        {
            for (int k = 0; k < nodes.Count; k++)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if(nodes[i].Connections[j].Distance 
                            > nodes[i].Connections[k].Distance 
                                + nodes[k].Connections[j].Distance)
                        {
                            nodes[i].Connections[j].Distance
                                = nodes[i].Connections[k].Distance
                                    + nodes[k].Connections[j].Distance;
                        }
                    }
                }
            }
            return nodes;
        }
    }
}
