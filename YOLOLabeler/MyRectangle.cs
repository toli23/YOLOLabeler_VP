using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace YOLOLabeler
{
    /// <summary>
    /// Serializable Rectange class
    /// </summary>
    [ProtoContract]
    public class MyRectangle
    {
        [ProtoMember(1)]
        public int X { get; set; }
        [ProtoMember(2)]
        public int Y { get; set; }
        [ProtoMember(3)]
        public int Width { get; set; }
        [ProtoMember(4)]
        public int Height { get; set; }
     
        public MyRectangle()
        {
            X = Y = Width = Height = 0;
        }
        public MyRectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override bool Equals(object obj)
        {
            return obj is MyRectangle rectangle &&
                   X == rectangle.X &&
                   Y == rectangle.Y &&
                   Width == rectangle.Width &&
                   Height == rectangle.Height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Width, Height);
        }
    }
}
