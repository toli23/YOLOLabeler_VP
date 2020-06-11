using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace YOLOLabeler
{
    [Serializable]
    public class ClassesDoc
    {
        public int InitTop { get; set; }
        public int InitLeft { get; set; }
        public int CurrTop { get; set; }
        public Dictionary<Color, Tuple<string, int>> ClassObjects { get; set; }

        public ClassesDoc(int InitTop)
        {
            this.InitTop = InitTop;
            InitLeft = 20;
            CurrTop = InitTop;
            ClassObjects = new Dictionary<Color, Tuple<string, int>>();
        }
        
        public void Clear()
        {
            ClassObjects.Clear();
        }

        public bool ColorExists(Color c)
        {
            return ClassObjects.ContainsKey(c);
        }

        public bool IsEmpty()
        {
            return ClassObjects.Count == 0;
        }

        public void AddClassAndColor(Color c, string cls, int ind)
        {

            ClassObjects[c] = new Tuple<string, int>(cls, ind);
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
