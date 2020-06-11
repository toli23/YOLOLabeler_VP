using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace YOLOLabeler
{      
    [Serializable]
    public class Action
    {
        public KeyValuePair<Rectangle,Color> LastAction { get; set; }
        public Action(KeyValuePair<Rectangle,Color> kv)
        {
            LastAction = kv;

        }
    }
}
