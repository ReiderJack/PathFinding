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

        // Floyad related
        public List<Node> FloyadCalculate(ICollection<Node> graph)
        {
            List<Node> newListOfNodes = new List<Node>();
            var graphListNodes = graph.ToList();
            foreach (var node in graphListNodes)
            {
                newListOfNodes.Add(FloyadFillNodeWithConnections(node, graphListNodes));
            }
            newListOfNodes = newListOfNodes.OrderBy(n => n.NodeName).ToList();
            return FloyadCalculateDistances(newListOfNodes);
        }

        public Node FloyadFillNodeWithConnections(Node newNode, List<Node> listOfNodes)
        {
            Node nodeToAdd = new Node(newNode.NodeName);
            foreach (var con in newNode.Connections)
            {
                nodeToAdd.Connections.Add(new NodeConnection(con.Target,con.Distance));
            }
            foreach (var node in listOfNodes)
            {
                if (!nodeToAdd.Connections.Any(n => n.Target.NodeName == node.NodeName))
                {
                    if (nodeToAdd.NodeName == node.NodeName)
                    {
                        nodeToAdd.AddConnection(node, 0, false);
                    }
                    else
                    {
                        nodeToAdd.AddConnection(node, double.PositiveInfinity, false);
                    }
                }
            }
            nodeToAdd.Connections = nodeToAdd.Connections.OrderBy(c => c.Target.NodeName).ToList();
            return nodeToAdd;
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

        // Bellmmn-Ford

        public Node BellmanFordCalculateDistances(List<Node> nodes, string startingNode)
        {
            Node resultNode = new Node(startingNode);
            var graphListNodes = nodes.OrderBy(n => n.NodeName).ToList();
            int edgesCount = 0;
            foreach (var node in graphListNodes)
            {
                node.Connections = node.Connections.OrderBy(n => n.Target.NodeName).ToList();
                foreach (var con in node.Connections)
                {
                    edgesCount++;
                }
            }
            
            foreach (var node in nodes)
            {
                if (node.NodeName == resultNode.NodeName)
                {
                    foreach (var con in node.Connections)
                    {
                        resultNode.AddConnection(con.Target, con.Distance, false);
                    }
                }
                if(resultNode.Connections.Any(c => c.Target.NodeName != node.NodeName))
                {
                    resultNode.AddConnection(new Node(node.NodeName),double.PositiveInfinity, false);
                }
            }
            resultNode.Connections = resultNode.Connections.OrderBy(n => n.Target.NodeName).ToList();
            for(int i = 0; i < nodes.Count - 1; i++)
            {
                for (int j = 0; j < edgesCount; ++j)
                {
                    /*nodes[j].Connections[j]
                    int u = graph.edge[j].Source;
                    int v = graph.edge[j].Destination;
                    int weight = graph.edge[j].Weight;

                    if (distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                        distance[v] = distance[u] + weight;*/
                }
            }
            return resultNode;
        }
    }
}
