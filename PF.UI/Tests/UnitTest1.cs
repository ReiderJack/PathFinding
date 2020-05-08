using Algorithms.Dijkstras;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DijkstraCalculate()
        {
            Graph graph = new Graph();

            //Nodes
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");
            graph.AddNode("F");
            graph.AddNode("G");
            graph.AddNode("H");
            graph.AddNode("I");
            graph.AddNode("J");
            graph.AddNode("Z");

            //Connections
            // From A
            graph.AddConnection("A", "B", 14, true);
            graph.AddConnection("A", "C", 10, true);
            graph.AddConnection("A", "D", 14, true);
            graph.AddConnection("A", "E", 21, true);

            // From B
            graph.AddConnection("B", "C", 9, true);
            graph.AddConnection("B", "E", 10, true);
            graph.AddConnection("B", "F", 14, true);

            // From C
            graph.AddConnection("C", "D", 9, false);

            // From D
            graph.AddConnection("D", "G", 10, false);

            // From E
            graph.AddConnection("E", "H", 11, true);

            // From F
            graph.AddConnection("F", "C", 10, false);
            graph.AddConnection("F", "H", 10, true);
            graph.AddConnection("F", "I", 9, true);

            // From G
            graph.AddConnection("G", "F", 8, false);
            graph.AddConnection("G", "I", 9, true);

            // From H
            graph.AddConnection("H", "J", 9, true);

            // From I
            graph.AddConnection("I", "J", 10, true);

            var calculator = new DistanceCalculator();
            var distances = calculator.CalculateDistancesDijkstra(graph.Nodes.Values, "G");

            Assert.Pass();
        }

        [Test]
        public void Test_RemoveConnection()
        {
            Node node0 = new Node("0");
            Node node1 = new Node("1");
            node0.AddConnection(node1, 13, false);

            node0.RemoveConnection(node0.Connections.First());

            Assert.That(node0.Connections.Count(), Is.EqualTo(0));
        }

        [Test]
        public void FloydCalculate()
        {
            Graph graph = new Graph();

            //Nodes
            graph.AddNode("A");
            graph.AddNode("B");
            graph.AddNode("C");
            graph.AddNode("D");
            graph.AddNode("E");
            graph.AddNode("F");
            graph.AddNode("G");
            graph.AddNode("H");
            graph.AddNode("I");
            graph.AddNode("J");
            graph.AddNode("Z");

            //Connections
            // From A
            graph.AddConnection("A", "B", 14, true);
            graph.AddConnection("A", "C", 10, true);
            graph.AddConnection("A", "D", 14, true);
            graph.AddConnection("A", "E", 21, true);

            // From B
            graph.AddConnection("B", "C", 9, true);
            graph.AddConnection("B", "E", 10, true);
            graph.AddConnection("B", "F", 14, true);

            // From C
            graph.AddConnection("C", "D", 9, false);

            // From D
            graph.AddConnection("D", "G", 10, false);

            // From E
            graph.AddConnection("E", "H", 11, true);

            // From F
            graph.AddConnection("F", "C", 10, false);
            graph.AddConnection("F", "H", 10, true);
            graph.AddConnection("F", "I", 9, true);

            // From G
            graph.AddConnection("G", "F", 8, false);
            graph.AddConnection("G", "I", 9, true);

            // From H
            graph.AddConnection("H", "J", 9, true);

            // From I
            graph.AddConnection("I", "J", 10, true);

            var calculator = new DistanceCalculator();
            var distances = calculator.FloyadCalculate(graph.Nodes.Values);

            Assert.Pass();
        }
    }
}