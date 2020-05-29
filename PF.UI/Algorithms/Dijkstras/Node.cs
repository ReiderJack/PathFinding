using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class Node
    {
        // Лист связей 
        List<NodeConnection> _connections;

        // имя точки
        public string NodeName { get; set; }

        // Дистанция от стартовой точки
        // Используется только для Дикстры
        public double DistanceFromStart { get; set; }

        // Проперти листа связей для доступа к листу
        public List<NodeConnection> Connections
        {
            get { return _connections; }
            set { _connections = value; }
        }

        // Конструктор для точки
        public Node(string name)
        {
            NodeName = name;
            _connections = new List<NodeConnection>();
        }

        /// <summary>
        /// Добавляет новую связь
        /// </summary>
        /// <param name="targetNode"> Точка до которой ведет связь</param>
        /// <param name="distance"> Дистанция </param>
        /// <param name="twoWay"> да\нет создать связь туда и обратно </param>
        public void AddConnection(Node targetNode, double distance, bool twoWay)
        {
            // Если точка до которой ведет связь не существует, ошибка
            if (targetNode == null) throw new ArgumentNullException("targetNode");
            // if (targetNode == this) throw new ArgumentException("Node may not connect to itself.");
            // if (distance <= 0) throw new ArgumentException("Distance must be positive.");

            // Добавляет связь в лист связей
            _connections.Add(new NodeConnection(targetNode, distance));

            // Создает связь туда обратно если было задано да
            if (twoWay) targetNode.AddConnection(this, distance, false);
        }

        /// <summary>
        /// Удаляет связь из списка
        /// которая ведет до заданной точки
        /// </summary>
        /// <param name="targetNode"> Заданная точка </param>
        public void RemoveConnection(Node targetNode)
        {
            //Если точка не существует ничего не делать
            if (targetNode == null) return;

            // Если точка есть в списе связей - удалить её
            if (_connections.Any(c => c.Target == targetNode))
            {
                // Удалить точку
                _connections.Remove(_connections.Find(c => c.Target == targetNode));
            }
        }

        /// <summary>
        /// Удаляет заданную в функцию связь
        /// </summary>
        /// <param name="connection"> Заданная связь </param>
        public void RemoveConnection(NodeConnection connection)
        {
            // Если не существует ничего не делать
            if (connection == null) return;

            // Если связь есть в списке связей
            if (_connections.Contains(connection))
            {
                // Удалить связь
                _connections.Remove(connection);
            }
        }
    }
}
