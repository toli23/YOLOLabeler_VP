using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace YOLOLabeler
{

    public partial class Form1 : Form
    {
        private Button browseClasses;
        private Button browsePictures;
        private Button saveLabels;
        private Random rnd = new Random();
        private Bitmap image;
        private Pen p;
        private Scene s;
        private string filename;

        public Form1()
        {
            InitializeComponent();
            InitializeDynamic();
            p = null;
            s = new Scene(browseClasses.Top + 50, pictureBox1.Width, pictureBox1.Height);
            image = null;
            filename = string.Empty;

        }

        private void InitializeDynamic()
        {
            browseClasses = new Button();
            browseClasses.Text = "Browse";
            browseClasses.Name = "btnBrowse";
            browseClasses.Height = 30;
            browseClasses.Top = 70;
            browseClasses.Left = (colorPanel.Width / 2) - (browseClasses.Width / 2);
            browseClasses.Click += browseClassesButton_Click;
            colorPanel.Controls.Add(browseClasses);
            


            browsePictures = new Button();
            browsePictures.Text = "Browse";
            browsePictures.Name = "btnPicturesBrowse";
            browsePictures.Height = 30;
            browsePictures.Top = 45;
            browsePictures.Left = (mainPanel.Width / 2) - (browsePictures.Width / 2);
            browsePictures.Click += browsePicturesButton_Click;
            mainPanel.Controls.Add(browsePictures);

            saveLabels = new Button();
            saveLabels.Text = "Save Label";
            saveLabels.Name = "btnSaveLabel";
            saveLabels.Width = 90;
            saveLabels.Height = 30;
            saveLabels.Top = pictureBox1.Top + pictureBox1.Height + 20;
            saveLabels.Left = (mainPanel.Width / 2) - (saveLabels.Width / 2);
            saveLabels.Click += btnSaveLabel_Click;
            saveLabels.Visible = false;
            mainPanel.Controls.Add(saveLabels);

            labelInsertClasses.Left = (colorPanel.Width / 2) - (labelInsertClasses.Width / 2);
            labelPictures.Left = (mainPanel.Width / 2) - (labelPictures.Width / 2);

        }


        private void browseClassesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ".names file (*.names)|*.names|All files (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!Path.GetFileName(dialog.FileName).EndsWith(".names"))
                {
                    MessageBox.Show("The selected file is not .names file");
                    return;
                }

                using (Stream fileStream = dialog.OpenFile())
                {
                    string content = s.cd.ReadClassesFromFile(fileStream);
                    GenerateClasses(content);
                }

            }
        }
        private void browsePicturesButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string folderName = dialog.SelectedPath;
                string[] paths = Directory.GetFiles(folderName);
                if (paths.Length == 0)
                {
                    MessageBox.Show("Folder is empty");
                    return;
                }
                s.AddPaths(paths);
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }
                image = new Bitmap(s.PicturePaths[s.currentPic]);
                linkLabelNext.Visible = true;
                saveLabels.Visible = true;
                pictureBox1.Image = image;
                toolStripStatusLabel2.Text = string.Format("Name: {0}, Objects: {1} ", Path.GetFileName(s.PicturePaths[s.currentPic]), s.BBoxes[s.currentPic].Count);
            }
        }

        private void btnSaveLabel_Click(object sender, EventArgs e)
        {
            if (s.BBoxes[s.currentPic].Count == 0)
            {
                MessageBox.Show("There are no objects selected");
            }
            else
            {
                s.SaveLabels();
            }
        }
        private void colorButton_Click(object sender, EventArgs e)
        {

            p = new Pen(((Button)sender).BackColor, s.PenWidth);
        }

        private void GenerateClasses(string content)
        {
            if (s.cd.ClassObjects.Count != 0)
            {
                foreach (Tuple<string, int> t in s.cd.ClassObjects.Values)
                {
                    colorPanel.Controls.RemoveByKey("colorLabel_" + t.Item1);
                    colorPanel.Controls.RemoveByKey("btnColor_" + t.Item1);

                }
                s.cd.CurrTop = s.cd.InitTop;
                s.cd.Clear();
            }
            string[] arr = content.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            CreateClassesControls(arr);
        }

        private void CreateClassesControls(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Label l = new Label();
                l.Text = arr[i];
                l.Name = "colorLabel_" + l.Text;
                Button b = new Button();
                b.Name = "btnColor_" + l.Text;
                b.Width = 45;
                b.Top = s.cd.CurrTop;
                b.Left = s.cd.InitLeft;
                l.Top = s.cd.CurrTop;
                l.Left = s.cd.InitLeft + b.Width;
                s.cd.CurrTop += 20;
                Color c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                if (s.cd.ColorExists(c))
                {
                    c = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
                b.BackColor = c;
                b.Click += colorButton_Click;
                s.cd.AddClassAndColor(c, arr[i], i);
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
                if (files.Length != 1)
                {
                    MessageBox.Show("You need to drag one .names file");
                    return;
                }
                if (!Path.GetFileName(files[0]).EndsWith(".names"))
                {
                    MessageBox.Show("The selected file is not .names file");
                    return;
                }
                using (Stream fileStream = new FileStream(files[0], FileMode.Open, FileAccess.Read))
                {
                    string content = s.cd.ReadClassesFromFile(fileStream);
                    GenerateClasses(content);
                }


            }
        }

        private void linkLabelNext_Click(object sender, EventArgs e)
        {
            if (s.isDrawing)
            {
                s.isDrawing = false;
            }
            if (s.currentPic == s.PicturePaths.Count - 2)
            {
                linkLabelNext.Visible = false;
            }
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            image = new Bitmap(s.PicturePaths[++s.currentPic]);
            pictureBox1.Image = image;
            toolStripStatusLabel2.Text = string.Format("Name: {0}, Objects: {1} ", Path.GetFileName(s.PicturePaths[s.currentPic]), s.BBoxes[s.currentPic].Count);

            if (s.currentPic > 0)
            {
                linkLabelPrev.Visible = true;
            }
        }

        private void linkLabelPrev_Click(object sender, EventArgs e)
        {
            if (s.isDrawing)
            {
                s.isDrawing = false;
            }
            if (s.currentPic == 1)
            {
                linkLabelPrev.Visible = false;
            }

            if (image != null)
            {
                image.Dispose();
                image = null;
            }
            image = new Bitmap(s.PicturePaths[--s.currentPic]);
            pictureBox1.Image = image;
            toolStripStatusLabel2.Text = string.Format("Name: {0}, Objects: {1} ", Path.GetFileName(s.PicturePaths[s.currentPic]), s.BBoxes[s.currentPic].Count);

            if (s.currentPic != (s.PicturePaths.Count - 1))
            {
                linkLabelNext.Visible = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            s.EndPos = e.Location;
            s.currentPoint = e.Location;

            toolStripStatusLabel1.Text = string.Format("X: {0}, Y: {1} ", e.Location.X, e.Location.Y);


            pictureBox1.Invalidate(true);

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            if (!s.IsBBoxesEmpty() && s.BBoxes[s.currentPic].Count > 0)
            {
                s.DrawAll(e.Graphics);
            }
            else if (!s.IsPathsEmpty())
            {
                if (s.CrosshairsEnabled)
                {
                    s.DrawLines(e.Graphics);
                }
            }
            if (s.isDrawing)
            {
                Rectangle r = s.GetRectangle();
                Color c = Color.FromArgb(128, p.Color.R, p.Color.G, p.Color.B);
                Brush b = new SolidBrush(c);
                e.Graphics.DrawRectangle(p, r);
                e.Graphics.FillRectangle(b, r);
                b.Dispose();



            }
            if (!s.IsPathsEmpty())
            {
                toolStripStatusLabel2.Text = string.Format("Name: {0}, Objects: {1} ", Path.GetFileName(s.PicturePaths[s.currentPic]), s.BBoxes[s.currentPic].Count);
            }

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!s.IsPathsEmpty() && !s.cd.IsEmpty())
            {
                if (p == null)
                {
                    MessageBox.Show("Please select a class to label");
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {

                        if (!s.isDrawing)
                        {
                            s.isDrawing = true;
                            s.StartPos = e.Location;
                            s.EndPos = e.Location;
                        }

                        else
                        {
                            s.isDrawing = false;
                            s.EndPos = e.Location;
                            Rectangle rect = s.GetRectangle();
                            if (rect.Width > 0 && rect.Height > 0)
                            {
                                s.AddPair(rect, p.Color);
                            }
                            pictureBox1.Invalidate();
                        }
                    }
                }

            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save our project";
            saveFileDialog.Filter = "Yolo Labeller Project |*.ylp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog.FileName;
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, s);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename != string.Empty)
            {
                using (FileStream stream = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, s);
                }
            }
            else
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open our project";
            openFileDialog.Filter = "Yolo Labeller Project |*.ylp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    IFormatter formatter = new BinaryFormatter();
                    s = (Scene)formatter.Deserialize(stream);
                    filename = openFileDialog.FileName;

                }
                LoadFiles();
            }
        }
        private void LoadFiles()
        {

            if (!s.IsPathsEmpty())
            {
                if (image != null)
                {
                    image.Dispose();
                    image = null;
                }

                image = new Bitmap(s.PicturePaths[s.currentPic]);
                pictureBox1.Image = image;
                saveLabels.Visible = true;

                if (s.currentPic == 0)
                {
                    linkLabelNext.Visible = true;
                    linkLabelPrev.Visible = false;

                }
                else if (s.currentPic == s.PicturePaths.Count - 2)
                {
                    linkLabelNext.Visible = false;
                    linkLabelPrev.Visible = true;
                }
                else
                {
                    linkLabelNext.Visible = true;
                    linkLabelPrev.Visible = true;
                }
            }



            if (s.cd.ClassObjects.Count != 0)
            {
                foreach (Tuple<string, int> t in s.cd.ClassObjects.Values)
                {
                    colorPanel.Controls.RemoveByKey("colorLabel_" + t.Item1);
                    colorPanel.Controls.RemoveByKey("btnColor_" + t.Item1);

                }
                s.cd.CurrTop = s.cd.InitTop;
                foreach (KeyValuePair<Color, Tuple<string, int>> pair in s.cd.ClassObjects)
                {
                    Label l = new Label();
                    l.Text = pair.Value.Item1;
                    l.Name = "colorLabel_" + l.Text;
                    Button b = new Button();
                    b.Name = "btnColor_" + l.Text;
                    b.Width = 45;
                    b.Top = s.cd.CurrTop;
                    b.Left = s.cd.InitLeft;
                    l.Top = s.cd.CurrTop;
                    l.Left = s.cd.InitLeft + b.Width;
                    s.cd.CurrTop += 20;
                    b.BackColor = pair.Key;
                    b.Click += colorButton_Click;
                    colorPanel.Controls.Add(b);
                    colorPanel.Controls.Add(l);

                }
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s.Undo();

            toolStripStatusLabel2.Text = string.Format("Name: {0}, Objects: {1} ", Path.GetFileName(s.PicturePaths[s.currentPic]), s.BBoxes[s.currentPic].Count);
            pictureBox1.Invalidate();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void toolStripMenuItemSmall_Click(object sender, EventArgs e)
        {
            s.PenWidth = 1.0f;
            if(p != null)
            {
                p.Width = s.PenWidth;
            }
            toolStripMenuItem3.Checked = true;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;
            pictureBox1.Invalidate();


        }
        private void toolStripMenuItemMedium_Click(object sender, EventArgs e)
        {
            s.PenWidth = 3.0f;
            if (p != null)
            {
                p.Width = s.PenWidth;
            }
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;
            toolStripMenuItem5.Checked = false;
            pictureBox1.Invalidate();


        }
        private void toolStripMenuItemLarge_Click(object sender, EventArgs e)
        {
            s.PenWidth = 5.0f;
            if (p != null)
            {
                p.Width = s.PenWidth;
            }
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = true;
            pictureBox1.Invalidate();


        }
        private void toolStripMenuItemCrosshairs_Click(object sender, EventArgs e)
        {
            s.CrosshairsEnabled = !s.CrosshairsEnabled;
            toolStripMenuItem2.Checked = s.CrosshairsEnabled;
            pictureBox1.Invalidate();

        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/toli23/YOLOLabeler_VP");
        }
    }

}
