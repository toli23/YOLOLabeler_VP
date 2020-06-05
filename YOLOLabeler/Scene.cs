using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Text;

namespace YOLOLabeler
{
    public class Scene
    {
        public List<string> PicturePaths { get; set; }
        public List<Dictionary<Rectangle, Pen>> BBoxes { get; set; }
        public Point StartPos { get; set; }
        public Point EndPos { get; set; }
        public int currentPic { get; set; }
        public bool isDrawing { get; set; }

        public Scene()
        {
            PicturePaths = new List<string>();
            BBoxes = new List<Dictionary<Rectangle, Pen>>();
            currentPic = 0;
            StartPos = Point.Empty;
            EndPos = Point.Empty;
            isDrawing = false;
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

        public void DrawAll(Graphics g)
        {
            foreach(KeyValuePair<Rectangle,Pen> pair in BBoxes[currentPic])
            {
                Color c = Color.FromArgb(128, pair.Value.Color.R, pair.Value.Color.G, pair.Value.Color.B);
                Brush b = new SolidBrush(c);

                g.DrawRectangle(pair.Value, pair.Key);
                g.FillRectangle(b, pair.Key);
                b.Dispose();
                
            }
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

    }
}
