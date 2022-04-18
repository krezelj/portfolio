namespace EvolutionSimulationUI
{
    partial class ControlsForm
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
            this.OpenPlaneButton = new System.Windows.Forms.Button();
            this.OpenChartsButton = new System.Windows.Forms.Button();
            this.DrawSectorsCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawRangeCheckBox = new System.Windows.Forms.CheckBox();
            this.ColorCodeSpeedCheckBox = new System.Windows.Forms.CheckBox();
            this.ColorCodePredatorinessCheckBox = new System.Windows.Forms.CheckBox();
            this.IgnoreSizeCheckBox = new System.Windows.Forms.CheckBox();
            this.GenerationLabel = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.RangeLabel = new System.Windows.Forms.Label();
            this.PredatorinessLabel = new System.Windows.Forms.Label();
            this.CreatureCountLabel = new System.Windows.Forms.Label();
            this.FoodCountLabel = new System.Windows.Forms.Label();
            this.TickLenghtLabel = new System.Windows.Forms.Label();
            this.EnergyLabel = new System.Windows.Forms.Label();
            this.PauseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OpenPlaneButton
            // 
            this.OpenPlaneButton.Location = new System.Drawing.Point(12, 12);
            this.OpenPlaneButton.Name = "OpenPlaneButton";
            this.OpenPlaneButton.Size = new System.Drawing.Size(102, 23);
            this.OpenPlaneButton.TabIndex = 0;
            this.OpenPlaneButton.TabStop = false;
            this.OpenPlaneButton.Text = "Open Plane";
            this.OpenPlaneButton.UseVisualStyleBackColor = true;
            this.OpenPlaneButton.Click += new System.EventHandler(this.OpenPlaneButton_Click);
            // 
            // OpenChartsButton
            // 
            this.OpenChartsButton.Location = new System.Drawing.Point(120, 12);
            this.OpenChartsButton.Name = "OpenChartsButton";
            this.OpenChartsButton.Size = new System.Drawing.Size(102, 23);
            this.OpenChartsButton.TabIndex = 1;
            this.OpenChartsButton.TabStop = false;
            this.OpenChartsButton.Text = "Open Charts";
            this.OpenChartsButton.UseVisualStyleBackColor = true;
            this.OpenChartsButton.Click += new System.EventHandler(this.OpenChartsButton_Click);
            // 
            // DrawSectorsCheckBox
            // 
            this.DrawSectorsCheckBox.AutoSize = true;
            this.DrawSectorsCheckBox.Location = new System.Drawing.Point(12, 41);
            this.DrawSectorsCheckBox.Name = "DrawSectorsCheckBox";
            this.DrawSectorsCheckBox.Size = new System.Drawing.Size(90, 17);
            this.DrawSectorsCheckBox.TabIndex = 2;
            this.DrawSectorsCheckBox.Text = "Draw Sectors";
            this.DrawSectorsCheckBox.UseVisualStyleBackColor = true;
            this.DrawSectorsCheckBox.CheckedChanged += new System.EventHandler(this.DrawSectorsCheckBox_CheckedChanged);
            // 
            // DrawRangeCheckBox
            // 
            this.DrawRangeCheckBox.AutoSize = true;
            this.DrawRangeCheckBox.Location = new System.Drawing.Point(12, 64);
            this.DrawRangeCheckBox.Name = "DrawRangeCheckBox";
            this.DrawRangeCheckBox.Size = new System.Drawing.Size(86, 17);
            this.DrawRangeCheckBox.TabIndex = 3;
            this.DrawRangeCheckBox.Text = "Draw Range";
            this.DrawRangeCheckBox.UseVisualStyleBackColor = true;
            this.DrawRangeCheckBox.CheckedChanged += new System.EventHandler(this.DrawRangeCheckBox_CheckedChanged);
            // 
            // ColorCodeSpeedCheckBox
            // 
            this.ColorCodeSpeedCheckBox.AutoSize = true;
            this.ColorCodeSpeedCheckBox.Location = new System.Drawing.Point(12, 87);
            this.ColorCodeSpeedCheckBox.Name = "ColorCodeSpeedCheckBox";
            this.ColorCodeSpeedCheckBox.Size = new System.Drawing.Size(112, 17);
            this.ColorCodeSpeedCheckBox.TabIndex = 4;
            this.ColorCodeSpeedCheckBox.Text = "Color Code Speed";
            this.ColorCodeSpeedCheckBox.UseVisualStyleBackColor = true;
            this.ColorCodeSpeedCheckBox.CheckedChanged += new System.EventHandler(this.ColorCodeSpeedCheckBox_CheckedChanged);
            // 
            // ColorCodePredatorinessCheckBox
            // 
            this.ColorCodePredatorinessCheckBox.AutoSize = true;
            this.ColorCodePredatorinessCheckBox.Checked = true;
            this.ColorCodePredatorinessCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ColorCodePredatorinessCheckBox.Location = new System.Drawing.Point(12, 110);
            this.ColorCodePredatorinessCheckBox.Name = "ColorCodePredatorinessCheckBox";
            this.ColorCodePredatorinessCheckBox.Size = new System.Drawing.Size(145, 17);
            this.ColorCodePredatorinessCheckBox.TabIndex = 5;
            this.ColorCodePredatorinessCheckBox.Text = "Color Code Predatoriness";
            this.ColorCodePredatorinessCheckBox.UseVisualStyleBackColor = true;
            this.ColorCodePredatorinessCheckBox.CheckedChanged += new System.EventHandler(this.ColorCodePredatorinessCheckBox_CheckedChanged);
            // 
            // IgnoreSizeCheckBox
            // 
            this.IgnoreSizeCheckBox.AutoSize = true;
            this.IgnoreSizeCheckBox.Location = new System.Drawing.Point(12, 133);
            this.IgnoreSizeCheckBox.Name = "IgnoreSizeCheckBox";
            this.IgnoreSizeCheckBox.Size = new System.Drawing.Size(79, 17);
            this.IgnoreSizeCheckBox.TabIndex = 6;
            this.IgnoreSizeCheckBox.Text = "Ignore Size";
            this.IgnoreSizeCheckBox.UseVisualStyleBackColor = true;
            this.IgnoreSizeCheckBox.CheckedChanged += new System.EventHandler(this.IgnoreSizeCheckBox_CheckedChanged);
            // 
            // GenerationLabel
            // 
            this.GenerationLabel.AutoSize = true;
            this.GenerationLabel.Location = new System.Drawing.Point(9, 212);
            this.GenerationLabel.Name = "GenerationLabel";
            this.GenerationLabel.Size = new System.Drawing.Size(62, 13);
            this.GenerationLabel.TabIndex = 7;
            this.GenerationLabel.Text = "Generation:";
            // 
            // SizeLabel
            // 
            this.SizeLabel.AutoSize = true;
            this.SizeLabel.Location = new System.Drawing.Point(9, 238);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(30, 13);
            this.SizeLabel.TabIndex = 8;
            this.SizeLabel.Text = "Size:";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(9, 251);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(41, 13);
            this.SpeedLabel.TabIndex = 9;
            this.SpeedLabel.Text = "Speed:";
            // 
            // RangeLabel
            // 
            this.RangeLabel.AutoSize = true;
            this.RangeLabel.Location = new System.Drawing.Point(9, 264);
            this.RangeLabel.Name = "RangeLabel";
            this.RangeLabel.Size = new System.Drawing.Size(42, 13);
            this.RangeLabel.TabIndex = 10;
            this.RangeLabel.Text = "Range:";
            // 
            // PredatorinessLabel
            // 
            this.PredatorinessLabel.AutoSize = true;
            this.PredatorinessLabel.Location = new System.Drawing.Point(9, 277);
            this.PredatorinessLabel.Name = "PredatorinessLabel";
            this.PredatorinessLabel.Size = new System.Drawing.Size(74, 13);
            this.PredatorinessLabel.TabIndex = 11;
            this.PredatorinessLabel.Text = "Predatoriness:";
            // 
            // CreatureCountLabel
            // 
            this.CreatureCountLabel.AutoSize = true;
            this.CreatureCountLabel.Location = new System.Drawing.Point(9, 164);
            this.CreatureCountLabel.Name = "CreatureCountLabel";
            this.CreatureCountLabel.Size = new System.Drawing.Size(81, 13);
            this.CreatureCountLabel.TabIndex = 12;
            this.CreatureCountLabel.Text = "Creature Count:";
            // 
            // FoodCountLabel
            // 
            this.FoodCountLabel.AutoSize = true;
            this.FoodCountLabel.Location = new System.Drawing.Point(9, 177);
            this.FoodCountLabel.Name = "FoodCountLabel";
            this.FoodCountLabel.Size = new System.Drawing.Size(65, 13);
            this.FoodCountLabel.TabIndex = 13;
            this.FoodCountLabel.Text = "Food Count:";
            // 
            // TickLenghtLabel
            // 
            this.TickLenghtLabel.AutoSize = true;
            this.TickLenghtLabel.Location = new System.Drawing.Point(9, 190);
            this.TickLenghtLabel.Name = "TickLenghtLabel";
            this.TickLenghtLabel.Size = new System.Drawing.Size(67, 13);
            this.TickLenghtLabel.TabIndex = 14;
            this.TickLenghtLabel.Text = "Tick Lenght:";
            // 
            // EnergyLabel
            // 
            this.EnergyLabel.AutoSize = true;
            this.EnergyLabel.Location = new System.Drawing.Point(9, 225);
            this.EnergyLabel.Name = "EnergyLabel";
            this.EnergyLabel.Size = new System.Drawing.Size(43, 13);
            this.EnergyLabel.TabIndex = 15;
            this.EnergyLabel.Text = "Energy:";
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(82, 465);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 16;
            this.PauseButton.Text = "Start";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ControlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 500);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.EnergyLabel);
            this.Controls.Add(this.TickLenghtLabel);
            this.Controls.Add(this.FoodCountLabel);
            this.Controls.Add(this.CreatureCountLabel);
            this.Controls.Add(this.PredatorinessLabel);
            this.Controls.Add(this.RangeLabel);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.GenerationLabel);
            this.Controls.Add(this.IgnoreSizeCheckBox);
            this.Controls.Add(this.ColorCodePredatorinessCheckBox);
            this.Controls.Add(this.ColorCodeSpeedCheckBox);
            this.Controls.Add(this.DrawRangeCheckBox);
            this.Controls.Add(this.DrawSectorsCheckBox);
            this.Controls.Add(this.OpenChartsButton);
            this.Controls.Add(this.OpenPlaneButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ControlsForm";
            this.ShowIcon = false;
            this.Text = "Controls";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlsForm_FormClosing);
            this.Load += new System.EventHandler(this.ControlsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenPlaneButton;
        private System.Windows.Forms.Button OpenChartsButton;
        private System.Windows.Forms.CheckBox DrawSectorsCheckBox;
        private System.Windows.Forms.CheckBox DrawRangeCheckBox;
        private System.Windows.Forms.CheckBox ColorCodeSpeedCheckBox;
        private System.Windows.Forms.CheckBox ColorCodePredatorinessCheckBox;
        private System.Windows.Forms.CheckBox IgnoreSizeCheckBox;
        private System.Windows.Forms.Label GenerationLabel;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Label RangeLabel;
        private System.Windows.Forms.Label PredatorinessLabel;
        private System.Windows.Forms.Label CreatureCountLabel;
        private System.Windows.Forms.Label FoodCountLabel;
        private System.Windows.Forms.Label TickLenghtLabel;
        private System.Windows.Forms.Label EnergyLabel;
        private System.Windows.Forms.Button PauseButton;
    }
}