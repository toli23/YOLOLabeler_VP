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
        private Button browsePictures;
        private Random rnd = new Random();
        private ClassesDoc cd;
        private Bitmap image;
        string[] pics;
        int currPic = 0;
        private Pen p;
        public Form1()
        {
            InitializeComponent();
            InitializeDynamic();
            cd = new ClassesDoc(browseClasses.Top + 50);
            p = null;
            image = null;
            pics = null;
            
          
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


            browsePictures = new Button();
            browsePictures.Text = "Browse";
            browsePictures.Name = "btnPicturesBrowse";
            browsePictures.Height = 30;
            browsePictures.Top = 60;
            browsePictures.Left = 270;
            browsePictures.Click += browsePicturesButton_Click;
            mainPanel.Controls.Add(browsePictures);

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
        private void browsePicturesButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                string folderName = dialog.SelectedPath;
                pics = Directory.GetFiles(folderName);
                if (image != null)
                {
                    image.Dispose(); 
                    image = null;
                }
                image = new Bitmap(pics[currPic]);
                linkLabelNext.Visible = true;
                pictureBox1.Image = image;
            }
        }
        private void colorButton_Click(object sender, EventArgs e)
        {
            if(p != null)
            {
                p.Dispose();
                p = null;
            }

            p = new Pen(((Button)sender).BackColor, 1.0f);
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
                b.Click += colorButton_Click;
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

        private void linkLabelNext_Click(object sender, EventArgs e)
        {
            if(currPic == pics.Length - 2)
            {
                linkLabelNext.Visible = false;
            }
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            image = new Bitmap(pics[++currPic]);
            pictureBox1.Image = image;

            if(currPic > 0)
            {
                linkLabelPrev.Visible = true;
            }
        }

        private void linkLabelPrev_Click(object sender, EventArgs e)
        {
            if(currPic == 1)
            {
                linkLabelPrev.Visible = false;
            }

            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            image = new Bitmap(pics[--currPic]);
            pictureBox1.Image = image;

            if(currPic != (pics.Length - 1))
            {
                linkLabelNext.Visible = true;
            }
        }
    }
}
