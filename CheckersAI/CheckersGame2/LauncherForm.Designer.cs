namespace CheckersGame2
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
            this.PlayerOnePickerBox = new System.Windows.Forms.ComboBox();
            this.PlayerTwoPickerBox = new System.Windows.Forms.ComboBox();
            this.PlayerOnePickerLabel = new System.Windows.Forms.Label();
            this.PlayerTwoPickerLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PlayerOnePickerBox
            // 
            this.PlayerOnePickerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlayerOnePickerBox.FormattingEnabled = true;
            this.PlayerOnePickerBox.Location = new System.Drawing.Point(12, 43);
            this.PlayerOnePickerBox.Name = "PlayerOnePickerBox";
            this.PlayerOnePickerBox.Size = new System.Drawing.Size(121, 21);
            this.PlayerOnePickerBox.TabIndex = 0;
            this.PlayerOnePickerBox.TabStop = false;
            // 
            // PlayerTwoPickerBox
            // 
            this.PlayerTwoPickerBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlayerTwoPickerBox.FormattingEnabled = true;
            this.PlayerTwoPickerBox.Location = new System.Drawing.Point(164, 43);
            this.PlayerTwoPickerBox.Name = "PlayerTwoPickerBox";
            this.PlayerTwoPickerBox.Size = new System.Drawing.Size(121, 21);
            this.PlayerTwoPickerBox.TabIndex = 1;
            this.PlayerTwoPickerBox.TabStop = false;
            // 
            // PlayerOnePickerLabel
            // 
            this.PlayerOnePickerLabel.AutoSize = true;
            this.PlayerOnePickerLabel.ForeColor = System.Drawing.Color.White;
            this.PlayerOnePickerLabel.Location = new System.Drawing.Point(12, 27);
            this.PlayerOnePickerLabel.Name = "PlayerOnePickerLabel";
            this.PlayerOnePickerLabel.Size = new System.Drawing.Size(62, 13);
            this.PlayerOnePickerLabel.TabIndex = 2;
            this.PlayerOnePickerLabel.Text = "Player One:";
            // 
            // PlayerTwoPickerLabel
            // 
            this.PlayerTwoPickerLabel.AutoSize = true;
            this.PlayerTwoPickerLabel.ForeColor = System.Drawing.Color.White;
            this.PlayerTwoPickerLabel.Location = new System.Drawing.Point(161, 27);
            this.PlayerTwoPickerLabel.Name = "PlayerTwoPickerLabel";
            this.PlayerTwoPickerLabel.Size = new System.Drawing.Size(63, 13);
            this.PlayerTwoPickerLabel.TabIndex = 3;
            this.PlayerTwoPickerLabel.Text = "Player Two:";
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(109, 87);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(75, 23);
            this.PlayButton.TabIndex = 5;
            this.PlayButton.TabStop = false;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(297, 132);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.PlayerTwoPickerLabel);
            this.Controls.Add(this.PlayerOnePickerLabel);
            this.Controls.Add(this.PlayerTwoPickerBox);
            this.Controls.Add(this.PlayerOnePickerBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LauncherForm";
            this.Text = "Checkers Launcher";
            this.Load += new System.EventHandler(this.LauncherForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PlayerOnePickerBox;
        private System.Windows.Forms.ComboBox PlayerTwoPickerBox;
        private System.Windows.Forms.Label PlayerOnePickerLabel;
        private System.Windows.Forms.Label PlayerTwoPickerLabel;
        private System.Windows.Forms.Button PlayButton;
    }
}