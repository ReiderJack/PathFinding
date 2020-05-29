using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Algorithms.Dijkstras
{
    public class LineData
    {
        // Начальная координата линии
        public Coordinate Point1 { get; set; }

        // Конечная координата линии
        public Coordinate Point2 { get; set; }


        // Дистанция связи
        public double ConnectionDistance { get; set; }
        // Середина линии(координата для отображения дистанции
        public Coordinate DistanceCoordinate { get; set; }

        // координата для стрелки
        public Coordinate ArrowCoord { get; set; }
        // Угол стрелки
        public double Angle { get; set; }
        // Двигает точку отчета в середину стрелки
        public Thickness ArrowOffset { get; set; }

        /// <summary>
        ///  Пустой конструктор
        /// </summary>
        public LineData()
        {

        }

        /// <summary>
        /// Конструктор информации линии
        /// </summary>
        /// <param name="point1"> Начальная точка </param>
        /// <param name="point2"> Конечная точка </param>
        /// <param name="distance"> Дистанция связи </param>
        public LineData(Coordinate point1, Coordinate point2, double distance)
        {
            // Начальная
            Point1 = point1;
            // конечная
            Point2 = point2;

            ConnectionDistance = distance;
            // Расчет середины линии для цифры дистанции на линии
            DistanceCoordinate = new Coordinate((Point2.X + Point1.X) / 2, (Point2.Y + Point1.Y) / 2);

            // Расчитывает угол для стрелки
            float xDiff = Point2.X - Point1.X;
            float yDiff = Point2.Y - Point1.Y;
            Angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

            // Поправляем точку отчета для стрелки
            ArrowOffset = new Thickness(-14 / 2, -14 / 2, 0, 0);// 14 потомучто размер стрелки

            // Расчитывает кординату для стрелки чтобы та была в конце линии
            float xDiff1 = Point1.X - Point2.X;
            float yDiff1 = Point1.Y - Point2.Y;
            var Angle1 = Math.Atan2(yDiff1, xDiff1);

            var Sin = Math.Sin(Angle1) * 25;// 25 so it's not inside of the elipse
            var Cos = Math.Cos(Angle1) * 25;

            ArrowCoord = new Coordinate(Point2.X + (float)Cos, Point2.Y + (float)Sin);
        }
    }
}
