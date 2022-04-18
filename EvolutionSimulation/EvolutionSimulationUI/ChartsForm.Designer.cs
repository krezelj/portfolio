namespace EvolutionSimulationUI
{
    partial class ChartsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.ChartOne = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartTwo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartThree = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartFour = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChartOne)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTwo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartThree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFour)).BeginInit();
            this.SuspendLayout();
            // 
            // ChartOne
            // 
            chartArea1.Name = "ChartArea1";
            this.ChartOne.ChartAreas.Add(chartArea1);
            this.ChartOne.Location = new System.Drawing.Point(12, 12);
            this.ChartOne.Name = "ChartOne";
            this.ChartOne.Size = new System.Drawing.Size(596, 200);
            this.ChartOne.TabIndex = 0;
            this.ChartOne.Text = "Chart One";
            // 
            // ChartTwo
            // 
            chartArea2.Name = "ChartArea1";
            this.ChartTwo.ChartAreas.Add(chartArea2);
            this.ChartTwo.Location = new System.Drawing.Point(12, 218);
            this.ChartTwo.Name = "ChartTwo";
            this.ChartTwo.Size = new System.Drawing.Size(596, 200);
            this.ChartTwo.TabIndex = 1;
            this.ChartTwo.Text = "Chart Two";
            // 
            // ChartThree
            // 
            chartArea3.Name = "ChartArea1";
            this.ChartThree.ChartAreas.Add(chartArea3);
            this.ChartThree.Location = new System.Drawing.Point(12, 424);
            this.ChartThree.Name = "ChartThree";
            this.ChartThree.Size = new System.Drawing.Size(596, 200);
            this.ChartThree.TabIndex = 2;
            this.ChartThree.Text = "Chart Three";
            // 
            // ChartFour
            // 
            chartArea4.Name = "ChartArea1";
            this.ChartFour.ChartAreas.Add(chartArea4);
            this.ChartFour.Location = new System.Drawing.Point(12, 630);
            this.ChartFour.Name = "ChartFour";
            this.ChartFour.Size = new System.Drawing.Size(596, 200);
            this.ChartFour.TabIndex = 3;
            this.ChartFour.Text = "Chart Four";
            // 
            // ChartsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 1008);
            this.Controls.Add(this.ChartFour);
            this.Controls.Add(this.ChartThree);
            this.Controls.Add(this.ChartTwo);
            this.Controls.Add(this.ChartOne);
            this.Name = "ChartsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Charts";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChartsForm_FormClosing);
            this.Load += new System.EventHandler(this.ChartsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChartOne)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTwo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartThree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartFour)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChartOne;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartTwo;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartThree;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartFour;
    }
}