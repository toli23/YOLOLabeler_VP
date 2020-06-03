using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YOLOLabeler
{
    public partial class Form1 : Form
    {
        private Button browseClasses;
        private Random rnd = new Random();
        private ClassesDoc cd;

        public Form1()
        {
            InitializeComponent();
            InitializeDynamic();
            cd = new ClassesDoc(browseClasses.Top + 50);
            
          
        }

        private void InitializeDynamic() {
            browseClasses = new Button();
            browseClasses.Text = "Browse";
            browseClasses.Name = "btnBrowse";
            browseClasses.Height = 30;
            browseClasses.Top = 70;
            browseClasses.Left = 80;
            browseClasses.Click += browseClassesButton_Click;
            colorPanel.Controls.Add(browseClasses);

     
        }


        private void browseClassesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ".names file (*.names)|*.names|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
    
                var fileStream = dialog.OpenFile();
                string content = cd.ReadClassesFromFile(fileStream);
                GenerateClasses(content);
            }
        }

        private void GenerateClasses(string content)
        {
            if (cd.Classes.Count != 0)
            {
                foreach (string cls in cd.Classes)
                {
                    colorPanel.Controls.RemoveByKey("colorLabel_" + cls);
                    colorPanel.Controls.RemoveByKey("btnColor_" + cls);

                }
                cd.CurrTop = cd.InitTop;
                cd.RemoveAllColors();
            }
            string[] arr = content.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            cd.AddClasses(arr);

            CreateClassesControls();
        }

        private void CreateClassesControls()
        {
            foreach(string cls in cd.Classes)
            {
                Label l = new Label();
                l.Text = cls;
                l.Name = "colorLabel_" + l.Text;
                Button b = new Button();
                b.Name = "btnColor_" + l.Text;
                b.Width = 35;
                b.Top = cd.CurrTop;
                b.Left = cd.InitLeft;
                l.Top = cd.CurrTop;
                l.Left = cd.InitLeft + b.Width;
                cd.CurrTop += 20;
                Color c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                b.BackColor = c;
                cd.AddColor(c);
                colorPanel.Controls.Add(b);
                colorPanel.Controls.Add(l);

            }
        }

        private void colorPanel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void colorPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if(files.Length != 1)
                {
                    MessageBox.Show("You need to drag one .names file");
                }
                if (!Path.GetFileName(files[0]).EndsWith(".names"))
                {
                    MessageBox.Show("The selected file is not .names file");
                }
                string content = cd.ReadClassesFromFile(new FileStream(files[0], FileMode.Open, FileAccess.Read));
                GenerateClasses(content);

            }
        }

       
    }
}
