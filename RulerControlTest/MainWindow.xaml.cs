using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CG;

namespace RulerControlTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Vector windowOrigin = new Vector(0, 0);
        ToolTip cursorToolTip;
        CG.Polygon mmP = new CG.Polygon();
        LinkedList<Ellipse> sortedEllipseList;
        LinkedList<PointByMe> sortedPolygon;
        CG.Polygon myPoligon = new CG.Polygon();
        public static PointByMe origin = new PointByMe(0, 0);
        System.Collections.Generic.List<Ellipse> ellipseList = new System.Collections.Generic.List<Ellipse>();
        
        public MainWindow()
        {
            InitializeComponent();
            cursorToolTip = new ToolTip();
            myCanvas.ToolTip = cursorToolTip;
            
        }

        #region
        private void btnPointToConvexPolygon_Click(object sender, RoutedEventArgs e)
        {
            //sortedPolygon = myPoligon.sortByxPolygon();


            //PointByMe p0 = new PointByMe(182,-291); mmP.insert(p0);
            //PointByMe p1 = new PointByMe(348,-116); mmP.insert(p1);
            //PointByMe p2 = new PointByMe(585,-191); mmP.insert(p2);
            //PointByMe p3 = new PointByMe(425,-95); mmP.insert(p3);
            //PointByMe p4 = new PointByMe(351,-276); mmP.insert(p4);

            //for(int i=0;i<mmP.size();i++,mmP.cw())
            //{
            //    PointByMe myPoint = mmP.point();
            //            Ellipse ellipse = new Ellipse();
            //            ellipse.Width = 10;
            //            ellipse.Height = 10;
            //            ellipse.ToolTip = "(" + myPoint.x + "," + myPoint.y + ")";
            //            ellipse.Fill = new SolidColorBrush(Colors.White);
            //            ellipse.StrokeThickness = 1;


            //            myPoint = myPoint - origin;
            //            myPoint.y = -(myPoint.y);
            //            ellipse.ToolTip = "(" + myPoint.x + "," + myPoint.y + ")";

            //            //myPoligon.advance(Rotation.CLOCKWISE);
            //            ////if(myPoligon!=null)
            //            //        if(myPoligon.size()>0) MessageBox.Show("şu an point x değeri :"+myPoligon.point().x.ToString());

            //            myPoligon.insert(myPoint);
            //            // cw() testi için bu döngü
            //            //for (int i = 0; i < myPoligon.size(); i++, myPoligon.cw()) //.advance(CG.Rotation.CLOCKWISE))
            //            //{
            //            //    MessageBox.Show("i:"+ i + "\n"+ myPoligon.size() + "\n" +myPoligon.v().x.ToString() + "\n" +myPoligon.v().point().x.ToString());
            //            //}


            //            myCanvas.Children.Add(ellipse);
            //            }
            //derya.drawPoligon(mmP,myCanvas);
            //MessageBox.Show("noktalar çizildi mi?");

            //myPoligon = mmP;
            //sortedEllipseList = sortByxEllipse(ellipseList);
            btnPointToConvexPolygon.IsEnabled = false;

            MessageBox.Show(" gönderilen poligon size " + myPoligon.size() + " \n");
            myPoligon.cw();

            CG.Polygon pp = ConvexHullAlgorithm.derya.convex_Uret(myPoligon, Dispatcher, myCanvas);

            string polygonToMessageBox = "";
            for (int i = 0; i < pp.size(); i++, pp.cw())
            {
                polygonToMessageBox += "(" + pp.point().x + "," + pp.point().y + ")\n";
            }
            MessageBox.Show("Oluşan poligon size : " + pp.size() + "\n" + polygonToMessageBox);



        }
        #endregion

        private LinkedList<Ellipse> sortByxEllipse(List<Ellipse> ellipseList)
        {
            Ellipse firstEllipse = ellipseList.ElementAt(0);
            LinkedList<Ellipse> sortedList = new LinkedList<Ellipse>();
            sortedList.AddFirst(firstEllipse);
            for (int i = 1; i < ellipseList.Count; i++)
            {
                LinkedListNode<Ellipse> current = sortedList.First;
                while (Canvas.GetLeft(current.Value) < Canvas.GetLeft(ellipseList.ElementAt(i)))
                {
                    if (current == sortedList.Last)
                    {
                        sortedList.AddAfter(current, ellipseList.ElementAt(i));
                        break;
                    }
                    current = current.Next;
                }

                if (Canvas.GetLeft(current.Value) > Canvas.GetLeft(ellipseList.ElementAt(i)))
                {
                    sortedList.AddBefore(current, ellipseList.ElementAt(i));
                }
            }
            return sortedList;
        }

        private void Window_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            //double x = (double)Convert.ToInt32(e.GetPosition(coordinateCanvas).X);
            //double y = (double)Convert.ToInt32(e.GetPosition(coordinateCanvas).Y);
            //Ellipse ellipse = new Ellipse();
            //ellipse.Width = 10;
            //ellipse.Height = 10;
            //ellipse.Fill = new SolidColorBrush(Colors.White);
            //ellipse.Stroke = new SolidColorBrush(Colors.Black);
            //ellipse.StrokeThickness = 1;
            //#region Tooltip oluşturma
            //Vector myPoint = new Vector(x, y);
            //myPoint.Y = myPoint.Y;
            //ellipse.ToolTip = "(" + myPoint.X + "," + myPoint.Y + ")";
            //#endregion

            //coordinateCanvas.Children.Add(ellipse);
            //Canvas.SetLeft(ellipse, x);
            //Canvas.SetTop(ellipse, y);
            //Canvas.SetZIndex(ellipse, 1);
            //ellipseDictionary.Add(new Vector(x + 3, y + 3));
            //if (ellipseDictionary.Count > 1)
            //{
            //    Line l = new Line();
            //    l.Stroke = new SolidColorBrush(Colors.Gray);
            //    l.StrokeThickness = 1;
            //    l.X1 = ellipseDictionary[ellipseDictionary.Count - 2].X;
            //    l.X2 = ellipseDictionary[ellipseDictionary.Count - 1].X;
            //    l.Y1 = ellipseDictionary[ellipseDictionary.Count - 2].Y;
            //    l.Y2 = ellipseDictionary[ellipseDictionary.Count - 1].Y;
            //    coordinateCanvas.Children.Add(l);
            //    Canvas.SetZIndex(l, 0);

            //}
        }

        private void Window_MouseMove_1(object sender, MouseEventArgs e)
        {
            Point myPoint = e.GetPosition(myCanvas);
            windowOrigin.X = myCanvas.ActualWidth / 2;
            windowOrigin.Y = myCanvas.ActualHeight / 2;
            cursorToolTip.HorizontalOffset = myPoint.X - 200;
            cursorToolTip.VerticalOffset = myPoint.Y - 200;
            cursorToolTip.Content = "(" + (myPoint.X) + ", -" + myPoint.Y + ")";
            horizontalRuler.RaiseHorizontalRulerMoveEvent(e);
            verticalRuler.RaiseVerticalRulerMoveEvent(e);
        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double x = (double)Convert.ToInt32(e.GetPosition(myCanvas).X);
            double y = (double)Convert.ToInt32(e.GetPosition(myCanvas).Y);
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 10;
            ellipse.Height = 10;
            ellipse.ToolTip = "(" + x + "," + y + ")";
            ellipse.Fill = new SolidColorBrush(Colors.White);
            ellipse.StrokeThickness = 1;

            PointByMe myPoint = new PointByMe(x, y);
            myPoint = myPoint - origin;
            myPoint.y = -(myPoint.y);
            ellipse.ToolTip = "(" + myPoint.x + "," + myPoint.y + ")";

            //myPoligon.advance(Rotation.CLOCKWISE);
            ////if(myPoligon!=null)
            //        if(myPoligon.size()>0) MessageBox.Show("şu an point x değeri :"+myPoligon.point().x.ToString());

            myPoligon.insert(myPoint);
            // cw() testi için bu döngü
            //for (int i = 0; i < myPoligon.size(); i++, myPoligon.cw()) //.advance(CG.Rotation.CLOCKWISE))
            //{
            //    MessageBox.Show("i:"+ i + "\n"+ myPoligon.size() + "\n" +myPoligon.v().x.ToString() + "\n" +myPoligon.v().point().x.ToString());
            //}


            myCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            ellipseList.Add(ellipse);

            myListBox.Items.Add(myPoint);          
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            //Point myPoint = e.GetPosition(this);
            //cursorToolTip.HorizontalOffset = myPoint.X;
            //cursorToolTip.VerticalOffset = myPoint.Y;
            //windowOrigin.X = myCanvas.ActualWidth / 2;
            //windowOrigin.Y = myCanvas.ActualHeight / 2;
            //myPoint.Y = myPoint.Y;
            //cursorToolTip.Content = "(" + myPoint.X + "," + myPoint.Y + ")";
            //horizontalRuler.RaiseHorizontalRulerMoveEvent(e);
            //verticalRuler.RaiseVerticalRulerMoveEvent(e);
        }


    }
}
