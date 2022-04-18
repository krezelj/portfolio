namespace BattleshipTechPrototype
{
    partial class BoardForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.BackgroundPainter = new System.ComponentModel.BackgroundWorker();
            this.ClearBoardButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.ValidationMessageLabel = new System.Windows.Forms.Label();
            this.ShowEnemyValuesCheckBox = new System.Windows.Forms.CheckBox();
            this.ShowValueModeBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ClearBoardButton
            // 
            this.ClearBoardButton.Location = new System.Drawing.Point(50, 465);
            this.ClearBoardButton.Name = "ClearBoardButton";
            this.ClearBoardButton.Size = new System.Drawing.Size(75, 23);
            this.ClearBoardButton.TabIndex = 0;
            this.ClearBoardButton.Text = "Clear Board";
            this.ClearBoardButton.UseVisualStyleBackColor = true;
            this.ClearBoardButton.Click += new System.EventHandler(this.ClearBoardButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(131, 465);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 1;
            this.ConfirmButton.Text = "Confirm";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // ValidationMessageLabel
            // 
            this.ValidationMessageLabel.AutoSize = true;
            this.ValidationMessageLabel.Location = new System.Drawing.Point(341, 470);
            this.ValidationMessageLabel.Name = "ValidationMessageLabel";
            this.ValidationMessageLabel.Size = new System.Drawing.Size(0, 13);
            this.ValidationMessageLabel.TabIndex = 2;
            // 
            // ShowEnemyValuesCheckBox
            // 
            this.ShowEnemyValuesCheckBox.AutoSize = true;
            this.ShowEnemyValuesCheckBox.Enabled = false;
            this.ShowEnemyValuesCheckBox.Location = new System.Drawing.Point(212, 469);
            this.ShowEnemyValuesCheckBox.Name = "ShowEnemyValuesCheckBox";
            this.ShowEnemyValuesCheckBox.Size = new System.Drawing.Size(123, 17);
            this.ShowEnemyValuesCheckBox.TabIndex = 3;
            this.ShowEnemyValuesCheckBox.Text = "Show Enemy Values";
            this.ShowEnemyValuesCheckBox.UseVisualStyleBackColor = true;
            this.ShowEnemyValuesCheckBox.CheckedChanged += new System.EventHandler(this.ShowEnemyValuesCheckBox_CheckedChanged);
            // 
            // ShowValueModeBox
            // 
            this.ShowValueModeBox.Location = new System.Drawing.Point(888, 467);
            this.ShowValueModeBox.Name = "ShowValueModeBox";
            this.ShowValueModeBox.Size = new System.Drawing.Size(100, 20);
            this.ShowValueModeBox.TabIndex = 4;
            this.ShowValueModeBox.Text = "1";
            // 
            // BoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.ShowValueModeBox);
            this.Controls.Add(this.ShowEnemyValuesCheckBox);
            this.Controls.Add(this.ValidationMessageLabel);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.ClearBoardButton);
            this.DoubleBuffered = true;
            this.Name = "BoardForm";
            this.Text = "Battleship Prototype";
            this.Load += new System.EventHandler(this.BoardForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.BoardForm_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.BoardForm_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BoardForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BoardForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker BackgroundPainter;
        private System.Windows.Forms.Button ClearBoardButton;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Label ValidationMessageLabel;
        private System.Windows.Forms.CheckBox ShowEnemyValuesCheckBox;
        private System.Windows.Forms.TextBox ShowValueModeBox;
    }
}

