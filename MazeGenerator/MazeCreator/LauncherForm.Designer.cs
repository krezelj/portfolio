namespace MazeCreator
{
    partial class LauncherForm
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
            this.RowBox = new System.Windows.Forms.TextBox();
            this.ColBox = new System.Windows.Forms.TextBox();
            this.SeedBox = new System.Windows.Forms.TextBox();
            this.CreateMazeButton = new System.Windows.Forms.Button();
            this.ValidationMessageLabel = new System.Windows.Forms.Label();
            this.FindPathButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RowBox
            // 
            this.RowBox.ForeColor = System.Drawing.Color.Gray;
            this.RowBox.Location = new System.Drawing.Point(100, 12);
            this.RowBox.Name = "RowBox";
            this.RowBox.Size = new System.Drawing.Size(100, 20);
            this.RowBox.TabIndex = 1;
            this.RowBox.Text = "Number of rows";
            this.RowBox.TextChanged += new System.EventHandler(this.RowBox_TextChanged);
            this.RowBox.Enter += new System.EventHandler(this.RowBox_Enter);
            this.RowBox.Leave += new System.EventHandler(this.RowBox_Leave);
            // 
            // ColBox
            // 
            this.ColBox.ForeColor = System.Drawing.Color.Gray;
            this.ColBox.Location = new System.Drawing.Point(100, 38);
            this.ColBox.Name = "ColBox";
            this.ColBox.Size = new System.Drawing.Size(100, 20);
            this.ColBox.TabIndex = 2;
            this.ColBox.Text = "Number of columns";
            this.ColBox.TextChanged += new System.EventHandler(this.ColBox_TextChanged);
            this.ColBox.Enter += new System.EventHandler(this.ColBox_Enter);
            this.ColBox.Leave += new System.EventHandler(this.ColBox_Leave);
            // 
            // SeedBox
            // 
            this.SeedBox.ForeColor = System.Drawing.Color.Gray;
            this.SeedBox.Location = new System.Drawing.Point(100, 64);
            this.SeedBox.Name = "SeedBox";
            this.SeedBox.Size = new System.Drawing.Size(100, 20);
            this.SeedBox.TabIndex = 3;
            this.SeedBox.Text = "Seed (random)";
            this.SeedBox.TextChanged += new System.EventHandler(this.SeedBox_TextChanged);
            this.SeedBox.Enter += new System.EventHandler(this.SeedBox_Enter);
            this.SeedBox.Leave += new System.EventHandler(this.SeedBox_Leave);
            // 
            // CreateMazeButton
            // 
            this.CreateMazeButton.Location = new System.Drawing.Point(68, 90);
            this.CreateMazeButton.Name = "CreateMazeButton";
            this.CreateMazeButton.Size = new System.Drawing.Size(75, 23);
            this.CreateMazeButton.TabIndex = 0;
            this.CreateMazeButton.Text = "Create Maze";
            this.CreateMazeButton.UseVisualStyleBackColor = true;
            this.CreateMazeButton.Click += new System.EventHandler(this.CreateMazeButton_Click);
            // 
            // ValidationMessageLabel
            // 
            this.ValidationMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.ValidationMessageLabel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ValidationMessageLabel.Location = new System.Drawing.Point(0, 120);
            this.ValidationMessageLabel.Name = "ValidationMessageLabel";
            this.ValidationMessageLabel.Size = new System.Drawing.Size(284, 23);
            this.ValidationMessageLabel.TabIndex = 4;
            this.ValidationMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FindPathButton
            // 
            this.FindPathButton.Enabled = false;
            this.FindPathButton.Location = new System.Drawing.Point(163, 90);
            this.FindPathButton.Name = "FindPathButton";
            this.FindPathButton.Size = new System.Drawing.Size(75, 23);
            this.FindPathButton.TabIndex = 5;
            this.FindPathButton.TabStop = false;
            this.FindPathButton.Text = "Find Path";
            this.FindPathButton.UseVisualStyleBackColor = true;
            this.FindPathButton.Click += new System.EventHandler(this.FindPathButton_Click);
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 151);
            this.Controls.Add(this.FindPathButton);
            this.Controls.Add(this.ValidationMessageLabel);
            this.Controls.Add(this.CreateMazeButton);
            this.Controls.Add(this.SeedBox);
            this.Controls.Add(this.ColBox);
            this.Controls.Add(this.RowBox);
            this.Name = "LauncherForm";
            this.Text = "Launcher";
            this.Load += new System.EventHandler(this.LauncherForm_Load);
            this.Resize += new System.EventHandler(this.LauncherForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RowBox;
        private System.Windows.Forms.TextBox ColBox;
        private System.Windows.Forms.TextBox SeedBox;
        private System.Windows.Forms.Button CreateMazeButton;
        private System.Windows.Forms.Label ValidationMessageLabel;
        private System.Windows.Forms.Button FindPathButton;
    }
}