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
            // Places distance value on in the middle of the line
            DistanceCoordinate = new Coordinate((Point2.X + Point1.X) / 2, (Point2.Y + Point1.Y) / 2);

            // Arrow rotation
            float xDiff = Point2.X - Point1.X;
            float yDiff = Point2.Y - Point1.Y;
            Angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

            ArrowOffset = new Thickness(-14 / 2, -14 / 2, 0, 0);// 14 because of the arrow size

            // Coordinates for the arrow to be at the end of the line
            float xDiff1 = Point1.X - Point2.X;
            float yDiff1 = Point1.Y - Point2.Y;
            var Angle1 = Math.Atan2(yDiff1, xDiff1);

            var Sin = Math.Sin(Angle1) * 25;// 25 so it's not inside of the elipse
            var Cos = Math.Cos(Angle1) * 25;

            ArrowCoord = new Coordinate(Point2.X + (float)Cos, Point2.Y + (float)Sin);
        }
    }
}
