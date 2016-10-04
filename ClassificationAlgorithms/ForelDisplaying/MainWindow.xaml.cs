using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ClassificationAlgorithms.Forel;
using Forel;

namespace ForelDisplaying
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly InversedForelAlgorithm _forelAlgorithm;
        public IList<Forel.Point> Points { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _forelAlgorithm = new InversedForelAlgorithm();
        }

        private void DisplayDefault_Click(object sender, RoutedEventArgs e)
        {
            var points = FileManager.GetData().ToList();
            foreach (var p in points)
            {
                DrawEllipse(defaultCanvas, p.X, p.Y, 15, 15, new SolidColorBrush(Colors.LightGray), 1, 1);
            }
            this.Points = points;
        }

        private void Cluster_Click(object sender, RoutedEventArgs e)
        {
            var clusters = _forelAlgorithm.Cluster(Points).ToList();
            Random rnd = new Random();
           
            foreach (var cluster in clusters)
            {
                var b = new byte[3];
                rnd.NextBytes(b);
                var randomColor = Color.FromRgb(b[0], b[1], b[2]);
                var color = new SolidColorBrush(randomColor);
                foreach (var point in cluster.Points)
                {
                    DrawEllipse(resultCanvas, point.X, point.Y, 15, 15, color, 1, 0.8);
                }


                var centerColor = new SolidColorBrush(randomColor);
                var center = cluster.Center;
                DrawEllipse(resultCanvas, center.X, center.Y, 5, 5, centerColor, 2, 0.4);
            }
        }

        private void DrawEllipse(Canvas canvas, double pointX, double pointY, double width, double height, Brush color, int strokeThickness, double opacity)
        {
            var elipse = new Ellipse
            {
                Fill = color,
                Opacity = opacity,
                StrokeThickness = strokeThickness,
                Stroke = Brushes.Black,
                Width = width,
                Height = height
            };
            Canvas.SetTop(elipse, pointY * 25);
            Canvas.SetLeft(elipse, pointX * 25);
            canvas.Children.Add(elipse);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvases();
        }

        private void ClearCanvases()
        {
            defaultCanvas.Children.Clear();
            resultCanvas.Children.Clear();
        }


        private void RadiusTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ClearCanvases();
            var radiusText = radiusTextBox.Text;
            var radius = 2d;
            if(double.TryParse(radiusText, out radius))
            {
                //_forelAlgorithm.Radius = radius;
                Cluster_Click(sender, e);
                DisplayDefault_Click(sender, e);
            }
        }
    }
}
