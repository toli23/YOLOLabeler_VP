using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Text;

namespace YOLOLabeler
{

    [ProtoContract]
    public class Scene
    {
        [ProtoMember(1)]
        public List<string> PicturePaths { get; set; }
        [ProtoMember(2)]
        public List<ImageBoxes> ImageBoxes { get; set; }
        [ProtoMember(3)]
        public MyPoint StartPos { get; set; }
        [ProtoMember(4)]
        public MyPoint EndPos { get; set; }
        [ProtoMember(5)]
        public MyPoint currentPoint { get; set; }
        [ProtoMember(6)]
        public int currentPic { get; set; }
        [ProtoMember(7)]
        public bool isDrawing { get; set; }
        [ProtoMember(8)]
        public int Width { get; set; }
        [ProtoMember(9)]
        public int Height { get; set; }
        [ProtoMember(10)]
        public ClassesDoc cd { get; set; }
        [ProtoMember(11)]
        public float PenWidth { get; set; }
        [ProtoMember(12)]
        public bool CrosshairsEnabled { get; set; }
        [ProtoMember(13)]
        public List<ActionStack> UndoList{ get; set; }

        public Scene()
        {
            PicturePaths = new List<string>();
            ImageBoxes = new List<ImageBoxes>();
            cd = new ClassesDoc(20);
            currentPic = 0;
            StartPos = new MyPoint();
            EndPos = new MyPoint();
            isDrawing = false;
            Width = 800;
            Height = 600;
            currentPoint = new MyPoint();
            PenWidth = 3.0f;
            UndoList = new List<ActionStack>();
            CrosshairsEnabled = true;
        }
        public Scene(int initTop, int w, int h)
        {
            PicturePaths = new List<string>();
            ImageBoxes = new List<ImageBoxes>();
            cd = new ClassesDoc(initTop);
            currentPic = 0;
            StartPos = new MyPoint();
            EndPos = new MyPoint();
            isDrawing = false;
            Width = w;
            Height = h;
            currentPoint = new MyPoint();
            PenWidth = 3.0f;
            UndoList = new List<ActionStack>();
            CrosshairsEnabled = true;
        }
        public void SetCurrentPoint(MyPoint p)
        {
            currentPoint = p;

        }
        public void AddPaths(string[] paths)
        {
            PicturePaths.AddRange(paths);
            for(int i = 0; i < PicturePaths.Count; i++)
            {
                ImageBoxes.Add(new ImageBoxes());
                UndoList.Add(new ActionStack());
            }
        }

        public void AddPair(MyRectangle r, Color c)
        {
            ImageBoxes[currentPic].Add(r, c);
            UndoList[currentPic].Push(r);
        }
        public bool IsPathsEmpty()
        {
            return PicturePaths.Count == 0;
        }
        public bool IsBBoxesEmpty()
        {
            return ImageBoxes.Count == 0;
        }

        public void DrawAll(Graphics g)
        {

            ImageBoxes[currentPic].DrawBoxes(g, PenWidth);

            if (CrosshairsEnabled)
            {
                DrawLines(g);
            }
        }
        public void DrawLines (Graphics g)
        {
            Pen po = new Pen(Color.Gold, 3.5f);
            po.DashStyle = DashStyle.Dot;
            g.DrawLine(po, new Point(0, currentPoint.Y), new Point(Width, currentPoint.Y));
            g.DrawLine(po, new Point(currentPoint.X, 0), new Point(currentPoint.X, Height));
            po.Dispose();
        }

        public MyRectangle GetRectangle()
        {
            int x = Math.Min(StartPos.X, EndPos.X);
            int y = Math.Min(StartPos.Y, EndPos.Y);
            int w = Math.Abs(StartPos.X - EndPos.X);
            int h = Math.Abs(StartPos.Y - EndPos.Y);
            MyRectangle rect = new MyRectangle(x ,y ,w ,h);
       

            return rect;
        }

        public void SaveLabels()
        {
            List<string> rows = new List<string>();
            float dw = 1.0f / Width;
            float dh = 1.0f / Height;
            Dictionary<MyRectangle, int> boxes = ImageBoxes[currentPic].GetBoxes();
            foreach(KeyValuePair<MyRectangle, int> pair in boxes)
            {
                int cls_ind = cd.ClassObjects[pair.Value].Item2;
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
        public void Undo()
        {
            if (UndoList[currentPic].DrawnRectangles.Count != 0)
            {
                MyRectangle r = UndoList[currentPic].Pop();


                ImageBoxes[currentPic].Remove(r);
                
            }
        }

        private Point SerializePoint(string s)
        {
            string[] splitted = s.Split(",");

            return new Point(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }

    }
}
