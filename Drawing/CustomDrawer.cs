﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MonteKarloWPFApp1.Drawing
{
    public class CustomDrawer
    {
        private MyFigure _myFigure;
        private MainWindow _mainWindow;
        private SolidColorBrush _colorStroke;
        private int _scaleNumber;

        public CustomDrawer(MainWindow mainWindow, MyFigure myFigure, int scaleNumber)
        {
            _myFigure = myFigure;
            _mainWindow = mainWindow;
            _colorStroke = new SolidColorBrush(myFigure.ColorStroke);
            _scaleNumber = scaleNumber;
        }

        /// <summary>
        /// Соединяет все точки по контуру
        /// </summary>
        private void DrawLinesByPoints(Point[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                var p1 = points[i];
                var p2 = i != points.Length - 1 ? points[i + 1] : points[0];
                var line = new Line
                {
                    X1 = p1.X,
                    Y1 = p1.Y,
                    X2 = p2.X,
                    Y2 = p2.Y
                };
                line.Stroke = _colorStroke;
                _mainWindow.MainCanvas.Children.Add(line);
            }
        }

        private IEnumerable<Point> ScalePoints(IEnumerable<Point> points)
        {
            return points.Select(p => new Point(p.X * _scaleNumber, p.Y * _scaleNumber));
        }

        private void DrawPointTitles(MyPoint[] myPoints)
        {
            for (int i = 0; i < myPoints.Length; i++)
            {
                var textBlock = new TextBlock();
                textBlock.Text = myPoints[i].Title;
                textBlock.RenderTransform = new ScaleTransform { ScaleY = -1 };
                Canvas.SetLeft(textBlock, myPoints[i].Point.X * _scaleNumber);
                Canvas.SetTop(textBlock, myPoints[i].Point.Y * _scaleNumber);
                _mainWindow.MainCanvas.Children.Add(textBlock);
            }
        }

        public void DrawLines()
        {
            var points = _myFigure.Points.Select(i => i.Point);
            points = ScalePoints(points);
            DrawLinesByPoints(points.ToArray());
            DrawPointTitles(_myFigure.Points);
        }

        public void DrawArc()
        {
            var bPoint = _myFigure.Points.First(mp => mp.Title == "B").Point;
            var ePoint = _myFigure.Points.First(mp => mp.Title == "E").Point;
            var arcSize = new System.Windows.Size(54, 78);

            var g = new StreamGeometry();
            using (var gc = g.Open())
            {
                gc.BeginFigure(
                    startPoint: new System.Windows.Point(bPoint.X * _scaleNumber, bPoint.Y * _scaleNumber),
                    isFilled: false,
                    isClosed: false);
                gc.ArcTo(
                    point: new System.Windows.Point(ePoint.X * _scaleNumber, ePoint.Y * _scaleNumber),
                    size: arcSize,
                    rotationAngle: 5,
                    isLargeArc: true,
                    sweepDirection: SweepDirection.Counterclockwise,
                    isStroked: true,
                    isSmoothJoin: false);
            }

            var path = new Path
            {
                Stroke = _colorStroke,
                StrokeThickness = 1,
                Data = g
            };

            _mainWindow.MainCanvas.Children.Add(path);
        }

        public static void DrawPoints(Canvas canvas, int scaleNumber, IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                var ellipse = new Ellipse();
                ellipse.Width = 1;
                ellipse.Height = 1;
                ellipse.Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0));
                Canvas.SetLeft(ellipse, point.X * scaleNumber);
                Canvas.SetTop(ellipse, point.Y * scaleNumber);
                canvas.Children.Add(ellipse);
            }
        }
    }
}