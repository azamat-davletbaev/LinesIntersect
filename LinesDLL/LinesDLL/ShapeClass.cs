using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinesDLL
{
    public class ShapeClass
    {        
        public abstract class Shape
        {
            public ushort ShapeId { get; set; }
            public string Color { get; set; }
        }

        public class Point : Shape
        {            
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y) => (X, Y) = (x, y);
            public Point() { }
        }            

        public class Line : Shape
        {             
            public Point P1 { get; set; }  //LineStart
            public Point P2 { get; set; }  //LineEnd
            public Line(Point LStart, Point LEnd) => (P1, P2) = (LStart, LEnd);
            public Line(Point LStart, Point LEnd, string _color) => (P1, P2, Color) = (LStart, LEnd, _color);
            public Line() { }
            public static bool GetLineIntersect(Line LineA, Line LineB)
            {
                double zn = (LineB.P2.Y - LineB.P1.Y) * (LineA.P2.X - LineA.P1.X) - (LineB.P2.X - LineB.P1.X) * (LineA.P2.Y - LineA.P1.Y);

                double chA = (LineB.P2.X - LineB.P1.X) * (LineA.P1.Y - LineB.P1.Y) - (LineB.P2.Y - LineB.P1.Y) * (LineA.P1.X - LineB.P1.X);

                double chB = (LineA.P2.X - LineA.P1.X) * (LineA.P1.Y - LineB.P1.Y) - (LineA.P2.Y - LineA.P1.Y) * (LineA.P1.X - LineB.P1.X);

                if ((chA == 0) & (zn == 0) || (chB == 0) & (zn == 0)) return true;        // проверка на совпадение отрезков            
                if (((chA / zn >= 0) & (chA / zn <= 1)) & ((chB / zn >= 0) & (chB / zn <= 1)))   // проверка на пересечение отрезков
                    return true;
                else
                    return false;
            }
        }
        public class Polygon : Shape
        {
            public List<Line> Edges { get; set; }
            public ushort VertexCount { get; set; }
            public Polygon(List<Line> _edges) => (Edges) = (_edges);
            public Polygon() {
                Edges = new List<Line>();
            }            
        }

        
        public static bool GetPolygonIntersectLine(Polygon pol, Line ln)
        {
          if ((ln.P1.X > pol.Edges[0].P1.X) & (ln.P2.X < pol.Edges[1].P2.X) &
             (ln.P1.Y > pol.Edges[0].P1.Y) & (ln.P2.Y < pol.Edges[1].P2.Y)) 
            { 
                return true; 
            }

             foreach (Line ll in pol.Edges)
             {
               if (Line.GetLineIntersect(ln, ll)) return true;
             }
            return false;
        }
    }
}
