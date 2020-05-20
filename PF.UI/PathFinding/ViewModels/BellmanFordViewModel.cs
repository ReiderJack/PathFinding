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

        public void OpenGraph()
        {
            Process.Start("C:/Users/Jack/Desktop/release/releaseee/gg.exe");
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

        public Coordinate[] strelka(Coordinate p1, Coordinate p2)
        {
            //    int os = 10;
            int kr = 15;
            int kr_ang = 40;
            int angle = 0;
            int tochn = 5;
            Coordinate dd;
            for (int i = 0; i < 361; i++)
            {
                dd = pov(p2, i,dlina(p2, p1));
                if (Math.Abs(p1.X- dd.X) < tochn && Math.Abs(p1.Y - dd.Y) < tochn)
                    angle = i;
            }
            Coordinate kr1 = pov(p2, angle - kr_ang, kr);
            Coordinate kr2 = pov(p2, angle + kr_ang, kr);

            return new Coordinate[] { kr1, kr2 };
        }

        public int ArrowAngle(Coordinate p1, Coordinate p2)
        {
            int angle = 0;
            int tochn = 20;
            Coordinate dd;
            for (int i = 0; i < 361; i++)
            {
                dd = pov(p2, i, dlina(p2, p1));
                if (Math.Abs(p1.X - dd.X) < tochn && Math.Abs(p1.Y - dd.Y) < tochn)
                    angle = i;
            }
            return angle;
        }

        public int orent(Coordinate p1, Coordinate p2)
        {
            if (p1.X == p2.X)
            {
                if (p1.Y > p2.Y)
                    return 1;
                if (p1.Y < p2.Y)
                    return 5;
            }
            if (p1.Y == p2.Y)
            {
                if (p1.X > p2.X)
                    return 7;
                if (p1.X < p2.X)
                    return 3;
            }
            if (p1.X > p2.X)
            {
                if (p1.Y > p2.Y)
                    return 8;
                if (p1.Y < p2.Y)
                    return 6;
            }
            if (p1.X < p2.X)
            {
                if (p1.Y > p2.Y)
                    return 2;
                if (p1.Y < p2.Y)
                    return 4;
            }
            return 0;
        }

        public int dlina(Coordinate p1, Coordinate p2)
        {
            switch (orent(p1, p2))
            {
                case 0: return 0;
                case 1: return (int)(p1.Y - p2.Y);
                case 2: return (int)(Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
                case 3: return (int)(p2.X - p1.X);
                case 4: return (int)(Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
                case 5: return (int)(p2.Y - p1.Y);
                case 6: return (int)(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p2.Y - p1.Y, 2)));
                case 7: return (int)(p1.X - p2.X);
                case 8: return (int)(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
            }
            return 0;
        }
    }

    public class Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Coordinate()
        {

        }
        public Coordinate(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public class LineData
    {
        public Coordinate Point1 { get; set; }
        public Coordinate Point2 { get; set; }
        public double ConnectionDistance { get; set; }
        public Coordinate DistanceCoordinate { get; set; }

        public Coordinate ArrowCoord { get; set; }
        public double Angle { get; set; }
        public Thickness ArrowOffset { get; set; }

        public LineData()
        {
               
        }

        public LineData(Coordinate point1, Coordinate point2, double distance)
        {
            Point1 = point1;
            Point2 = point2;

            ConnectionDistance = distance;
            DistanceCoordinate = new Coordinate((Point2.X + Point1.X) / 2, (Point2.Y +Point1.Y) /2);

            // Arrow rotation
            float xDiff = Point2.X - Point1.X;
            float yDiff = Point2.Y - Point1.Y;
            Angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

            ArrowOffset = new Thickness(-16 / 2, -16 / 2, 0, 0);

            ArrowCoord = new Coordinate((Point2.X + Point1.X -15) / 2, (Point2.Y + Point1.Y-15) / 2);
        }

    }
}
