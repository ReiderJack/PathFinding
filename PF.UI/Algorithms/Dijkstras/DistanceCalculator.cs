using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class DistanceCalculator
    {
        // Dijkstra

        /// <summary>
        /// Вычисляет путь
        /// </summary>
        /// <param name="graph"> Лист с точками</param>
        /// <param name="startingNode"> Начальная точка вычисления пути</param>
        /// <returns></returns>
        public ICollection<Node> CalculateDistancesDijkstra(ICollection<Node> graph, string startingNode)
        {
            // Если начальная точка не существует в листе точек
            // Вызвать ошибку
            if (!graph.Any(n => n.NodeName == startingNode))
                throw new ArgumentException("Starting node must be in graph.");

            InitialiseGraph(graph, startingNode);
            ProcessGraph(graph, startingNode);
            return graph;
        }

        /// <summary>
        /// Задать каждую начальную точку бесконечностью
        /// </summary>
        /// <param name="graph"> Лист с точками </param>
        /// <param name="startingNode"> Начальная точка вычисления пути</param>
        private void InitialiseGraph(ICollection<Node> graph, string startingNode)
        {
            // Преобразования коллекции в лист
            var listNodes = graph.ToList();

            // В каждой точке листа задаем дистанцию от начальной точки как бесконечность 
            foreach (Node node in listNodes)
                node.DistanceFromStart = double.PositiveInfinity;

            // Задаем дистанцию начальной точки 0
            listNodes.First(n => n.NodeName == startingNode).DistanceFromStart = 0;
        }

        /// <summary>
        /// Вычисляет путь от начальной точки
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="startingNode"></param>
        private void ProcessGraph(ICollection<Node> graph, string startingNode)
        {
            // да\нет для выхода из цикла
            bool finished = false;

            // Задает новый лист в котором будут все точки
            // чтобы удалять точки из листа, когда точка была обработана 
            var queue = graph.ToList();

            // цикл вычисления
            while (!finished)
            {
                Node nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(n => !double.IsPositiveInfinity(n.DistanceFromStart));
                if (nextNode != null)
                {
                    // Вычисляет расстояние между точками
                    ProcessNode(nextNode, queue);

                    // Удаляет обработанную точку из листа
                    queue.Remove(nextNode);
                }
                else
                {
                    // все рассчитали
                    // приравниваем к да чтобы выйти из цикла
                    finished = true;
                }
            }
        }

        /// <summary>
        /// Вычисляет расстояние между точками
        /// </summary>
        /// <param name="node"> Точка на которой мы сейчас находимся</param>
        /// <param name="queue"> Лист с точками еще оставшимися </param>
        private void ProcessNode(Node node, List<Node> queue)
        {
            // Находит связи которые соединены с текущей точкой
            var connections = node.Connections.Where(c => queue.Contains(c.Target));

            // Для каждой связи в связях соединненых с текущей точкой
            foreach (var connection in connections)
            {
                // Дистанция от начальной точки до текущей точки 
                // плюс дистанция до следующей точки
                double distance = node.DistanceFromStart + connection.Distance;

                // Если дистанция меньше, то приравниваем дистанцию 
                if (distance < connection.Target.DistanceFromStart)
                    connection.Target.DistanceFromStart = distance;
            }
        }

        // Floyd-Warshall

        /// <summary>
        /// Вычисляет путь от всех точек
        /// </summary>
        /// <param name="graph"> Коллекция точек </param>
        /// <returns> Возвращаем лист со всеми посчитанными точками </returns>
        public List<Node> FloyadCalculate(ICollection<Node> graph)
        {
            // Создаем новый пустой лист для точек
            List<Node> newListOfNodes = new List<Node>();

            // Преобразуем коллекцию в лист
            var graphListNodes = graph.ToList();

            // Для каждой точки в листе точек
            foreach (var node in graphListNodes)
            {
                // Добавляем точку с заполненым листом связей 
                newListOfNodes.Add(FloyadFillNodeWithConnections(node, graphListNodes));
            }
            // Сортируем точки по имени
            newListOfNodes = newListOfNodes.OrderBy(n => n.NodeName).ToList();

            // Возвращаем лист со всеми посчитанными точками
            return FloyadCalculateDistances(newListOfNodes);
        }

        /// <summary>
        /// Заполняет точку всеми связями из листа точек
        /// </summary>
        /// <param name="newNode"> Точка для заполнения(текущая) </param>
        /// <param name="listOfNodes"> Лист всех точек </param>
        /// <returns> Возвращаем новую точку </returns>
        public Node FloyadFillNodeWithConnections(Node newNode, List<Node> listOfNodes)
        {
            // создаем новую пустую точку с именем текущей точки
            Node nodeToAdd = new Node(newNode.NodeName);

            // Заполняем новую точку связями текущей точки
            foreach (var con in newNode.Connections)
            {
                // Добавляем новую связь
                nodeToAdd.Connections.Add(new NodeConnection(con.Target,con.Distance));
            }

            // Для каждой точки в листе всех точек
            foreach (var node in listOfNodes)
            {
                // Если связь не ведет к текущей точке
                if (!nodeToAdd.Connections.Any(n => n.Target.NodeName == node.NodeName))
                {
                    // Если имя текущей точки равно точки обрабатываемой сейчас в цикле
                    if (nodeToAdd.NodeName == node.NodeName)
                    {
                        // Добавляем связь к самому себе и ставим дистанцию 0
                        nodeToAdd.AddConnection(node, 0, false);
                    }
                    else
                    {
                        // Добавляем связь к точке и ставвим расстояние бесконечность
                        nodeToAdd.AddConnection(node, double.PositiveInfinity, false);
                    }
                }
            }
            // Отсортировываем лист связей по имени точки к которой связь ведет и трансформируем это в лист
            nodeToAdd.Connections = nodeToAdd.Connections.OrderBy(c => c.Target.NodeName).ToList();

            // Возвращаем новую точку
            return nodeToAdd;
        }

        /// <summary>
        /// Вычисляет расстояние по алгоритму Флойда
        /// </summary>
        /// <param name="nodes"> Лист точек для вычисления </param>
        /// <returns> Возвращает лист со всеми просчитанными расстояниями </returns>
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
            // Возвращает лист со всеми просчитанными расстояниями 
            return nodes;
        }

        // Bellmmn-Ford

        /// <summary>
        /// Вычисляет по алгоритму Белмана Форда
        /// </summary>
        /// <param name="nodes"> Лист со всеми точками</param>
        /// <param name="startingNode"> Имя начальной точки </param>
        /// <returns> Возвращаем точку со всеми просчитанными путями </returns>
        public Node BellmanFordCalculateDistances(List<Node> nodes, string startingNode)
        {
            // Создаем новую точку с именем стартовой
            Node resultNode = new Node(startingNode);

            // Создаем новый лист и сортируем по имени точек
            var graphListNodes = nodes.OrderBy(n => n.NodeName).ToList();

            //Количество связей
            int edgesCount = 0;

            // Для каждой точки в листе
            foreach (var node in graphListNodes)
            {
                // Сортируем лист связей по имени точки до которой связь идет
                node.Connections = node.Connections.OrderBy(n => n.Target.NodeName).ToList();

                // Считает количество связей 
                foreach (var con in node.Connections)
                {
                    edgesCount++;
                }
            }
            
            // Для каждой точки в точках
            foreach (var node in nodes)
            {
                // Если имя точки равно имени стратовой точки
                if (node.NodeName == resultNode.NodeName)
                {
                    // Добавляет связь к самому себе с расстоянием 0
                    resultNode.AddConnection(new Node(node.NodeName), 0, false);
                }
                else
                {
                    // Добавляем связь к точке и задаем расстояние бесконечностью
                    resultNode.AddConnection(new Node(node.NodeName), double.PositiveInfinity, false);
                }
            }

            // Сортируем лист связей по имени точки до которой ведет связь и трансформируем в лист
            resultNode.Connections = resultNode.Connections.OrderBy(n => n.Target.NodeName).ToList();

            // Сам Алгоритм
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                foreach (var node in graphListNodes)
                {
                    foreach (var con in node.Connections)
                    {
                        if(resultNode.Connections.First(n => n.Target.NodeName == con.Target.NodeName).Distance
                            > (con.Distance + resultNode.Connections.First(n => n.Target.NodeName == node.NodeName).Distance))
                            {
                            resultNode.Connections.First(n => n.Target.NodeName == con.Target.NodeName).Distance
                                = con.Distance + resultNode.Connections.First(n => n.Target.NodeName == node.NodeName).Distance;
                            }
                    }
                }
            }

            // Возвращаем точку со всеми просчитанными путями
            return resultNode;
        }
    }
}
