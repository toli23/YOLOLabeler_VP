namespace YOLOLabeler
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.colorPanel = new System.Windows.Forms.Panel();
            this.labelInsertClasses = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.linkLabelNext = new System.Windows.Forms.LinkLabel();
            this.linkLabelPrev = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelPictures = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.colorPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorPanel
            // 
            this.colorPanel.AllowDrop = true;
            this.colorPanel.AutoScroll = true;
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorPanel.Controls.Add(this.labelInsertClasses);
            this.colorPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.colorPanel.Location = new System.Drawing.Point(676, 0);
            this.colorPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(219, 527);
            this.colorPanel.TabIndex = 0;
            this.colorPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.colorPanel_DragDrop);
            this.colorPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.colorPanel_DragOver);
            // 
            // labelInsertClasses
            // 
            this.labelInsertClasses.AutoSize = true;
            this.labelInsertClasses.Location = new System.Drawing.Point(52, 16);
            this.labelInsertClasses.Name = "labelInsertClasses";
            this.labelInsertClasses.Size = new System.Drawing.Size(109, 30);
            this.labelInsertClasses.TabIndex = 0;
            this.labelInsertClasses.Text = "Drag or Browse \r\nfor your .names file";
            this.labelInsertClasses.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.linkLabelNext);
            this.mainPanel.Controls.Add(this.linkLabelPrev);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Controls.Add(this.labelPictures);
            this.mainPanel.Location = new System.Drawing.Point(0, 18);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(676, 483);
            this.mainPanel.TabIndex = 1;
            // 
            // linkLabelNext
            // 
            this.linkLabelNext.AutoSize = true;
            this.linkLabelNext.Location = new System.Drawing.Point(516, 62);
            this.linkLabelNext.Name = "linkLabelNext";
            this.linkLabelNext.Size = new System.Drawing.Size(31, 15);
            this.linkLabelNext.TabIndex = 3;
            this.linkLabelNext.TabStop = true;
            this.linkLabelNext.Text = "Next";
            this.linkLabelNext.Visible = false;
            this.linkLabelNext.Click += new System.EventHandler(this.linkLabelNext_Click);
            // 
            // linkLabelPrev
            // 
            this.linkLabelPrev.AutoSize = true;
            this.linkLabelPrev.Location = new System.Drawing.Point(139, 62);
            this.linkLabelPrev.Name = "linkLabelPrev";
            this.linkLabelPrev.Size = new System.Drawing.Size(52, 15);
            this.linkLabelPrev.TabIndex = 2;
            this.linkLabelPrev.TabStop = true;
            this.linkLabelPrev.Text = "Previous";
            this.linkLabelPrev.Visible = false;
            this.linkLabelPrev.Click += new System.EventHandler(this.linkLabelPrev_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 87);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(676, 346);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // labelPictures
            // 
            this.labelPictures.AutoSize = true;
            this.labelPictures.Location = new System.Drawing.Point(267, 0);
            this.labelPictures.Name = "labelPictures";
            this.labelPictures.Size = new System.Drawing.Size(128, 30);
            this.labelPictures.TabIndex = 0;
            this.labelPictures.Text = "Browse \r\nfor your pictures folder";
            this.labelPictures.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 505);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(676, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(51, 17);
            this.toolStripStatusLabel1.Text = "X: 0, Y: 0";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(610, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "Image: None, Objects: 0";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 527);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.colorPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.colorPanel.ResumeLayout(false);
            this.colorPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Label labelInsertClasses;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label labelPictures;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelNext;
        private System.Windows.Forms.LinkLabel linkLabelPrev;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

