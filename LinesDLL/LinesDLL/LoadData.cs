using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LinesDLL;
using System.IO;

namespace LinesDLL
{
    public static class LoadData
    {
        static List<string> FileList = ReadFile(@"C:\Temp\LinesIntersect\data.txt");
        static public List<string> ReadFile(string fileName)
        {
            int counter = 0;
            string line;
            List<string> rez = new List<string>();

            StreamReader file = new StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
            {
                rez.Add(line);
                counter++;
            }
            file.Close();
            return rez;
        }

        public static List<ShapeClass.Line> GetLines()
        {
            List<ShapeClass.Line> LinesList = new List<ShapeClass.Line>();
            List<ShapeClass.Point> PointList = new List<ShapeClass.Point>();
            int i = 0; int j = 0;
            bool rd = false;

            foreach (string ln in FileList)
            {
                j++;
                if (ln == "") continue;

                if ((i < FileList.Count) & (ln != "Line") & (ln != "polygon") & rd)
                {
                    double x = Convert.ToDouble(ln.Substring(0, ln.IndexOf(",")));
                    double y = Convert.ToDouble(ln.Substring(ln.IndexOf(","), ln.Length - 2).Trim(new char[] { ',', ';' }));
                    PointList.Add(new ShapeClass.Point(x, y));                    
                }

                if ((ln == "Line") || (j == FileList.Count))
                {
                    rd = true;

                    foreach (var p in PointList)
                    {
                        if (i < PointList.Count() - 1)
                        {
                            LinesList.Add(new ShapeClass.Line(p, PointList[i + 1], "DimGray"));
                        }
                        i++;
                    }
                    PointList.Clear();
                    i = 0;
                };
            }
            // проверка на пересечение линий полигона
            ShapeClass.Polygon pol2 = GetPolygon();
            foreach (ShapeClass.Line ln in LinesList)
            {
                if (ShapeClass.GetPolygonIntersectLine(pol2, ln))
                {
                    ln.Color = "Red";
                }
            }
            return LinesList;
        }
        public static ShapeClass.Polygon GetPolygon() 
        {
            ShapeClass.Polygon pol = new ShapeClass.Polygon();
            pol.Color = "Green";            
            List<ShapeClass.Point> PointList = new List<ShapeClass.Point>();
            int i = 0;
            bool rd = false;

            foreach (string ln in FileList)
            {
                if (ln == "polygon")
                {
                    rd = true;                                        
                };

                if ((i < FileList.Count - 1) & (ln != "Line") & (ln != "polygon") & rd)
                {
                    double x = Convert.ToDouble(ln.Substring(0, ln.IndexOf(",")));
                    double y = Convert.ToDouble(ln.Substring(ln.IndexOf(","), ln.Length - 2).Trim(new char[] { ',', ';' }));

                    PointList.Add(new ShapeClass.Point(x, y));
                }
                if (ln == "Line")
                {
                    break;
                };
            }
            i = 0;
            foreach (var p in PointList)
            {
                if (i < PointList.Count() - 1)
                {                    
                    pol.Edges.Add(new ShapeClass.Line(p, PointList[i + 1], pol.Color));                    
                }                
                i++;
            }
            pol.Edges.Add(new ShapeClass.Line(PointList[PointList.Count - 1], PointList[0], pol.Color));

            return pol;
        }




    }
}