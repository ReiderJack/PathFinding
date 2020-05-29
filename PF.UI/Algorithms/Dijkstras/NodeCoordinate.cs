using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Algorithms.Dijkstras
{
    public class NodeCoordinate
    {
        // Точка для круга 
        public Node Node { get; set; }

        // Координаты
        public float X { get; set; }
        public float Y { get; set; }

        // Двигает точку отчета в середину круга
        public Thickness Offset { get; set; }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public NodeCoordinate()
        {

        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="newNode"> Точка </param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public NodeCoordinate(Node newNode, float x, float y)
        {
            Node = newNode;
            X = x;
            Y = y;

            // Расчет новой координаты для точки отчета
            Offset = new Thickness(-30 / 2, -30 / 2, 0, 0);
        }
    }
}
