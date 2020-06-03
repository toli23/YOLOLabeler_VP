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
            this.colorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorPanel
            // 
            this.colorPanel.AllowDrop = true;
            this.colorPanel.AutoScroll = true;
            this.colorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorPanel.Controls.Add(this.labelInsertClasses);
            this.colorPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.colorPanel.Location = new System.Drawing.Point(716, 0);
            this.colorPanel.Name = "colorPanel";
            this.colorPanel.Size = new System.Drawing.Size(250, 551);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 551);
            this.Controls.Add(this.colorPanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.colorPanel.ResumeLayout(false);
            this.colorPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel colorPanel;
        private System.Windows.Forms.Label labelInsertClasses;
    }
}

