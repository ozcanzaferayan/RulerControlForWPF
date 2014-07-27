using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using CG;


namespace ConvexHullAlgorithm
{
    class derya
    {
       
        public static List<Line> myLineList = new List<Line>();
        public static List<Ellipse> myEllipseList = new List<Ellipse>();

        public static void drawPoints(PointByMe testPoint, List<PointByMe> pnts,System.Windows.Controls.Canvas canvas)
        {
            canvas.Children.Clear();
            if (myEllipseList.Count > 0)
            {
                foreach (Ellipse e in myEllipseList)
                    canvas.Children.Remove(e);
                myEllipseList.Clear();                
            }

            foreach(PointByMe p in pnts)
            {
                Ellipse e=new Ellipse();
                e.Width=10;
                e.Height=10;
                e.Fill=Brushes.White;
                Canvas.SetLeft(e, p.x);
                Canvas.SetTop(e, -p.y);
                canvas.Children.Add(e);
                myEllipseList.Add(e);
            }
  
            Ellipse e1 = new Ellipse();
            e1.Width = 10;
            e1.Height = 10;
            e1.Fill = Brushes.Red;
            Canvas.SetLeft(e1, testPoint.x);
            Canvas.SetTop(e1, -testPoint.y);
            canvas.Children.Add(e1);
            myEllipseList.Add(e1);             
        }
        public static void drawPoligon(CG.Polygon mP,System.Windows.Controls.Canvas canvas){

            if (myLineList.Count > 0)
            {
                foreach (Line l in myLineList)
                    canvas.Children.Remove(l);
                myLineList.Clear();
            }
           // MessageBox.Show("önceki poligon nokta sayısı : " + derya.myLineList.Count + " şu anki poligon nokta sayısı : " + mP.size());
            PointByMe p1;// = new PointByMe(p.v().point().x, p.v().point().y);
             PointByMe p2;// = new PointByMe(p.v().cw().point().x, p.cw().point().y);
             
             for (int i = 0; i < mP.size()+1; i++,mP.cw())  // neden +1
             {
                    Line line = new Line();         
                    p1 = mP.point();        //, p.v().point().y);
                    p2 = mP.v().cw().point();   //, p.cw().point().y);
                    p1 = utility.reversePoint(p1);
                    p2 = utility.reversePoint(p2);
                    line.X1 = p1.x;
                    line.Y1 =Math.Abs(p1.y);
                    line.X2 = p2.x;
                    line.Y2 =Math.Abs(p2.y);
                    line.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    line.Stroke = System.Windows.Media.Brushes.LightYellow;

                    line.StrokeThickness = 2;
                    canvas.Children.Add(line);
                    myLineList.Add(line);
                 
            }
             //MessageBox.Show("çizilen poligon doğru mu !!!");  
     }
        public static string poligonListString(CG.Polygon myPoligon)
        {

            string s = "poligon size : " + myPoligon.size();
            s += "\n noktalar : \n**";

            for (int i = 0; i < myPoligon.size(); i++, myPoligon.cw())
                s += myPoligon.point().x + "**";
            return s;
            //MessageBox.Show(s);
        }
        public static void poligonList(CG.Polygon myPoligon)
        {
            
            string s = "poligon size : " + myPoligon.size();
            s += "\n noktalar : \n**";

            for (int i = 0; i < myPoligon.size(); i++, myPoligon.cw())
                s += myPoligon.point().x + "**";

            MessageBox.Show(s);
        }
        public static int tegetBul_Right(List<PointByMe> pl, PointByMe po)
        {
            double d = double.MaxValue;
            PointByMe closestPoint = new PointByMe(0, 0);
            foreach (PointByMe temp in pl)
            {
                if ((temp - po).length() < d) closestPoint = temp;
            }
            foreach (PointByMe temp in pl)
            {
                Edge e = new Edge(temp, po);
                bool testStatus = true;
                foreach (PointByMe temp1 in pl)
                {
                    if (temp1.classify(e) == TopologyOrientation.LEFT) testStatus = false;
                }
                if (testStatus == true) return pl.IndexOf(temp);

            }
            return pl.Count - 1;
        }
        public static int tegetBul_Left(List<PointByMe> pl, PointByMe po)
        {
            double d = double.MaxValue;
            PointByMe closestPoint = new PointByMe(0, 0);
            foreach (PointByMe temp in pl)
            {
                if ((temp - po).length() < d) closestPoint = temp;
            }
            foreach (PointByMe temp in pl)
            {
                Edge e = new Edge(temp, po);
                bool testStatus = true;
                foreach (PointByMe temp1 in pl)
                {
                    if (temp1.classify(e) == TopologyOrientation.RIGHT) testStatus = false;
                }
                if (testStatus == true) return pl.IndexOf(temp);
            }
            return pl.Count - 1;
        }
        public static List<PointByMe> teget_Delete(List<PointByMe> pl, int l, int r)
        {
            List<PointByMe> newPoligon = new List<PointByMe>();
            foreach (PointByMe t in pl)
            {
                newPoligon.Add(t);
            }
            newPoligon.RemoveRange(l + 1, r - 1);
            return newPoligon;
        }
        //public static bool pointInConvexPolygon(PointByMe po, List<PointByMe> pl)
        //{
        //    bool status = true;
        //    for (int i = 0; i < pl.Count - 1; i++)
        //    {
        //        Edge e = new Edge(pl[i], pl[i + 1]);
        //        if (po.classify(e) == TopologyOrientation.LEFT)
        //            return false;

        //    }
        //    return status;
        //}
        //public static List<PointByMe> convex_Uret(List<PointByMe> pl, LinkedList<Ellipse> sortedEllipseList, Dispatcher dis, System.Windows.Controls.Canvas canvas)
        //{
        //    List<PointByMe> newPoligon = new List<PointByMe>();
        //    newPoligon.Add(pl[0]);
        //    newPoligon.Add(pl[1]);

        //    for (int i = 0; i < 2; i++)
        //    {
        //        dis.BeginInvoke(new Action(() =>
        //        { sortedEllipseList.ElementAt(i).Fill = new SolidColorBrush(Colors.Red); }));
        //        Thread.Sleep(500);
        //    }
        //    dis.BeginInvoke(new Action(() =>
        //    {
        //        Line l = new Line();
        //        PointByMe p1 = new PointByMe(newPoligon.ElementAt(0).x, newPoligon.ElementAt(0).y);
        //        PointByMe p2 = new PointByMe(newPoligon.ElementAt(1).x, newPoligon.ElementAt(1).y);
        //        p1 = utility.reversePoint(p1);
        //        p2 = utility.reversePoint(p2);
        //        l.X1 = p1.x;
        //        l.Y1 = p1.y;
        //        l.X2 = p2.x;
        //        l.Y2 = p2.y;
        //        l.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //        l.Stroke = System.Windows.Media.Brushes.LightYellow;

        //        l.StrokeThickness = 2;
        //        canvas.Children.Add(l);
        //    }));
        //    Thread.Sleep(500);

        //    int l_teget = 0, r_teget = 0;
        //    //for(int i=2;<)
        //    for (int i = 2; i < pl.Count; i++)
        //    {
        //        PointByMe point = pl[i];
        //        if (pointInConvexPolygon(pl[i], newPoligon) == true) continue;
        //        else
        //        {

        //            for (int i1 = 2; i1 < pl.Count; i1++)

        //                l_teget = tegetBul_Left(newPoligon, pl[i1]);

        //            for (int i2 = 2; i2 < pl.Count; i2++)
        //                r_teget = tegetBul_Right(newPoligon, pl[i2]);

        //            Thread.Sleep(500);
        //            dis.BeginInvoke(new Action(() =>
        //            { sortedEllipseList.ElementAt(i).Fill = new SolidColorBrush(Colors.Green); }));
        //            Thread.Sleep(500);
        //            dis.BeginInvoke(new Action(() =>
        //            {
        //                Line l = new Line();
        //                PointByMe p1 = new PointByMe(point.x, point.y);
        //                PointByMe p2 = new PointByMe(pl[l_teget].x, pl[l_teget].y);
        //                p1 = utility.reversePoint(p1);
        //                p2 = utility.reversePoint(p2);
        //                l.X1 = p1.x;
        //                l.Y1 = p1.y;
        //                l.X2 = p2.x;
        //                l.Y2 = p2.y;
        //                l.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //                l.Stroke = System.Windows.Media.Brushes.LightYellow;

        //                l.StrokeThickness = 2;
        //                canvas.Children.Add(l);
        //            }));
        //            Thread.Sleep(500);
        //            dis.BeginInvoke(new Action(() =>
        //            { sortedEllipseList.ElementAt(i).Fill = new SolidColorBrush(Colors.Red); }));
        //            Thread.Sleep(500);
        //            dis.BeginInvoke(new Action(() =>
        //            {
        //                Line l = new Line();
        //                PointByMe p1 = new PointByMe(point.x, point.y);
        //                PointByMe p2 = new PointByMe(pl[r_teget].x, pl[r_teget].y);
        //                p1 = utility.reversePoint(p1);
        //                p2 = utility.reversePoint(p2);
        //                l.X1 = p1.x;
        //                l.Y1 = p1.y;
        //                l.X2 = p2.x;
        //                l.Y2 = p2.y;
        //                l.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //                l.Stroke = System.Windows.Media.Brushes.LightYellow;

        //                l.StrokeThickness = 2;
        //                canvas.Children.Add(l);
        //            }));
        //            Thread.Sleep(500);

        //            newPoligon.Add(pl[i]);
        //            //newPoligon = teget_Delete(newPoligon, l_teget, r_teget);
        //        }

        //    }
        //    return newPoligon;
        //}

        public static CG.Polygon convex_Uret(CG.Polygon mP, Dispatcher dis, System.Windows.Controls.Canvas canvas)
        {
            PointByMe somePoint = new PointByMe(0, 0);
            List<PointByMe> s = new List<PointByMe>();
            CG.Polygon p = new CG.Polygon();
            
            
           //mP.advance(Rotation.COUNTERCLOCKWISE);
           string ss="";
            for (int i = 0; i < mP.size(); i++)
            {
                if(mP.v().point().y>0) mP.v().point().y = -mP.v().point().y;
                s.Add(mP.point());
                ss += mP.v().point().x + " ";
                mP.advance(Rotation.CLOCKWISE);
            }
            
            //MessageBox.Show(ss);
            p.insert(s[0]);//p.advance(Rotation.CLOCKWISE);
            p.insert(s[1]);//p.advance(Rotation.CLOCKWISE);
            p.insert(s[2]);//p.advance(Rotation.CLOCKWISE);
            
            drawPoligon(p, canvas);
            //poligonList(p);

            PointByMe temp1 = p.v().point();

            for (int iiii = 0; iiii < p.size(); iiii++, p.cw())
                if (p.point().y > 0) p.point().y = -p.point().y;

            for (; p.v() != temp1; p.cw()) ;

            for (int i = 3; i < s.Count; i++)
            {

                somePoint = s[i];
                canvas.Children.Clear();
                //MessageBox.Show("şu an canvas boş...");
                drawPoints(somePoint,s,canvas);
                drawPoligon(p,canvas);
                //MessageBox.Show("şu an canvas dolu mu ...");
                if (somePoint.y>0) somePoint.y = -somePoint.y;
                // new inside test
                bool isInside = true;
                temp1 = p.v().point();   // no change on active vertex
                for (int i5 = 0; i5 < p.size(); i5++, p.advance(Rotation.CLOCKWISE))
                {
                    temp1 = p.v().point();

                    for (int iiii = 0; iiii < p.size(); iiii++, p.cw())
                        if (p.point().y > 0) p.point().y = -p.point().y;

                    for (; p.v() != temp1; p.cw()) ;

                    if (s[i].classify(p.edge()) == TopologyOrientation.LEFT)   // x e simetri olduğu için tersini aldık
                    {
                        isInside = false;
                    }
                }
                for (; p.v() != temp1; p.cw()) ;
                if (isInside == true)
                {
                    MessageBox.Show("nokta içinde bulundu !!!");
                    continue;
                }
                
                MessageBox.Show("nokta içinde değil  !!!   i değeri " +i);
                //if (utility.pointInConvexPolygon(s[i], p))
                //{
                //    MessageBox.Show("nokta içeride bulundu noktanın x değeri : " + s[i].x);
                //    continue;
                //}
              
                //MessageBox.Show("incelenen nokta "+somePoint.x.ToString());
                PointByMe closestVertex = p.point();
                double minLength=(closestVertex-somePoint).length();
                double l1;

                for (int ii = 0; ii < p.size(); ii++,p.cw())
                {
                    //String sss = "şu an test değeri : " + p.v().x + " mesafe : " + (p.v().point() - somePoint).length() +
                                //"\nclosest değeri " + closestVertex.x + " \nmesafe : " + (closestVertex - somePoint).length();
                    //MessageBox.Show(sss);
                    temp1 = p.v().point();

                    for (int iiii = 0; iiii < p.size(); iiii++, p.cw())
                        if (p.point().y > 0) p.point().y = -p.point().y;

                    for(;p.v()!=temp1;p.cw());

                    
                    l1 = (p.v() - somePoint).length();
                    if (l1 < minLength)
                    {
                        closestVertex = p.v().point();
                        //string str1 = "karşılaştırılan nokta x değeri :" + somePoint.x;
                        //str1 += "\nbulunan en yakın nokta" + p.v().x;
                        //MessageBox.Show("güncelleme yapıyor...");
                        minLength = l1;
                    }
                          
                }
                temp1 = p.v().point();

                for (int iiii = 0; iiii < p.size(); iiii++, p.cw())
                    if (p.point().y > 0) p.point().y = -p.point().y;

                for (; p.v() != temp1; p.cw()) ;

                MessageBox.Show("en yakın nokta " + closestVertex.x.ToString());
                string s00 = poligonListString(p);
                string s0="incelenen nokta - en yakın nokta " + somePoint.x.ToString()+" "+closestVertex.ToString();
                //MessageBox.Show("incelenen nokta - en yakın nokta " + somePoint.x.ToString()+" "+closestVertex.point().x.ToString());
                
                while(p.v().point() != closestVertex)  // şu aktif vertex en yakın olanı
                    p.advance(Rotation.CLOCKWISE);

                string ps = "";  // poligon x değerlerini buraya alıyoruz
                string s1="şu an aktif vertex closest olmalı p.v() değeri "+" "+p.v().point().x.ToString();
                s1+="\nşu an aktif vertex closest olmalı" + " " + p.v().point().x.ToString();
                s1+="\n şu an left için taramaya başlayacak...";
                s1+="gönderilen değerler: "+s[i].x+" aktif vertex "+p.v().point().x+"poligon size:  "+p.size();
                for (int ii = 0; ii < p.size(); ii++, p.cw())
                    ps += " " + p.point().x.ToString();
                //s[i].y = -s[i].y;
                MessageBox.Show(s1+" "+ps);
                ps = "";
                utility.supportingLine(s[i], p, TopologyOrientation.LEFT);
                Vertex l = p.v();
               
                //MessageBox.Show("soldan teget değeri "+l.point().x.ToString());
                //string s2="soldan teget değeri "+l.point().x.ToString();

                string s2 ="\n şu an right için taramaya başlayacak...";
                s2 += "gönderilen değerler: ekleme noktası" + s[i].x + " aktif vertex " + p.v().point().x + " poligon size " + p.size();
                
                for (int ii = 0; ii < p.size(); ii++, p.cw())
                    ps += " " + p.point().x.ToString();
                MessageBox.Show(s2 + " \n" + "poligon içeriği : " + ps);

                    utility.supportingLine(s[i], p, TopologyOrientation.RIGHT);
                
                //MessageBox.Show("sağdan teget değeri "+l.point().x.ToString());
                string s3="sağdan teget değeri bulundu ve su an p.v() değeri de bu : "+p.point().x.ToString();
                int a, b;
                a = p.size();
                p.split(l);
                MessageBox.Show("silme yapıldı, yeni poligonn nokta sayısı : " + p.size());
                b = p.size();
                if (a < b) MessageBox.Show("silme nokta sayısı azaldı yapıldı...");

                //MessageBox.Show("eklenen değer : "+s[i].ToString());
                //string s4="eklenen değer : "+s[i].ToString();
                
                p.insert(s[i]);
                if (p.v().point().y > 0) p.v().point().y = -p.v().point().y;
                //MessageBox.Show(s00+" " + "\n" + s2 + "\n" + s3 + "\n" + s4);
                drawPoligon(p, canvas);
                temp1 = p.v().point();

                for (int iiii = 0; iiii < p.size(); iiii++, p.cw())
                    if (p.point().y > 0) p.point().y = -p.point().y;

                for (; p.v() != temp1; p.cw()) ;

            }
            return p;          
        }    
    }
}
