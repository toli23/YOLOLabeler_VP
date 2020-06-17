using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace YOLOLabeler
{
    ///<sumary>
    /// Holds information about the colors for each class label
    ///</summary>
    [ProtoContract]
    public class ClassesDoc
    {
        /// <summary>
        /// Initial Top offset from labels panel
        /// </summary>
        [ProtoMember(1)]
        public int InitTop { get; set; }
        /// <summary>
        /// Left offset from labels panel
        /// </summary>
        [ProtoMember(2)]
        public int InitLeft { get; set; }
        /// <summary>
        /// Current top offset from labels panel
        /// </summary>
        [ProtoMember(3)]
        public int CurrTop { get; set; }
        /// <summary>
        /// Holds a pair of color value and class label
        /// Key: Color as int, value: tuple of class name, index
        /// </summary>
        [ProtoMember(4)]
        public Dictionary<int, Tuple<string, int>> ClassObjects { get; set; }
        
        public ClassesDoc(int InitTop)
        {
            this.InitTop = InitTop;
            InitLeft = 20;
            CurrTop = InitTop;
            ClassObjects = new Dictionary<int, Tuple<string, int>>();
        }
        
        public void Clear()
        {
            ClassObjects.Clear();
        }

        public bool ColorExists(Color c)
        {
            return ClassObjects.ContainsKey(c.ToArgb());
        }

        public bool IsEmpty()
        {
            return ClassObjects.Count == 0;
        }

        public void AddClassAndColor(Color c, string cls, int ind)
        {

            ClassObjects[c.ToArgb()] = new Tuple<string, int>(cls, ind);
        }

        public string ReadClassesFromFile(Stream fileStream)
        {
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                string content = streamReader.ReadToEnd();
                
                return content;
            }
        }
    }
}
