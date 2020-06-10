﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Text;

namespace YOLOLabeler
{
    public class Scene
    {
        public List<string> PicturePaths { get; set; }
        public List<Dictionary<Rectangle, Pen>> BBoxes { get; set; }
        public Point StartPos { get; set; }
        public Point EndPos { get; set; }

        public Point currentPoint { get; set; }

        public int currentPic { get; set; }
        public bool isDrawing { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public ClassesDoc cd { get; set; }

        public Scene(int initTop, int w, int h)
        {
            PicturePaths = new List<string>();
            BBoxes = new List<Dictionary<Rectangle, Pen>>();
            cd = new ClassesDoc(initTop);
            currentPic = 0;
            StartPos = Point.Empty;
            EndPos = Point.Empty;
            isDrawing = false;
            Width = w;
            Height = h;
            currentPoint = Point.Empty;
        }
        public void SetCurrentPoint(Point p)
        {
            currentPoint = p;

        }
        public void AddPaths(string[] paths)
        {
            PicturePaths.AddRange(paths);
            for(int i = 0; i < PicturePaths.Count; i++)
            {
                BBoxes.Add(new Dictionary<Rectangle, Pen>());
            }
        }

        public void AddPair(Rectangle r, Pen p)
        {
            BBoxes[currentPic][r] = p;
        }
        public bool IsPathsEmpty()
        {
            return PicturePaths.Count == 0;
        }
        public bool IsBBoxesEmpty()
        {
            return BBoxes.Count == 0;
        }

        public void DrawAll(Graphics g)
        {
            
            foreach (KeyValuePair<Rectangle,Pen> pair in BBoxes[currentPic])
            {
                Color c = Color.FromArgb(128, pair.Value.Color.R, pair.Value.Color.G, pair.Value.Color.B);
                Brush b = new SolidBrush(c);

                g.DrawRectangle(pair.Value, pair.Key);
                g.FillRectangle(b, pair.Key);
                b.Dispose();
                
            }
            DrawLines(g);
        }
        public void DrawLines (Graphics g)
        {
            Pen po = new Pen(Color.Gray, 1);
            po.DashStyle = DashStyle.Dot;
            g.DrawLine(po, new Point(0, currentPoint.Y), new Point(Width, currentPoint.Y));
            g.DrawLine(po, new Point(currentPoint.X, 0), new Point(currentPoint.X, Height));
            po.Dispose();
        }

        public Rectangle GetRectangle()
        {
            Rectangle rect = new Rectangle();
            rect.X = Math.Min(StartPos.X, EndPos.X);
            rect.Y = Math.Min(StartPos.Y, EndPos.Y);

            rect.Width = Math.Abs(StartPos.X - EndPos.X);
            rect.Height = Math.Abs(StartPos.Y - EndPos.Y);

            return rect;
        }

        public void SaveLabels()
        {
            List<string> rows = new List<string>();
            float dw = 1.0f / Width;
            float dh = 1.0f / Height;
            foreach (KeyValuePair<Rectangle, Pen> pair in BBoxes[currentPic])
            {
                int cls_ind = cd.ClassObjects[pair.Value.Color].Item2;
                float x = (pair.Key.X + (pair.Key.X + pair.Key.Width)) / 2.0f;
                float y = (pair.Key.Y + (pair.Key.Y + pair.Key.Height)) / 2.0f;
                x *= dw;
                float w = pair.Key.Width * dw;
                y *= dh;
                float h = pair.Key.Height * dh;
                rows.Add(string.Format("{0} {1} {2} {3} {4}", cls_ind, x, y, w, h));
            }

            string FileName = Path.GetFileNameWithoutExtension(PicturePaths[currentPic]) + ".txt";
            if (!Directory.Exists("labels"))
            {
                Directory.CreateDirectory("labels");
            }
            File.WriteAllLines(Path.Combine("labels", FileName), rows);
        }

    }
}
