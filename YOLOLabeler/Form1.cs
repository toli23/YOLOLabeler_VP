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
        private string[] arr;
        int initTop = 80;
        int initLeft = 20;
        public Form1()
        {
            arr = null;
            InitializeComponent();
            InitializeDynamic();
          
        }

        private void InitializeDynamic() {
            browseClasses = new Button();
            browseClasses.Text = "Browse";
            browseClasses.Name = "btnBrowse";
            browseClasses.Height = 30;
            browseClasses.Top = 40;
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
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string content = reader.ReadToEnd();
                    if (arr != null)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            colorPanel.Controls.RemoveByKey("colorLabel_" + arr[i]);
                            colorPanel.Controls.RemoveByKey("btnColor_" + arr[i]);
                            initTop -= 20;
                        }
                    }
                    arr = content.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                }

                CreateClassesControls();
            }
        }

        private void CreateClassesControls()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Label l = new Label();
                l.Text = arr[i];
                l.Name = "colorLabel_" + l.Text;
                Button b = new Button();
                b.Name = "btnColor_" + l.Text;
                b.Width = 35;
                b.Top = initTop;
                b.Left = initLeft;
                l.Top = initTop;
                l.Left = initLeft + b.Width;
                initTop += 20;
                Color c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                b.BackColor = c;

                colorPanel.Controls.Add(b);
                colorPanel.Controls.Add(l);

            }
        }

    }
}
