using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace YOLOLabeler
{
    [ProtoContract]
    public class ActionStack
    {
        [ProtoMember(1)]
        public List<MyRectangle> DrawnRectangles { get; set; }
        public ActionStack()
        {
            DrawnRectangles = new List<MyRectangle>();

        }

        public MyRectangle Pop()
        {
            MyRectangle mr = DrawnRectangles[DrawnRectangles.Count - 1];
            DrawnRectangles.RemoveAt(DrawnRectangles.Count - 1);
            return mr;
        }
        public void Push(MyRectangle r)
        {
            DrawnRectangles.Add(r);
        }
    }
}
