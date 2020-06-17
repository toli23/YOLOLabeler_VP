using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace YOLOLabeler
{
    /// <summary>
    /// Stack class containing the actions (Rectangles) for each image
    /// </summary>
    [ProtoContract]
    public class ActionStack
    {
        [ProtoMember(1)]
        public List<MyRectangle> DrawnRectangles { get; set; }
        public ActionStack()
        {
            DrawnRectangles = new List<MyRectangle>();

        }
        /// <summary>
        /// Returns the last Rectangle
        /// </summary>
        /// <returns>Serializable Rectangle</returns>
        public MyRectangle Pop()
        {
            MyRectangle mr = DrawnRectangles[DrawnRectangles.Count - 1];
            DrawnRectangles.RemoveAt(DrawnRectangles.Count - 1);
            return mr;
        }
        /// <summary>
        /// Adds Rectangle on the stack
        /// </summary>
        /// <param name="r">Serializable Rectange</param>
        public void Push(MyRectangle r)
        {
            DrawnRectangles.Add(r);
        }
    }
}
