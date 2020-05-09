using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class Program
    {
        static void Main()
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
            graph.AddConnection("A", "B", 14, true);
            graph.AddConnection("A", "C", 10, true);
            graph.AddConnection("A", "D", 14, true);
            graph.AddConnection("A", "E", 21, true);
            graph.AddConnection("B", "C", 9, true);
            graph.AddConnection("B", "E", 10, true);
            graph.AddConnection("B", "F", 14, true);
            graph.AddConnection("C", "D", 9, false);
            graph.AddConnection("D", "G", 10, false);
            graph.AddConnection("E", "H", 11, true);
            graph.AddConnection("F", "C", 10, false);
            graph.AddConnection("F", "H", 10, true);
            graph.AddConnection("F", "I", 9, true);
            graph.AddConnection("G", "F", 8, false);
            graph.AddConnection("G", "I", 9, true);
            graph.AddConnection("H", "J", 9, true);
            graph.AddConnection("I", "J", 10, true);

            Graph graph1 = new Graph();

            graph1.AddNode("A");
            graph1.AddNode("B");
            graph1.AddNode("D");
            graph1.AddNode("C");

            graph1.AddConnection("A", "C", -2, false);
            graph1.AddConnection("C", "D", 2, false);
            graph1.AddConnection("B", "C", 3, false);
            graph1.AddConnection("B", "A", 4, false);
            graph1.AddConnection("D", "B", -1, false);

            /*foreach (var node in graph1.Nodes.Values)
            {
                Console.WriteLine(node.NodeName);
                foreach (var con in node.Connections)
                {
                    Console.WriteLine("{0}, {1}", con.Target.NodeName, con.Distance);
                }
            }*/
            Console.WriteLine("Calculated graph");
            var calculator = new DistanceCalculator();
            var distances = calculator.FloyadCalculate(graph.Nodes.Values);  // Start from "G"

            /*foreach (var d in distances)
            {
                Console.WriteLine("{0}, {1}", d.NodeName, d.DistanceFromStart);
            }*/
            foreach (var node in distances)
            {
                Console.WriteLine(node.NodeName);
                foreach (var con in node.Connections)
                {
                    Console.WriteLine("{0}, {1}", con.Target.NodeName, con.Distance);
                }
            }
            Console.Read();
        }
    }
}
