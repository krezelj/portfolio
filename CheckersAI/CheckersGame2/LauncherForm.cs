using CheckersGame2LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame2
{
    public partial class LauncherForm : Form
    {
        public LauncherForm()
        {
            InitializeComponent();
        }

        private void LauncherForm_Load(object sender, EventArgs e)
        {
            PlayerOnePickerBox.Items.Add("Human");
            PlayerOnePickerBox.Items.Add("Computer Easy");
            PlayerOnePickerBox.Items.Add("Computer Medium");
            PlayerOnePickerBox.Items.Add("Computer Hard");
            PlayerOnePickerBox.SelectedText = "Human";

            PlayerTwoPickerBox.Items.Add("Human");
            PlayerTwoPickerBox.Items.Add("Computer Easy");
            PlayerTwoPickerBox.Items.Add("Computer Medium");
            PlayerTwoPickerBox.Items.Add("Computer Hard");
            PlayerTwoPickerBox.SelectedText = "Computer Hard";
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Manager.PlayerOneType = PlayerType.Computer;
            switch (PlayerOnePickerBox.Text)
            {
                case "Human":
                    Manager.PlayerOneType = PlayerType.Human;
                    break;
                case "Computer Easy":
                    Manager.PlayerOneDifficulty = 2;
                    break;
                case "Computer Medium":
                    Manager.PlayerOneDifficulty = 3;
                    break;
                case "Computer Hard":
                    Manager.PlayerOneDifficulty = 4;
                    break;
            }

            Manager.PlayerTwoType = PlayerType.Computer;
            switch (PlayerTwoPickerBox.Text)
            {
                case "Human":
                    Manager.PlayerTwoType = PlayerType.Human;
                    break;
                case "Computer Easy":
                    Manager.PlayerTwoDifficulty = 2;
                    break;
                case "Computer Medium":
                    Manager.PlayerTwoDifficulty = 3;
                    break;
                case "Computer Hard":
                    Manager.PlayerTwoDifficulty = 4;
                    break;
            }
            BoardForm boardForm = new BoardForm();
            Manager.BoardCanvas = boardForm;
            Manager.LauncherPlayButton = PlayButton;
            CanvasManager.BoardCanvas = boardForm;
            boardForm.LauncherPlayButton = PlayButton;
            PlayButton.Enabled = false;
            boardForm.Show();
        }
    }
}
