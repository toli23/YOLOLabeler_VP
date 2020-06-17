using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace YOLOLabeler
{
    /// <summary>
    /// Holds the rectangles for an image
    /// </summary>
    [ProtoContract]
    public class ImageBoxes
    {
        /// <summary>
        /// Key: Serializable Rectangle, Value: Color as int
        /// </summary>
        [ProtoMember(1)]
        public Dictionary<MyRectangle, int> BBoxes {get; set;}
        public ImageBoxes()
        {
            BBoxes = new Dictionary<MyRectangle, int>();
        }

        public void Add(MyRectangle r, Color c)
        {
            BBoxes[r] = c.ToArgb();
        }

        public void DrawBoxes(Graphics g, float PenWidth)
        {
            foreach (KeyValuePair<MyRectangle, int> pair in BBoxes)
            {
                Color c = Color.FromArgb(pair.Value);
                Brush b = new SolidBrush(Color.FromArgb(127, c.R, c.G, c.B));
                Pen p = new Pen(c, PenWidth);
                Rectangle r = new Rectangle(pair.Key.X, pair.Key.Y, pair.Key.Width, pair.Key.Height);
                g.DrawRectangle(p, r);
                g.FillRectangle(b, r);
                b.Dispose();

            }
        }

        public Dictionary<MyRectangle, int> GetBoxes()
        {
            return BBoxes;
        }

        public void Remove(MyRectangle r)
        {
            BBoxes.Remove(r);
        }

        public int GetCount()
        {
            return BBoxes.Count;
        }
    }
}
