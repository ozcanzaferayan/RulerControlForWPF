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
using System.ComponentModel;

namespace RulerControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:RulerControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:RulerControl;assembly=RulerControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    /// 
    #region Enumerations
    public enum enumOrientation { Horizontal, Vertical }
    #endregion

    [TemplatePart(Name = "trackLine", Type = typeof(Line))]
    public class RulerControl : Control
    {
        #region MouseMoveRoutedEvent
        public static readonly RoutedEvent MouseMoveEvent = EventManager.RegisterRoutedEvent(
            "MouseMove", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RulerControl));

        public event RoutedEventHandler MouseMove
        {
            add { AddHandler(MouseMoveEvent, value); }
            remove { RemoveHandler(MouseMoveEvent, value); }
        }
        #endregion

        #region DepencyProperty OrientationProperty
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("DisplayMode", typeof(enumOrientation), typeof(RulerControl),
            new UIPropertyMetadata(enumOrientation.Horizontal));

        public enumOrientation Orientation
        {
            get { return (enumOrientation)base.GetValue(OrientationProperty); }
            set { this.SetValue(OrientationProperty, value); }
        }
        #endregion
        #region DepencyProperty MajorIntervalProperty
        public static readonly DependencyProperty MajorIntervalProperty =
            DependencyProperty.Register("MajorIntervalProperty", typeof(int), typeof(RulerControl),
            new UIPropertyMetadata(100));

        public int MajorInterval
        {
            get { return (int)base.GetValue(MajorIntervalProperty); }
            set { this.SetValue(MajorIntervalProperty, value); }
        }
        #endregion
        #region DepencyProperty MarkLengthProperty
        public static readonly DependencyProperty MarkLengthProperty =
            DependencyProperty.Register("MarkLengthProperty", typeof(int), typeof(RulerControl),
            new UIPropertyMetadata(20));

        public int MarkLength
        {
            get { return (int)base.GetValue(MarkLengthProperty); }
            set { this.SetValue(MarkLengthProperty, value); }
        }
        #endregion
        #region DepencyProperty MiddleMarkLengthProperty
        public static readonly DependencyProperty MiddleMarkLengthProperty =
            DependencyProperty.Register("MiddleMarkLengthProperty", typeof(int), typeof(RulerControl),
            new UIPropertyMetadata(10));

        public int MiddleMarkLength
        {
            get { return (int)base.GetValue(MiddleMarkLengthProperty); }
            set { this.SetValue(MiddleMarkLengthProperty, value); }
        }
        #endregion
        #region DepencyProperty LittleMarkLengthProperty
        public static readonly DependencyProperty LittleMarkLengthProperty =
            DependencyProperty.Register("LittleMarkLengthProperty", typeof(int), typeof(RulerControl),
            new UIPropertyMetadata(5));

        public int LittleMarkLength
        {
            get { return (int)base.GetValue(LittleMarkLengthProperty); }
            set { this.SetValue(LittleMarkLengthProperty, value); }
        }
        #endregion
        #region DepencyProperty StartValueProperty
        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValueProperty", typeof(double), typeof(RulerControl),
            new UIPropertyMetadata(0.0));

        public double StartValue
        {
            get { return (double)base.GetValue(StartValueProperty); }
            set { this.SetValue(StartValueProperty, value); }
        }
        #endregion
        Point mousePosition;
        Pen mouseTrackPen = new Pen(new SolidColorBrush(Colors.Black), 1);
        Line mouseVerticalTrackLine;
        Line mouseHorizontalTrackLine;
        
        static RulerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RulerControl), new FrameworkPropertyMetadata(typeof(RulerControl)));
        }
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            RenderOptions.SetEdgeMode(this, EdgeMode.Aliased);
            double psuedoStartValue = StartValue;
            #region Horizontal Ruler
            if (this.Orientation == enumOrientation.Horizontal)
            {
                for (int i = 0; i < this.ActualWidth / MajorInterval; i++)
                {
                    var ft = new FormattedText((psuedoStartValue * MajorInterval).ToString(), System.Globalization.CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 10, Brushes.Black);
                    drawingContext.DrawText(ft, new Point(i * MajorInterval, 0));
                    drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(i * MajorInterval, MarkLength), new Point(i * MajorInterval,0));
                    drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Green), 1),
                        new Point(i * MajorInterval + (MajorInterval / 2), MiddleMarkLength),
                        new Point(i * MajorInterval + (MajorInterval / 2), 0));
                    for (int j = 1; j < 10; j++)
                    {
                        if (j == 5)
                        {
                            continue;
                        }
                        drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Blue), 1),
                        new Point(i * MajorInterval + (((MajorInterval * j) / 10)), LittleMarkLength),
                        new Point(i * MajorInterval + (((MajorInterval * j) / 10)), 0));
                    }
                    psuedoStartValue++;
                }
            }
            #endregion
            #region Vertical Ruler
            else
            {
                psuedoStartValue = StartValue;
                for (int i = 0; i < this.ActualHeight / MajorInterval; i++)
                {
                    var ft = new FormattedText((psuedoStartValue * MajorInterval).ToString(), System.Globalization.CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 10, Brushes.Black);
                    drawingContext.DrawText(ft, new Point(0, i * MajorInterval));
                    drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(MarkLength, i * MajorInterval), new Point(0, i * MajorInterval));
                    drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), new Point(MarkLength, i * MajorInterval), new Point(0, i * MajorInterval));
                    drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Green), 1),
                        new Point(MiddleMarkLength, i * MajorInterval + (MajorInterval / 2)),
                        new Point(0, i * MajorInterval + (MajorInterval / 2)));
                    for (int j = 1; j < 10; j++)
                    {
                        if (j==5)
                        {
                            continue;
                        }
                        drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Blue), 1),
                        new Point(LittleMarkLength, i * MajorInterval + (((MajorInterval*j) / 10))),
                        new Point(0, i * MajorInterval + (((MajorInterval*j) / 10))));
                    }
                    psuedoStartValue++;
                }
            }
            #endregion
            
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            
        }
        public void RaiseHorizontalRulerMoveEvent(MouseEventArgs e)
        {
            Point mousePoint = e.GetPosition(this);
            mouseHorizontalTrackLine.X1 = mouseHorizontalTrackLine.X2 = mousePoint.X;
        }
        public void RaiseVerticalRulerMoveEvent(MouseEventArgs e)
        {
            Point mousePoint = e.GetPosition(this);
            mouseVerticalTrackLine.Y1 = mouseVerticalTrackLine.Y2 = mousePoint.Y;
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            mouseVerticalTrackLine = GetTemplateChild("verticalTrackLine") as Line;
            mouseHorizontalTrackLine = GetTemplateChild("horizontalTrackLine") as Line;
            mouseVerticalTrackLine.Visibility = Visibility.Visible;
            mouseHorizontalTrackLine.Visibility = Visibility.Visible;

        }
    }
}
