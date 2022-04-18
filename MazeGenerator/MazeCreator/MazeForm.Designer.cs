namespace MazeCreator
{
    partial class MazeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackgroundMazeCreator = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // BackgroundMazeCreator
            // 
            this.BackgroundMazeCreator.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundMazeCreator_DoWork);
            this.BackgroundMazeCreator.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundMazeCreator_RunWorkerCompleted);
            // 
            // MazeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.DoubleBuffered = true;
            this.Name = "MazeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Maze";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MazeForm_FormClosed);
            this.Load += new System.EventHandler(this.MazeForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MazeForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MazeForm_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker BackgroundMazeCreator;
    }
}

