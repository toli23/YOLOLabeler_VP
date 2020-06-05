using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace YOLOLabeler
{
    public class ClassesDoc
    {
        public int InitTop { get; set; }
        public int InitLeft { get; set; }
        public int CurrTop { get; set; }
        public Dictionary<string,Color> ClassObjects { get; set; }

        public ClassesDoc(int InitTop)
        {
            this.InitTop = InitTop;
            InitLeft = 20;
            CurrTop = InitTop;
            ClassObjects = new Dictionary<string, Color>();
        }
        
        public void Clear()
        {
            ClassObjects.Clear();
        }

        public void AddClassAndColor(string cls, Color c)
        {

            ClassObjects[cls] = c;
        }

        public string ReadClassesFromFile(Stream fileStream)
        {
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                string content = streamReader.ReadToEnd();
                fileStream.Close();
                return content;
            }
        }
    }
}
