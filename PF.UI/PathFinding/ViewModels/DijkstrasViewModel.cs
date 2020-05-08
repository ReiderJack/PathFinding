using Algorithms.Dijkstras;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace PathFinding.ViewModels
{
    public class DijkstrasViewModel : Screen
    {
        public BindableCollection<BindableCollection<Node>> DefaultGraphs { get; set; }

        private BindableCollection<Node> _nodesGraph;

        public BindableCollection<Node> NodesGraph
        {
            get => _nodesGraph;

            set
            {
                if(_nodesGraph == null)
                {
                    new BindableCollection<Node>();
                }
                _nodesGraph = value;
                NotifyOfPropertyChange(() => NodesGraph);
            }
        }

        private BindableCollection<Node> _calculationResult;

        public BindableCollection<Node> CalculationResult
        {
            get => _calculationResult;

            set
            {
                if (_calculationResult == null)
                {
                    new BindableCollection<Node>();
                }
                _calculationResult = value;
                NotifyOfPropertyChange(() => CalculationResult);
            }
        }

        private Node _selectedNode;

        public Node SelectedNode
        {
            get { return _selectedNode; }
            set 
            {
                _selectedNode = value;
                NotifyOfPropertyChange(() => SelectedNode);
            }
        }

        private string _newNodeName;

        public string NewNodeName 
        { 
            get => _newNodeName;
            set
            {
                _newNodeName = value;
                NotifyOfPropertyChange(() => NewNodeName);
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set 
            {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }

        private string _newConnectionTargetName;

        public string NewConnectionTargetName
        {
            get { return _newConnectionTargetName; }
            set 
            {
                _newConnectionTargetName = value;
                NotifyOfPropertyChange(() => NewConnectionTargetName);
            }
        }

        private double _newConnectionDistance;

        public double NewConnectionDistance
        {
            get { return _newConnectionDistance; }
            set 
            { 
                _newConnectionDistance = value;
                NotifyOfPropertyChange(() => NewConnectionDistance);
            }
        }

        public DijkstrasViewModel()
        {
            DefaultGraphs = new BindableCollection<BindableCollection<Node>>();

            DefaultGraphs.Add(FillFirstDefaultGraph());
        }

        private BindableCollection<Node> FillFirstDefaultGraph()
        {
            var graph = new BindableCollection<Node>()
            {
                new Node("A"),
                new Node("B"),
                new Node("C"),
                new Node("D"),
                new Node("E"),
                new Node("F"),
                new Node("G"),
                new Node("H"),
                new Node("I"),
                new Node("J"),
                new Node("Z"),
            };

            graph[0].AddConnection(GetNodeByName(graph,"B"), 14, true);
            graph[0].AddConnection(GetNodeByName(graph, "C"), 10, true);
            graph[0].AddConnection(GetNodeByName(graph, "D"), 14, true);
            graph[0].AddConnection(GetNodeByName(graph, "E"), 21, true);

            graph[1].AddConnection(GetNodeByName(graph, "C"), 9, true);
            graph[1].AddConnection(GetNodeByName(graph, "E"), 10, true);
            graph[1].AddConnection(GetNodeByName(graph, "F"), 14, true);

            graph[2].AddConnection(GetNodeByName(graph, "D"), 9, false);

            // From D
            graph[3].AddConnection(GetNodeByName(graph, "G"), 10, false);

            // From E
            graph[4].AddConnection(GetNodeByName(graph, "H"), 11, true);

            // From F
            graph[5].AddConnection(GetNodeByName(graph, "C"), 10, false);
            graph[5].AddConnection(GetNodeByName(graph, "H"), 10, true);
            graph[5].AddConnection(GetNodeByName(graph, "I"), 9, true);

            // From G
            graph[6].AddConnection(GetNodeByName(graph, "F"), 8, false);
            graph[6].AddConnection(GetNodeByName(graph, "I"), 9, true);

            // From H
            graph[7].AddConnection(GetNodeByName(graph, "J"), 9, true);

            // From I
            graph[8].AddConnection(GetNodeByName(graph, "J"), 10, true);

            return graph;
        }

        private Node GetNodeByName(BindableCollection<Node> graph, string name)
        {
            return graph.FirstOrDefault(n => n.NodeName == name);
        }

        public bool DoesGraphHaveNode(Node node)
        {
            if(NodesGraph.Any(n => n.NodeName == node.NodeName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DoesGraphHaveNode(string node)
        {
            if (NodesGraph.Any(n => n.NodeName == node))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddNode()
        {
            if(String.IsNullOrWhiteSpace(_newNodeName) 
                || DoesGraphHaveNode(_newNodeName))
            {
                return;
            }
            else
            {
                NodesGraph.Add(new Node(_newNodeName));
                NewNodeName = null;
            }
        }

        public void RemoveNode()
        {
            if (NodesGraph.Contains(SelectedNode))
            {
                foreach (var node in NodesGraph)
                {
                    node.RemoveConnection(SelectedNode);
                }
                NodesGraph.Remove(SelectedNode);
            }
        }

        public void AddNewConnection()
        {
            if (SelectedNode == null) return;
            if (String.IsNullOrWhiteSpace(NewConnectionTargetName)) return;
            if (SelectedNode.Connections.Any(c => c.Target.NodeName == NewConnectionTargetName)) return;
            if (NewConnectionDistance < 0) return;
            if (!NodesGraph.Any(n => n.NodeName == NewConnectionTargetName)) return;
            Node chosenNode = NodesGraph.First(n => n.NodeName == NewConnectionTargetName);
            SelectedNode.AddConnection(chosenNode, NewConnectionDistance, false);
        }

        public void RemoveConnection()
        {
            if (SelectedNode == null) return;
            if (SelectedIndex >= SelectedNode.Connections.Count()) return;
            SelectedNode.Connections.RemoveAt(SelectedIndex);
        }

        public void CalculateDijkstra()
        {
            if (NodesGraph.Count() == 0) return;
            var newResultGraph = NodesGraph;
            CalculationResult = null;
            var calculator = new DistanceCalculator();

            CalculationResult = (BindableCollection<Node>)calculator.CalculateDistancesDijkstra(newResultGraph, SelectedNode.NodeName); ;
        }
    }
}
