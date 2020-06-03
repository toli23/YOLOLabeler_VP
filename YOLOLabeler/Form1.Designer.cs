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
            this.colorPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // colorPanel
            // 
            this.colorPanel.AllowDrop = true;
            this.colorPanel.AutoScroll = true;
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorPanel.Controls.Add(this.labelInsertClasses);
            this.colorPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.colorPanel.Location = new System.Drawing.Point(773, 0);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(250, 703);
            this.colorPanel.TabIndex = 0;
            this.colorPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.colorPanel_DragDrop);
            this.colorPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.colorPanel_DragOver);
            // 
            // labelInsertClasses
            // 
            this.labelInsertClasses.AutoSize = true;
            this.labelInsertClasses.Location = new System.Drawing.Point(55, 22);
            this.labelInsertClasses.Name = "labelInsertClasses";
            this.labelInsertClasses.Size = new System.Drawing.Size(136, 40);
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
            this.mainPanel.Location = new System.Drawing.Point(70, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(634, 638);
            this.mainPanel.TabIndex = 1;
            // 
            // linkLabelNext
            // 
            this.linkLabelNext.AutoSize = true;
            this.linkLabelNext.Location = new System.Drawing.Point(456, 82);
            this.linkLabelNext.Name = "linkLabelNext";
            this.linkLabelNext.Size = new System.Drawing.Size(40, 20);
            this.linkLabelNext.TabIndex = 3;
            this.linkLabelNext.TabStop = true;
            this.linkLabelNext.Text = "Next";
            this.linkLabelNext.Visible = false;
            this.linkLabelNext.Click += new System.EventHandler(this.linkLabelNext_Click);
            // 
            // linkLabelPrev
            // 
            this.linkLabelPrev.AutoSize = true;
            this.linkLabelPrev.Location = new System.Drawing.Point(96, 82);
            this.linkLabelPrev.Name = "linkLabelPrev";
            this.linkLabelPrev.Size = new System.Drawing.Size(64, 20);
            this.linkLabelPrev.TabIndex = 2;
            this.linkLabelPrev.TabStop = true;
            this.linkLabelPrev.Text = "Previous";
            this.linkLabelPrev.Visible = false;
            this.linkLabelPrev.Click += new System.EventHandler(this.linkLabelPrev_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 116);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(634, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // labelPictures
            // 
            this.labelPictures.AutoSize = true;
            this.labelPictures.Location = new System.Drawing.Point(227, 12);
            this.labelPictures.Name = "labelPictures";
            this.labelPictures.Size = new System.Drawing.Size(161, 40);
            this.labelPictures.TabIndex = 0;
            this.labelPictures.Text = "Drag or Browse \r\nfor your pictures folder";
            this.labelPictures.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 703);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.colorPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.colorPanel.ResumeLayout(false);
            this.colorPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Label labelInsertClasses;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Label labelPictures;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabelNext;
        private System.Windows.Forms.LinkLabel linkLabelPrev;
    }
}

