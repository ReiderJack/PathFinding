using Algorithms.Dijkstras;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace PathFinding.ViewModels
{
    public class BellmanFordViewModel : Screen
    {
        public BindableCollection<BindableCollection<Node>> DefaultGraphs { get; set; }

        private BindableCollection<Node> _nodesGraph;

        public BindableCollection<Node> NodesGraph
        {
            get => _nodesGraph;

            set
            {
                if (_nodesGraph == null)
                {
                    new BindableCollection<Node>();
                }
                _nodesGraph = value;
                NotifyOfPropertyChange(() => NodesGraph);
                FillCanvasGraph();
                FillLinesData();
            }
        }

        private BindableCollection<NodeCoordinate> _nodesGraphCoord;

        public BindableCollection<NodeCoordinate> NodesGraphCoord
        {
            get => _nodesGraphCoord;

            set
            {
                if (_nodesGraphCoord == null)
                {
                    _nodesGraphCoord = new BindableCollection<NodeCoordinate>();
                }
                _nodesGraphCoord = value;
                NotifyOfPropertyChange(() => NodesGraphCoord);
            }
        }

        private BindableCollection<LineData> _linesData;

        public BindableCollection<LineData> LinesData 
        {
            get => _linesData;
            set
            {
                if(_linesData == null)
                {
                    _linesData = new BindableCollection<LineData>();
                }
                _linesData = value;
                NotifyOfPropertyChange(() => LinesData);
            }
        }

        private Node _calculationResult;

        public Node CalculationResult
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

        public BellmanFordViewModel()
        {
            DefaultGraphs = new BindableCollection<BindableCollection<Node>>();

            DefaultGraphs.Add(FillFirstDefaultGraph());

            DefaultGraphs.Add(FillSecondDefaultGraph());
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

            graph[0].AddConnection(GetNodeByName(graph, "B"), 14, true);
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

        private BindableCollection<Node> FillSecondDefaultGraph()
        {
            var graph = new BindableCollection<Node>()
            {
                new Node("A"),
                new Node("B"),
                new Node("C"),
                new Node("D"),
                new Node("E"),
                new Node("S"),
            };

            graph[0].AddConnection(GetNodeByName(graph, "C"), 2, false);

            graph[1].AddConnection(GetNodeByName(graph, "A"), 1, false);

            graph[2].AddConnection(GetNodeByName(graph, "B"), -2, false);

            graph[3].AddConnection(GetNodeByName(graph, "C"), -1, false);
            graph[3].AddConnection(GetNodeByName(graph, "A"), -4, false);

            graph[4].AddConnection(GetNodeByName(graph, "D"), 1, false);

            graph[5].AddConnection(GetNodeByName(graph, "A"), 10, false);
            graph[5].AddConnection(GetNodeByName(graph, "E"), 8, false);

            return graph;
        }
        
        private Node GetNodeByName(BindableCollection<Node> graph, string name)
        {
            return graph.FirstOrDefault(n => n.NodeName == name);
        }

        public bool DoesGraphHaveNode(Node node)
        {
            if (NodesGraph.Any(n => n.NodeName == node.NodeName))
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
            if (String.IsNullOrWhiteSpace(_newNodeName)
                || DoesGraphHaveNode(_newNodeName))
            {
                return;
            }
            else
            {
                NodesGraph.Add(new Node(_newNodeName));
                NewNodeName = null;
            }
            NodesGraph = NodesGraph;
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
            NodesGraph = NodesGraph;
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

            NodesGraph = NodesGraph;
        }

        public void RemoveConnection()
        {
            if (SelectedNode == null) return;
            if (SelectedIndex >= SelectedNode.Connections.Count()) return;
            SelectedNode.Connections.RemoveAt(SelectedIndex);

            NodesGraph = NodesGraph;
        }

        public void CalculateBellman()
        {
            if (SelectedNode == null) return;
            if (NodesGraph == null) return;
            if (NodesGraph.Count() == 0) return;
            var newResultGraph = NodesGraph.ToList();
            CalculationResult = null;
            var calculator = new DistanceCalculator();

            CalculationResult = calculator.BellmanFordCalculateDistances(newResultGraph, SelectedNode.NodeName);
        }

        private void FillCanvasGraph()
        {
            if (NodesGraph.Count == 0) return;
            int step = 360 / NodesGraph.Count;

            var canvasGraph = new BindableCollection<NodeCoordinate>();
            for (int i = 0; i < NodesGraph.Count; i++)
            {
                var coordinate = pov(new Coordinate(400, 400), 0 + step * i, 200);
                canvasGraph.Add(new NodeCoordinate(NodesGraph[i], coordinate.X, coordinate.Y));
            }
            NodesGraphCoord = canvasGraph;
        }

        private void FillLinesData()
        {
            if (NodesGraphCoord.Count == 0) return;
            var linesData = new BindableCollection<LineData>();
            foreach (var node in NodesGraphCoord)
            {
                foreach (var connection in node.Node.Connections)
                {
                    var nodePoint1 = new Coordinate(node.X, node.Y);

                    var targetNodeCoords = NodesGraphCoord.FirstOrDefault(n => n.Node.NodeName == connection.Target.NodeName);
                    var nodePoint2 = new Coordinate(targetNodeCoords.X, targetNodeCoords.Y);
                    var p1 = new Coordinate(nodePoint1.X , nodePoint1.Y);
                    var p2 = new Coordinate(nodePoint2.X, nodePoint2.Y);

                    linesData.Add(new LineData(nodePoint1, nodePoint2, connection.Distance));
                }
            }

            LinesData = linesData;
        }

        public Coordinate pov(Coordinate p, int angle, int width)
        {
            int ang = 0;
            while (angle < 0)
                angle += 360;
            angle = angle % 360;

            Coordinate e = p;

            if (angle >= 0 && angle <= 90)
                ang = angle;
            if (angle > 90 && angle <= 180)
                ang = 180 - angle;
            if (angle > 180 && angle <= 270)
                ang = angle - 180;
            if (angle > 270 && angle <= 360)
                ang = 360 - angle;

            double ax = Math.Sin(ang * 0.0175);
            double ay = Math.Cos(ang * 0.0175);

            if (angle >= 0 && angle <= 90)
            {
                e.X = (p.X + (int)(ax * width));
                e.Y = (p.Y - (int)(ay * width));
            }
            if (angle > 90 && angle <= 180)
            {
                e.X = (p.X + (int)(ax * width));
                e.Y = (p.Y + (int)(ay * width));
            }
            if (angle > 180 && angle <= 270)
            {
                e.X = (p.X - (int)(ax * width));
                e.Y = (p.Y + (int)(ay * width));
            }
            if (angle > 270 && angle <= 360)
            {
                e.X = (p.X - (int)(ax * width));
                e.Y = (p.Y - (int)(ay * width));
            }

            return e;
        }
    }
}
