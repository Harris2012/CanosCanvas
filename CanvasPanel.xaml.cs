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

namespace CanosCanvas
{
    /// <summary>
    /// CanvasPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CanvasPanel : UserControl
    {
        public CanvasPanel()
        {
            InitializeComponent();
        }

        #region Dependency Properties
        public static readonly DependencyProperty CanvasWidthProperty = DependencyProperty.Register(nameof(CanvasWidth), typeof(int), typeof(CanvasPanel), new PropertyMetadata(768, new PropertyChangedCallback(CanvasWidthChanged)));

        public static readonly DependencyProperty CanvasHeightProperty = DependencyProperty.Register(nameof(CanvasHeight), typeof(int), typeof(CanvasPanel), new PropertyMetadata(1080, new PropertyChangedCallback(CanvasHeightChanged)));

        public int CanvasWidth
        {
            get { return (int)GetValue(CanvasWidthProperty); }
            set { SetValue(CanvasWidthProperty, value); }
        }

        public int CanvasHeight
        {
            get { return (int)GetValue(CanvasHeightProperty); }
            set { SetValue(CanvasHeightProperty, value); }
        }

        private static void CanvasWidthChanged(DependencyObject item, DependencyPropertyChangedEventArgs e)
        {
            CanvasPanel canvasPanel = item as CanvasPanel;
            if (canvasPanel != null)
            {
                Rectangle rect = new Rectangle();


            }
        }

        private static void CanvasHeightChanged(DependencyObject item, DependencyPropertyChangedEventArgs e)
        {

        }
        #endregion

        #region Members
        private CanvasDraw canvasDraw;
        private Rectangle border;
        #endregion

        #region Events
        private void CanvasPanelRoot_Loaded(object sender, RoutedEventArgs e)
        {
            init();
        }
        #endregion

        private void btTest_Click(object sender, RoutedEventArgs e)
        {
            init();

            var input = pathDataTextBox.Text;

            var pointGroupList = GetPointGroupList(canvasDraw.CanvasWidth, canvasDraw.CanvasHeight, input);
            if (pointGroupList != null)
            {
                foreach (var pointGroup in pointGroupList)
                {
                    Polygon polygon = new Polygon();
                    polygon.Points = new PointCollection();
                    foreach (var point in pointGroup.PointList)
                    {
                        polygon.Points.Add(new Point(point.X + canvasDraw.Left, point.Y + canvasDraw.Top));
                    }
                    polygon.Stroke = Brushes.Blue;
                    polygon.StrokeThickness = 1;
                    theCanvas.Children.Add(polygon);
                }
            }
        }

        private List<PointGroup> GetPointGroupList(double canosWidth, double canosHeight, string input)
        {
            List<PointGroup> pointGroupList = null;

            if (!string.IsNullOrEmpty(input))
            {
                pointGroupList = new List<PointGroup>();

                var items = input.Split('|');

                foreach (var item in items)
                {
                    pointGroupList.Add(GetPointGroup(canosWidth, canosHeight, item));
                }
            }

            return pointGroupList;
        }

        private PointGroup GetPointGroup(double canosWidth, double canosHeight, string input)
        {
            PointGroup pointGroup = null;

            if (!string.IsNullOrEmpty(input))
            {
                pointGroup = new PointGroup();

                List<Point> pointList = new List<Point>();
                pointGroup.PointList = pointList;

                var groups = input.Split(';');
                foreach (var group in groups)
                {
                    var point = group.Split(',');

                    var pointX = float.Parse(point[0].Replace("f", string.Empty));
                    var pointY = float.Parse(point[1].Replace("f", string.Empty));

                    Point pointF = getPointF(canosWidth, canosHeight, pointX, pointY);

                    pointList.Add(pointF);
                }
            }

            return pointGroup;
        }

        private Point getPointF(double panelWidth, double panelHeight, float x, float y)
        {
            Point point = new Point();

            double actualHeight = this.CanvasHeight * panelWidth / this.CanvasWidth;

            if (actualHeight < panelHeight)
            {
                point.X = panelWidth * x / this.CanvasWidth;
                point.Y = actualHeight * y / this.CanvasHeight;
            }
            else
            {
                var actualWidth = this.CanvasWidth * panelHeight / this.CanvasHeight;

                point.X = actualWidth * x / this.CanvasWidth;
                point.Y = panelHeight * y / this.CanvasHeight;
            }

            return point;
        }


        private void init()
        {
            theCanvas.Children.Clear();

            border = new Rectangle();

            canvasDraw = CanosSizeUtility.GetCanvasSize(theCanvas.ActualWidth, theCanvas.ActualHeight, this.CanvasWidth, this.CanvasHeight);

            border.Width = canvasDraw.CanvasWidth;
            border.Height = canvasDraw.CanvasHeight;
            border.SetValue(Canvas.LeftProperty, canvasDraw.Left);
            border.SetValue(Canvas.TopProperty, canvasDraw.Top);
            border.Stroke = Brushes.LightBlue;
            border.StrokeThickness = 1;

            this.theCanvas.Children.Add(border);
        }
    }
}
