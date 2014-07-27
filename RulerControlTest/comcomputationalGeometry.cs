//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using System.Windows.Threading;

//namespace ConvexHullAlgorithm
//{

//    public enum TwoLines { COLLINEAR, PARALLEL, SKEW, SKEW_CROSS, SKEW_NO_CROSS };
//    public enum Rotation { CLOCKWISE, COUNTERCLOCKWISE };
//    public enum TopologyOrientation { LEFT, RIGHT, BEHIND, BEYOND, ORIGIN, DESTINATION, BETWEEN };

//    // BU UÇ CLASS I YAZALIM BERABER .........
//    public class PointByMe
//    {

//        public double x;
//        public double y;
//        override public string ToString()
//        {
//            string s =
//                    " " +
//                    "x : " + this.x + " " +
//                    "y : " + this.y + "\n" +
//                    "length : " + this.length() + "\n" +
//                    "polarAngle: " + this.polarAngle();
//            //+"\n"+"------------------";
//            return s;
//        }

//        public PointByMe(double _x, double _y)
//        {
//            x = _x;
//            y = _y;
//        }
//        public PointByMe(PointByMe p)
//        {
//            this.x = p.x;
//            this.y = p.y;
//        }
//        public static PointByMe operator +(PointByMe obj1, PointByMe obj2)
//        {
//            return (new PointByMe(obj1.x + obj2.x, obj1.y + obj2.y));
//        }

//        public static PointByMe operator -(PointByMe obj1, PointByMe obj2)
//        {
//            return (new PointByMe(obj1.x - obj2.x, obj1.y - obj2.y));
//        }

//        public static PointByMe operator *(double obj1, PointByMe obj2)
//        {
//            return (new PointByMe(obj1 * obj2.x, obj1 * obj2.y));
//        }

//        public static Boolean operator ==(PointByMe a, PointByMe b)
//        {
//            return ((a.x == b.x && a.y == b.y));

//        }
//        public static Boolean operator !=(PointByMe a, PointByMe b)
//        {
//            return !(a == b);
//        }

//        public static bool operator <(PointByMe a, PointByMe b)
//        {
//            return (
//                (a.x < b.x && a.y < b.y)
//                );
//        }
//        public static bool operator >(PointByMe a, PointByMe b)
//        {
//            return (
//                (a.x > b.x && a.y > b.y)
//                );
//        }
//        public int CompareTo(PointByMe obj)
//        {

//            if (this < obj)
//                return -1;
//            else if (this == obj)
//                return 0;
//            else return 1;
//            //throw new NotImplementedException();
//        }
//        public TopologyOrientation classify(PointByMe p0, PointByMe p1)
//        {
//            PointByMe p2 = this;
//            PointByMe a = p1 - p0;
//            PointByMe b = p2 - p0;
//            double SignedArea = a.x * b.y - b.x * a.y;
//            if (SignedArea > 0.0)
//                return TopologyOrientation.LEFT;
//            else if (SignedArea < 0.0)
//                return TopologyOrientation.RIGHT;
//            else if (a.x * b.x < 0.0 || (a.y * b.y < 0.0))  // zıt işaretli ters yönde , biri eşit olabilir
//                return TopologyOrientation.BEHIND;
//            else if (a.length() < b.length())
//                return TopologyOrientation.BEYOND;
//            else if (p0 == p2)
//                return TopologyOrientation.ORIGIN;
//            else if (p1 == p2)
//                return TopologyOrientation.DESTINATION;
//            else
//                return TopologyOrientation.BETWEEN;


//        }
//        public TopologyOrientation classify(Edge e)
//        {
//            return classify(e.org, e.dest);
//            //return TopologyOrientation.Left;
//        }
//        public double polarAngle()
//        {
//            if ((x == 0.0) && (y == 0.0))
//                return -1.0;
//            else if (x == 0.0)
//                return ((y > 0.0) ? 90 : 270);
//            double theta = Math.Atan(y / x);    // in radians
//            theta *= 360 / (2 * Math.PI);       // convert to degrees
//            if (x > 0.0)   // quadrants 1 and 4
//                return ((y >= 0.0) ? theta : 360 + theta);
//            else // quadrants 2 and 3
//                return (180 + theta);
//        }
//        public double length()
//        {
//            return Math.Sqrt(x * x + y * y);
//        }
//        public double distance(Edge obj1)
//        {
//            return 0.0;
//        }
//        public override int GetHashCode()
//        {
//            return base.GetHashCode();
//        }
//        public override bool Equals(object obj)
//        {
//            return base.Equals(obj);
//        }
//        //public override string ToString()
//        //{
//        //    return base.ToString();
//        //}



//    }
//    public class Vertex : PointByMe
//    {
//        private LinkedListNode<Vertex> current;
//        static public LinkedList<Vertex> list;


//        public Vertex v()
//        {

//            return current.Value;
//        }
//        public Vertex insert(PointByMe point)
//        {
//            if (list == null)
//            {

//                current = new LinkedListNode<Vertex>(new Vertex(point));
//                list = new LinkedList<Vertex>();
//                list.AddFirst(current);
//                return current.Value;

//            }
//            else
//            {

//                current.List.AddAfter(current, new Vertex(point));
//                return current.Next.Value;
//            }


//        }


//        public Vertex(double x, double y)
//            : base(x, y)
//        {

//        }


//        public Vertex(PointByMe point)
//            : base(point)
//        {

//        }

//        public PointByMe point()
//        {
//            return this;
//        }
//        public Vertex remove()
//        {
//            current = current.Next;
//            Vertex temp = current.Previous.Value;
//            list.Remove(current.Previous);

//            return temp;
//        }

//        public Vertex split(Vertex b)
//        {
//            Vertex v = cw();
//            while (v != b)
//            {
//                list.Remove(list.Find(v));
//                v = cw();
//            }
//            return current.Value;
//        }
//        public Vertex neighbor(Rotation rotation)
//        {
//            return ((rotation == Rotation.CLOCKWISE) ? cw() : ccw());
//        }
//        public Vertex cw()
//        {
//            if (current.Next == null)
//            {
//                return list.First.Value;

//            }

//            return current.Next.Value;
//        }
//        public Vertex ccw()
//        {
//            if (current.Previous == null)
//            {
//                return list.Last.Value;

//            }
//            return current.Previous.Value;
//        }

//        public Vertex advance(Rotation rotation)
//        {
//            if (rotation == Rotation.CLOCKWISE)
//            {
//                if (current.Next != null)
//                {
//                    current = current.Next;

//                }
//                else
//                {
//                    current = current.List.First;
//                }
//            }
//            else
//            {
//                if (current.Previous != null)
//                {
//                    current = current.Previous;

//                }
//                else
//                {
//                    current = current.List.Last;
//                }
//            }
//            return current.Value;

//        }



//        internal void find(Vertex vertex)
//        {
//            current = list.Find(vertex);
//        }
//    }
//    public class Edge
//    {
//        public PointByMe org;
//        public PointByMe dest;
//        public Edge(PointByMe _org, PointByMe _dest)
//        {
//            org = _org;
//            dest = _dest;
//        }
//        public Edge()
//        {
//            org = new PointByMe(0.0, 0.0);
//            dest = new PointByMe(1.0, 0.0);
//        }
//        public Edge rot()
//        {
//            PointByMe m = 0.5 * (org + dest);
//            PointByMe v = dest - org;
//            PointByMe n = new PointByMe(v.y, -(v.x));
//            org = m - 0.5 * n;
//            dest = m + 0.5 * n;
//            return this;
//        }
//        public Edge flip()
//        {
//            return this.rot().rot();
//        }
//        PointByMe point(double t)
//        {
//            return (new PointByMe(org + t * (dest - org)));
//        }
//        TwoLines intersect(Edge e, ref double t)
//        {
//            PointByMe a = org;
//            PointByMe b = dest;
//            PointByMe c = e.org;
//            PointByMe d = e.dest;
//            PointByMe n = new PointByMe((d - c).y, (c - d).x);
//            double denom = n.x * (b - a).x + n.y * (b - a).y;         //dotProduct(n, b-a);
//            if (denom == 0.0)
//            {
//                TopologyOrientation aclass = org.classify(e);
//                if ((aclass == TopologyOrientation.LEFT) || (aclass == TopologyOrientation.RIGHT))
//                    return TwoLines.PARALLEL;
//                else
//                    return TwoLines.COLLINEAR;
//            }
//            double num = n.x * (a - c).x + n.y * (a - c).y;         //dotProduct(n, b-a);
//            t = -num / denom;
//            return TwoLines.SKEW;
//        }

//        //int cross(Edge&, double&);
//        public bool isVertical()
//        {
//            return (org.x == dest.x);
//        }
//        double slope()
//        {
//            if (org.x != dest.x)
//                return (dest.y - org.y) / (dest.x - org.x);
//            return Double.MaxValue;
//        }
//        double y(double x)
//        {
//            return slope() * (x - org.x) + org.y;
//        }

//    }
//    public class Polygon
//    {

//        Vertex _v;

//        public Polygon()
//        {
//            _v = new Vertex(0, 0);

//        }

//        public Vertex v()
//        {
//            return _v.v();
//        }
//        public int size()
//        {
//            return Vertex.list.Count;
//        }
//        public PointByMe point()
//        {
//            return v();
//        }

//        public Edge edge()
//        {
//            return new Edge(_v.point(), _v.cw());
//        }
//        public Vertex cw()
//        {
//            return _v.cw();
//        }
//        public Vertex ccw()
//        {
//            return _v.ccw();
//        }
//        public Vertex advance(Rotation rotation)
//        {
//            return _v.advance(rotation);
//        }
//        public Vertex neighbor(Rotation rotation)
//        {
//            return _v.neighbor(rotation);
//        }
//        public Vertex insert(PointByMe point)
//        {

//            return _v.insert(point);
//        }
//        public Vertex remove()
//        {
//            return _v.remove();
//        }
//        public Vertex setV(Vertex vertex)
//        {
//            _v.find(vertex);
//            return vertex;
//        }
//        public Vertex split(Vertex vertex)
//        {
//            return _v.split(vertex);
//        }
//    }
//    public static class utility
//    {
//        static PointByMe originPt;
//        public static System.Windows.Shapes.Rectangle getRectangle(PointByMe p, PointByMe org)
//        {
//            //x = myList.ElementAt(selected_list_index_current).x + origin.x;  //(double)Convert.ToInt32(e.GetPosition(myCanvas).X);
//            //y = -(myList.ElementAt(selected_list_index_current).y) + origin.y;  //(double)Convert.ToInt32(e.GetPosition(myCanvas).Y);
//            double x = p.x + org.x;
//            double y = p.y + org.y;

//            Rectangle myRectangle = new Rectangle();

//            myRectangle.Width = 10;
//            myRectangle.Height = 10;
//            myRectangle.Fill = new SolidColorBrush(Colors.Red);
//            //myRectangle.ToolTip = myList.ElementAt(selected_list_index_current).ToString();
//            //myCanvas.Children.Add(myRectangle);
//            //Canvas.SetLeft(myRectangle, x);
//            //Canvas.SetTop(myRectangle, y);
//            return myRectangle;
//        }

//        public static PointByMe reversePoint(PointByMe point)
//        {
//            point.y = -point.y;
//            point = point + MainWindow.origin;
//            return new PointByMe(point.x, point.y);
//        }

//        public static PointByMe leastPoint(PointByMe org, System.Collections.Generic.List<PointByMe> l)
//        {
//            PointByMe bestP = l.ElementAt(1);
//            PointByMe testP = new PointByMe(0, 0);
//            for (int i = 1; i < l.Count; i++)
//            {
//                testP = l.ElementAt(i);
//                //if (bestP == testP) continue;
//                if (
//                        (testP - org).length() < (bestP - org).length()
//                    )
//                    bestP = testP;
//            }
//            return bestP;
//        }

//        public static bool pointInConvexPolygon(PointByMe s, Polygon p)    // Edge sınıfına ihtiyaç var
//        {
//            //throw new InvalidOperationException("Implement edilmedi");
//            if (p.size() == 1) return (s == p.point());
//            if (p.size() == 2)
//            {
//                TopologyOrientation c = s.classify(p.edge());
//                return ((c == TopologyOrientation.BETWEEN) || (c == TopologyOrientation.ORIGIN) | (c == TopologyOrientation.DESTINATION));
//            }
//            Vertex org = p.v();
//            for (int i = 0; i < p.size(); i++, p.advance(Rotation.CLOCKWISE))
//            {
//                if (s.classify(p.edge()) == TopologyOrientation.LEFT)
//                {
//                    p.setV(org);
//                    return false;
//                }
//            }
//            return true;
//        }

//        static PointByMe somePoint;
//        //public static Polygon insertionHull(List<PointByMe> s)
//        //{
//        //    List<PointByMe> myListG = new List<PointByMe>();
//        //    Polygon p = new Polygon();
//        //    p.insert(s.ElementAt(0));
//        //    for (int i = 1; i < s.Count; i++)
//        //    {
//        //        if (pointInConvexPolygon(s.ElementAt(i), p))
//        //        {
//        //            continue;
//        //        }
//        //        somePoint = s.ElementAt(i);
//        //        leastVertex(p, closestToPolygonCmp(s.ElementAt(i), p.v().point()));
//        //        supportingLine(s.ElementAt(i), p, TopologyOrientation.LEFT);
//        //        Vertex l = p.v();
//        //        supportingLine(s.ElementAt(i), p, TopologyOrientation.RIGHT);
//        //        p.insert(s.ElementAt(i));
//        //    }
//        //    return p;
//        //}
//        public static void supportingLine(PointByMe s, Polygon p, TopologyOrientation side)
//        {
//            Rotation rotation;
//            if (side == ConvexHullAlgorithm.TopologyOrientation.LEFT)
//                rotation = Rotation.CLOCKWISE;
//            else
//                rotation = Rotation.COUNTERCLOCKWISE;
//            //TopologyOrientation rotation = (side == TopologyOrientation.LEFT) ? Rotation.CLOCKWISE : Rotation.COUNTERCLOCKWISE;
//            //int rotation = (side == (int)TopologyOrientation.LEFT) ? (int)Rotation.CLOCKWISE : (int)Rotation.COUNTERCLOCKWISE;
//            Vertex av = p.v();
//            PointByMe ap = p.point();
//            Vertex bv = p.neighbor((Rotation)rotation);
//            PointByMe bp = p.neighbor((Rotation)rotation).point();
//            TopologyOrientation c = bp.classify(s, ap);
//            while ((c == side) || (c == TopologyOrientation.BEYOND) || (c == TopologyOrientation.BETWEEN))
//            {
//                p.advance((Rotation)rotation);
//                av = p.v();
//                ap = p.point();
//                bv = p.neighbor((Rotation)rotation);
//                bp = p.neighbor((Rotation)rotation).point();
//                c = bp.classify(s, ap);
//            }
//        }

//        static int polarCmp(PointByMe p, PointByMe q)
//        {
//            PointByMe vp = p - originPt;
//            PointByMe vq = q - originPt;
//            double pPolar = vp.polarAngle();
//            double qPolar = vq.polarAngle();
//            if (pPolar < qPolar) return -1;
//            if (pPolar > qPolar) return 1;
//            if (vp.length() < vq.length()) return -1;
//            if (vp.length() > vq.length()) return 1;
//            return 0;
//        }

//        public static Vertex leastVertex(Polygon p1)
//        {
//            Vertex bestV = p1.v();
//            p1.advance(Rotation.CLOCKWISE);
//            for (int i = 1; i < p1.size(); p1.advance(Rotation.CLOCKWISE), i++)
//            {
//                if (p1.v().point().polarAngle() < bestV.point().polarAngle())
//                {
//                    bestV = p1.v();
//                }
//            }
//            p1.setV(bestV);
//            return bestV;
//        }

//        //public static double closestToPolygonCmp(PointByMe a, PointByMe b)
//        //{
//        //    //double distA = (somePoint - a).length();
//        //    //double distB = (somePoint - b).length();
//        //    //if (distA < distB) return -1;
//        //    //else if (distA > distB) return 1;
//        //    //return 0;

//        //    double l = Math.Sqrt(Math.Pow((a.x - b.x), 2) + Math.Pow((a.y - b.y), 2));
//        //    return l;
//        //}

//        //internal static void pointToConvexPolygon(Polygon myPoligon, LinkedList<PointByMe> sortedPolygon, LinkedList<Ellipse> sortedEllipseList, Dispatcher dis, System.Windows.Controls.Canvas canvas)
//        //{
//        //    List<PointByMe> list = new List<PointByMe>();
//        //    List<Line> lineList = new List<Line>();
//        //    #region ilk 3 nokta ile polygon
//        //    for (int i = 0; i < 3; i++)
//        //    {
//        //        list.Add(sortedPolygon.ElementAt(i));
//        //        dis.BeginInvoke(new Action(() =>
//        //        { sortedEllipseList.ElementAt(i).Fill = new SolidColorBrush(Colors.Red); }));
//        //        Thread.Sleep(1000);

//        //        dis.BeginInvoke(new Action(() =>
//        //        {
//        //            Line l = new Line();
//        //            PointByMe p1 = new PointByMe(list.ElementAt(0).x, list.ElementAt(0).y);
//        //            PointByMe p2 = new PointByMe(list.ElementAt(i - 1).x, list.ElementAt(i - 1).y);
//        //            p1 = utility.reversePoint(p1);
//        //            p2 = utility.reversePoint(p2);
//        //            l.X1 = p1.x;
//        //            l.Y1 = p1.y;
//        //            l.X2 = p2.x;
//        //            l.Y2 = p2.y;
//        //            l.VerticalAlignment = System.Windows.VerticalAlignment.Center;
//        //            l.Stroke = System.Windows.Media.Brushes.LightYellow;

//        //            l.StrokeThickness = 2;
//        //            canvas.Children.Add(l);
//        //            lineList.Add(l);
//        //        }));
//        //    }
//        //    Thread.Sleep(1000);
//        //    dis.BeginInvoke(new Action(() =>
//        //    {
//        //        Line l = new Line();
//        //        PointByMe p1 = new PointByMe(list.ElementAt(2).x, list.ElementAt(2).y);
//        //        PointByMe p2 = new PointByMe(list.ElementAt(1).x, list.ElementAt(1).y);
//        //        p1 = utility.reversePoint(p1);
//        //        p2 = utility.reversePoint(p2);
//        //        l.X1 = p1.x;
//        //        l.Y1 = p1.y;
//        //        l.X2 = p2.x;
//        //        l.Y2 = p2.y;
//        //        l.VerticalAlignment = System.Windows.VerticalAlignment.Center;
//        //        l.Stroke = System.Windows.Media.Brushes.LightYellow;

//        //        l.StrokeThickness = 2;
//        //        canvas.Children.Add(l);
//        //        lineList.Add(l);
//        //    }));
//        //    Thread.Sleep(1000);
//        //    #endregion

//        //    for (int i = 3; i < myPoligon.size(); i++)
//        //    {
//        //        list.Add(sortedPolygon.ElementAt(i));
//        //    }
//        //    //for (int i = 0; i < list.Count; i++)
//        //    //{
//        //    //    if (pointInConvexPolygon(list.ElementAt(i), myPoligon))
//        //    //    {
//        //    //        continue;
//        //    //    }
//        //    //    else
//        //    //    {
//        //    //        somePoint = list.ElementAt(i);
//        //    //        leastVertex(myPoligon, closestToPolygonCmp(list.ElementAt(i), myPoligon.v().point()));
//        //    //        supportingLine(list.ElementAt(i), myPoligon, (int)TopologyOrientation.LEFT);
//        //    //        Vertex l = myPoligon.v();
//        //    //        supportingLine(list.ElementAt(i), myPoligon, (int)TopologyOrientation.RIGHT);
//        //    //        myPoligon.insert(list.ElementAt(i));
//        //    //    }
//        //    //}
//        //    for (int i = 3; i < list.Count(); i++)
//        //    {
//        //        for (int j = 0; j < i; j++)
//        //        {
//        //            PointByMe point = list.ElementAt(i);
//        //            PointByMe po = list.ElementAt(j);
//        //            PointByMe po1 = list.ElementAt(j + 1);
//        //            PointByMe enYakin = po;
//        //            if (!pointInConvexPolygon(point, myPoligon))
//        //            {
//        //                if (closestToPolygonCmp(point, po) > closestToPolygonCmp(point, po1))
//        //                {
//        //                    enYakin = po1;
//        //                    int temp = pointIndex(list.ElementAt(i), sortedPolygon);
//        //                    Thread.Sleep(1000);
//        //                    dis.BeginInvoke(new Action(() =>
//        //                    { sortedEllipseList.ElementAt(temp).Fill = new SolidColorBrush(Colors.Green); }));
//        //                    Thread.Sleep(1000);
//        //                    dis.BeginInvoke(new Action(() =>
//        //                    {
//        //                        Line myLine = new Line();
//        //                        PointByMe point1 = new PointByMe(point.x, point.y);
//        //                        PointByMe point2 = new PointByMe(enYakin.x, enYakin.y);
//        //                        point1 = utility.reversePoint(point1);
//        //                        point2 = utility.reversePoint(point2);
//        //                        myLine.X1 = point1.x;
//        //                        myLine.Y1 = point1.y;
//        //                        myLine.X2 = point2.x;
//        //                        myLine.Y2 = point2.y;
//        //                        myLine.VerticalAlignment = System.Windows.VerticalAlignment.Center;
//        //                        myLine.Stroke = System.Windows.Media.Brushes.LightYellow;

//        //                        myLine.StrokeThickness = 2;
//        //                        canvas.Children.Add(myLine);

//        //                        lineList.Add(myLine);
//        //                        canvas.Children.Remove(lineList.ElementAt(i));

//        //                    }));
//        //                    Thread.Sleep(1000);
//        //                    dis.BeginInvoke(new Action(() =>
//        //                    { sortedEllipseList.ElementAt(temp).Fill = new SolidColorBrush(Colors.Red); }));
//        //                    //list.RemoveAt(0);
//        //                }
//        //            }
//        //        }
//        //    }
//        //}
//        public static int pointIndex(PointByMe point, LinkedList<PointByMe> list)
//        {
//            for (int i = 0; i < list.Count; i++)
//            {
//                if (list.ElementAt(i) == point)
//                {
//                    return i;
//                }
//            }
//            return -1;
//        }
//    }
//}
