using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace YOLOLabeler
{
    [ProtoContract]
    public class MyPoint
    {
        [ProtoMember(1)]
        public int X { get; set; }
        [ProtoMember(2)]
        public int Y { get; set; }

        public MyPoint()
        {
            X = 0;
            Y = 0;
        }
        public MyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetCoords(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
