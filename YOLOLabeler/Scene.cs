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
    /// <summary>
    /// Main class used to Save/Load project
    /// </summary>
    [ProtoContract]
    public class Scene
    {
        /// <summary>
        /// List containing the path to each image
        /// </summary>
        [ProtoMember(1)]
        public List<string> PicturePaths { get; set; }
        /// <summary>
        /// List containing the rectangles for each image
        /// </summary>
        [ProtoMember(2)]
        public List<ImageBoxes> ImageBoxes { get; set; }
        /// <summary>
        /// Contaning the X,Y coordinates when the mouse is clicked for the first time
        /// </summary>
        [ProtoMember(3)]
        public MyPoint StartPos { get; set; }
        /// <summary>
        /// Contaning the X,Y coordinates when the mouse is clicked for the second time
        /// </summary>
        [ProtoMember(4)]
        public MyPoint EndPos { get; set; }
        /// <summary>
        /// Contaning the X,Y coordinates of pointer of the mouse on the picture
        /// </summary>
        [ProtoMember(5)]
        public MyPoint currentPoint { get; set; }
        /// <summary>
        /// Index of picture currently showing
        /// </summary>
        [ProtoMember(6)]
        public int currentPic { get; set; }
        /// <summary>
        /// Value singalizing if we are drawing or not
        /// </summary>
        [ProtoMember(7)]
        public bool isDrawing { get; set; }
        /// <summary>
        /// Width of the picture box
        /// </summary>
        [ProtoMember(8)]
        public int Width { get; set; }
        /// <summary>
        /// Height of the picture box
        /// </summary>
        [ProtoMember(9)]
        public int Height { get; set; }
        /// <summary>
        /// Contains information about the colors and classes (labels) names
        /// </summary>
        [ProtoMember(10)]
        public ClassesDoc cd { get; set; }
        /// <summary>
        /// Width of the border of the rectangle
        /// </summary>
        [ProtoMember(11)]
        public float PenWidth { get; set; }
        /// <summary>
        /// Value representing if crosshairs are enabled
        /// </summary>
        [ProtoMember(12)]
        public bool CrosshairsEnabled { get; set; }
        /// <summary>
        /// List containing a stack of actions for each image
        /// </summary>
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
        /// <summary>
        /// Sets the current Point to the mouse location on the picture
        /// </summary>
        /// <param name="p">Serializable Point object</param>
        public void SetCurrentPoint(MyPoint p)
        {
            currentPoint = p;

        }
        /// <summary>
        /// Saves the paths to each image
        /// </summary>
        /// <param name="paths">String array of paths to images</param>
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
        /// <summary>
        /// Draws all rectangles on the given picture
        /// </summary>
        /// <param name="g">Graphics object</param>
        public void DrawAll(Graphics g)
        {

            ImageBoxes[currentPic].DrawBoxes(g, PenWidth);

            if (CrosshairsEnabled)
            {
                DrawLines(g);
            }
        }
        /// <summary>
        /// Draws the crosshairs
        /// </summary>
        /// <param name="g">Graphics object</param>
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
        /// <summary>
        /// Saves a .txt label file for the given image
        /// </summary>
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
        /// <summary>
        /// Performs undo action for the given picture
        /// </summary>
        public void Undo()
        {
            if (UndoList[currentPic].DrawnRectangles.Count != 0)
            {
                MyRectangle r = UndoList[currentPic].Pop();


                ImageBoxes[currentPic].Remove(r);
                
            }
        }
    }
}
