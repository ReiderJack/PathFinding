using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijkstras
{
    public class NodeConnection
    {
        // Точка до которой ведет связь
        public Node Target { get; private set; }

        //  Дистанция до точки
        public double Distance { get; set; }

        /// <summary>
        /// Конструктор связи
        /// </summary>
        /// <param name="target"> Точка до которой ведет </param>
        /// <param name="distance"> Дистанция до точки </param>
        public NodeConnection(Node target, double distance)
        {
            Target = target;
            Distance = distance;
        }
    }
}
