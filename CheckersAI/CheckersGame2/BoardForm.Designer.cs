namespace CheckersGame2
{
    partial class BoardForm
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
            this.NodesExploresLabel = new System.Windows.Forms.Label();
            this.NodesCountLabel = new System.Windows.Forms.Label();
            this.OutcomesExploredLabel = new System.Windows.Forms.Label();
            this.OutcomesCountLabel = new System.Windows.Forms.Label();
            this.BoardValueLabel = new System.Windows.Forms.Label();
            this.BoardValueNumberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NodesExploresLabel
            // 
            this.NodesExploresLabel.AutoSize = true;
            this.NodesExploresLabel.ForeColor = System.Drawing.Color.White;
            this.NodesExploresLabel.Location = new System.Drawing.Point(12, 778);
            this.NodesExploresLabel.Name = "NodesExploresLabel";
            this.NodesExploresLabel.Size = new System.Drawing.Size(85, 13);
            this.NodesExploresLabel.TabIndex = 0;
            this.NodesExploresLabel.Text = "Nodes Explored:";
            // 
            // NodesCountLabel
            // 
            this.NodesCountLabel.AutoSize = true;
            this.NodesCountLabel.ForeColor = System.Drawing.Color.White;
            this.NodesCountLabel.Location = new System.Drawing.Point(103, 778);
            this.NodesCountLabel.Name = "NodesCountLabel";
            this.NodesCountLabel.Size = new System.Drawing.Size(13, 13);
            this.NodesCountLabel.TabIndex = 1;
            this.NodesCountLabel.Text = "0";
            // 
            // OutcomesExploredLabel
            // 
            this.OutcomesExploredLabel.AutoSize = true;
            this.OutcomesExploredLabel.ForeColor = System.Drawing.Color.White;
            this.OutcomesExploredLabel.Location = new System.Drawing.Point(197, 778);
            this.OutcomesExploredLabel.Name = "OutcomesExploredLabel";
            this.OutcomesExploredLabel.Size = new System.Drawing.Size(102, 13);
            this.OutcomesExploredLabel.TabIndex = 2;
            this.OutcomesExploredLabel.Text = "Outcomes Explored:";
            // 
            // OutcomesCountLabel
            // 
            this.OutcomesCountLabel.AutoSize = true;
            this.OutcomesCountLabel.ForeColor = System.Drawing.Color.White;
            this.OutcomesCountLabel.Location = new System.Drawing.Point(305, 778);
            this.OutcomesCountLabel.Name = "OutcomesCountLabel";
            this.OutcomesCountLabel.Size = new System.Drawing.Size(13, 13);
            this.OutcomesCountLabel.TabIndex = 3;
            this.OutcomesCountLabel.Text = "0";
            // 
            // BoardValueLabel
            // 
            this.BoardValueLabel.AutoSize = true;
            this.BoardValueLabel.ForeColor = System.Drawing.Color.White;
            this.BoardValueLabel.Location = new System.Drawing.Point(399, 778);
            this.BoardValueLabel.Name = "BoardValueLabel";
            this.BoardValueLabel.Size = new System.Drawing.Size(68, 13);
            this.BoardValueLabel.TabIndex = 4;
            this.BoardValueLabel.Text = "Board Value:";
            // 
            // BoardValueNumberLabel
            // 
            this.BoardValueNumberLabel.AutoSize = true;
            this.BoardValueNumberLabel.ForeColor = System.Drawing.Color.White;
            this.BoardValueNumberLabel.Location = new System.Drawing.Point(473, 778);
            this.BoardValueNumberLabel.Name = "BoardValueNumberLabel";
            this.BoardValueNumberLabel.Size = new System.Drawing.Size(13, 13);
            this.BoardValueNumberLabel.TabIndex = 5;
            this.BoardValueNumberLabel.Text = "0";
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(this.BoardValueNumberLabel);
            this.Controls.Add(this.BoardValueLabel);
            this.Controls.Add(this.OutcomesCountLabel);
            this.Controls.Add(this.OutcomesExploredLabel);
            this.Controls.Add(this.NodesCountLabel);
            this.Controls.Add(this.NodesExploresLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BoardForm";
            this.Text = "Checkers Game";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BoardForm_FormClosed);
            this.Load += new System.EventHandler(this.BoardForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NodesExploresLabel;
        private System.Windows.Forms.Label NodesCountLabel;
        private System.Windows.Forms.Label OutcomesExploredLabel;
        private System.Windows.Forms.Label OutcomesCountLabel;
        private System.Windows.Forms.Label BoardValueLabel;
        private System.Windows.Forms.Label BoardValueNumberLabel;
    }
}

