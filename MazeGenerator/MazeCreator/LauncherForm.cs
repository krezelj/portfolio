using MazeCreatorLIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeCreator
{
    public partial class LauncherForm : Form
    {
        PathFinder pathFinder = new PathFinder();

        public LauncherForm()
        {
            InitializeComponent();
        }

        private void LauncherForm_Load(object sender, EventArgs e)
        {
            Manager.CreateMazeButton = this.CreateMazeButton;
            Manager.FindPathButton = this.FindPathButton;
            RowBox.Location = new Point((ClientSize.Width - RowBox.Width) / 2, RowBox.Location.Y);
            ColBox.Location = new Point((ClientSize.Width - ColBox.Width) / 2, ColBox.Location.Y);
            SeedBox.Location = new Point((ClientSize.Width - SeedBox.Width) / 2, SeedBox.Location.Y);
            int buttonMargin = 10;
            CreateMazeButton.Location = new Point((ClientSize.Width - (CreateMazeButton.Width + FindPathButton.Width + buttonMargin)) / 2, CreateMazeButton.Location.Y);
            FindPathButton.Location = new Point(CreateMazeButton.Location.X + CreateMazeButton.Width + buttonMargin, CreateMazeButton.Location.Y);
            ValidationMessageLabel.Location = new Point((ClientSize.Width - ValidationMessageLabel.Width) / 2, ValidationMessageLabel.Location.Y);
        }

        private void RowBox_Enter(object sender, EventArgs e)
        {
            if (RowBox.Text == "Number of rows")
            {
                RowBox.Text = "";
            }
        }

        private void ColBox_Enter(object sender, EventArgs e)
        {
            if (ColBox.Text == "Number of columns")
            {
                ColBox.Text = "";
            }
        }

        private void SeedBox_Enter(object sender, EventArgs e)
        {
            if (SeedBox.Text == "Seed (random)")
            {
                SeedBox.Text = "";
            }
        }

        private void RowBox_Leave(object sender, EventArgs e)
        {
            if (RowBox.Text == "")
            {  
                RowBox.Text = "Number of rows";
                RowBox.ForeColor = Color.Gray;
            }
        }

        private void ColBox_Leave(object sender, EventArgs e)
        {
            if (ColBox.Text == "")
            {
                ColBox.Text = "Number of columns";
                ColBox.ForeColor = Color.Gray;
            }
        }

        private void SeedBox_Leave(object sender, EventArgs e)
        {
            if (SeedBox.Text == "")
            {
                SeedBox.Text = "Seed (random)";
                SeedBox.ForeColor = Color.Gray;
            }
        }

        private void RowBox_TextChanged(object sender, EventArgs e)
        {

            RowBox.ForeColor = Color.Black;
        }

        private void ColBox_TextChanged(object sender, EventArgs e)
        {
            ColBox.ForeColor = Color.Black;
        }

        private void SeedBox_TextChanged(object sender, EventArgs e)
        {
            SeedBox.ForeColor = Color.Black;
        }

        private void CreateMazeButton_Click(object sender, EventArgs e)
        {
            if (Manager.ValidateInput(RowBox.Text, ColBox.Text, SeedBox.Text, out string validationMessage))
            {
                CreateMazeButton.Enabled = false;
                FindPathButton.Enabled = false;
                ValidationMessageLabel.Text = validationMessage;
                SeedBox.Text = Manager.Seed.ToString();
                Manager.Maze = new Maze(Manager.Rows, Manager.Cols, Manager.Seed);
                MazeForm mazeForm = new MazeForm();
                mazeForm.Show();
            }
            else
            {
                ValidationMessageLabel.Text = validationMessage;
            }
        }

        private void FindPathButton_Click(object sender, EventArgs e)
        {
            if(pathFinder.FindPath())
            {
                // MessageBox.Show("Success");
                MessageBox.Show($"Path found in: {pathFinder.SW.Elapsed.Milliseconds }ms.");
            }
            else
            {
                // MessageBox.Show("Failure");
            }
            Manager.MazeForm.Refresh();
        }

        private void LauncherForm_Resize(object sender, EventArgs e)
        {
            RowBox.Location = new Point((ClientSize.Width - RowBox.Width) / 2, RowBox.Location.Y);
            ColBox.Location = new Point((ClientSize.Width - ColBox.Width) / 2, ColBox.Location.Y);
            SeedBox.Location = new Point((ClientSize.Width - SeedBox.Width) / 2, SeedBox.Location.Y);
            int buttonMargin = 10;
            CreateMazeButton.Location = new Point((ClientSize.Width - (CreateMazeButton.Width + FindPathButton.Width + buttonMargin)) / 2, CreateMazeButton.Location.Y);
            FindPathButton.Location = new Point(CreateMazeButton.Location.X + CreateMazeButton.Width + buttonMargin, CreateMazeButton.Location.Y);
            ValidationMessageLabel.Location = new Point((ClientSize.Width - ValidationMessageLabel.Width) / 2, ValidationMessageLabel.Location.Y);
        }
    }
}
