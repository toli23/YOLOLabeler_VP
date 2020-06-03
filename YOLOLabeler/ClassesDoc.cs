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
        public List<string> Classes { get; set; }
        public List<Color>  Colors { get; set; }

        public ClassesDoc(int InitTop)
        {
            this.InitTop = InitTop;
            InitLeft = 20;
            CurrTop = InitTop;
            Classes = new List<string>();
            Colors = new List<Color>();
        }

        public void AddClasses(string[] classes)
        {
            if(Classes.Count != 0)
            {
                Classes.Clear();
            }
            Classes.AddRange(classes);
        }

        public void AddColor(Color c)
        {
            Colors.Add(c);
        }
        public void RemoveAllColors()
        {
            Colors.Clear();
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
