using Algorithms.Dijkstras;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinding.ViewModels
{
    public class DijkstrasViewModel : Screen
    {
        public BindableCollection<Node> NodesGraph { get; set; }

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

        private NodeConnection _selectedConnection;

        public NodeConnection SelectedConnection
        {
            get { return _selectedConnection; }
            set 
            {
                if (value == null) return;
                _selectedConnection = value;
                NotifyOfPropertyChange(() => SelectedConnection);
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

        public DijkstrasViewModel()
        {
            NodesGraph = new BindableCollection<Node>()
            {
              new Node("A"),
              new Node("B"),
              new Node("C"),
              new Node("D"),
              new Node("E"),
              new Node("F")
            };
            
            var nodeO = new Node("O");
            var nodeP = new Node("P");
            var node1 = new Node("T");

            var node0 = new Node("G");
            node0.AddConnection(node1, 23, false);
            node0.AddConnection(nodeO, 11, false);
            node0.AddConnection(nodeP, 100, false);

            NodesGraph.Add(node0);
            NodesGraph.Add(node1);
            NodesGraph.Add(nodeO);
            NodesGraph.Add(nodeP);
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
            NodesGraph.First().AddConnection(NodesGraph.ElementAt(2),12,false);
        }

        public void RemoveConnection()
        {
            if (SelectedNode == null) return;
            if (SelectedNode.Connections.Count() == 0) return;
            SelectedNode.Connections.RemoveAt(SelectedIndex);
        }
    }
}
