using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipTechPrototypeLIB
{
    /// <summary>
    /// A static instance of the GameManager class. Manages the game and holds its properties.
    /// </summary>
    public static class GameManager
    {
        //
        // PROPERTIES
        //

        public static Form Canvas;

        /// <summary>
        /// Stage of the game.
        /// </summary>
        public static int Stage = 0;

        /// <summary>
        /// Determines whether the game is finished or not.
        /// </summary>
        public static bool IsFinished = false;

        /// <summary>
        /// State of the first player's board.
        /// </summary>
        public static BoardModel Player1Board;

        /// <summary>
        /// State of the second player's board.
        /// </summary>
        public static BoardModel Player2Board;

        public static Enemy Enemy1;

        public static Enemy Enemy2;

        public static int player1wins = 0;
        
        public static int player2wins = 0;

        //
        // METHODS
        //

        /// <summary>
        /// Initializes the game.
        /// </summary>
        public static void InitializeGame()
        {
            Player1Board = new BoardModel(new Size(400, 400), new Point(50, 50), 10, 10);
            Player2Board = new BoardModel(new Size(400, 400), new Point(550, 50), 10, 10);
            Enemy1 = new Enemy(Player2Board);
            Enemy2 = new Enemy(Player1Board);
            Stage = 0;
            AdvanceGame();
            //SimulateGame();
        }

        public static void SimulateGame()
        {
            Player1Board.EditMode = true;
            Player2Board.EditMode = false;
            Enemy1.PlaceShips(Player1Board);
            // MessageBox.Show("First player placed ships");
            //Stage++;

            Player1Board.EditMode = false;
            Player2Board.EditMode = true;
            Enemy2.PlaceShips(Player2Board);
            // MessageBox.Show("Second player placed ships");

            //Stage++;
            Player1Board.EditMode = false;
            Player2Board.EditMode = false;
            Player1Board.PlayMode = true;
            Player2Board.PlayMode = false;

            while (true)
            {
                if (CheckIfGameFinished())
                {
                    player2wins++;
                    break;
                }

                Stage++;
                int x;
                int y;
                Enemy1.MakeMove(out x, out y, Player2Board);
                //EnemyOld.MakeMove(out x, out y, Player2Board);
                // MessageBox.Show($"{ x }, { y }");
                Player2Board.HitCell(x, y, false);
                Player1Board.PlayMode = false;
                Player2Board.PlayMode = true;
                //Stage++;

                if (CheckIfGameFinished())
                {
                    player1wins++;
                    break;
                    
                }

                //Enemy2.MakeMove(out x, out y, Player1Board);
                EnemyOld.MakeMove(out x, out y, Player1Board);
                // MessageBox.Show($"{ x }, { y }");
                Player1Board.HitCell(x, y, false);
                Player1Board.PlayMode = true;
                Player2Board.PlayMode = false;

                Canvas.Refresh();
                //Stage++;
            }
            //MessageBox.Show("The Game is finished.");
            Player1Board.PlayMode = true;
            Player2Board.PlayMode = true;
        }


        /// <summary>
        /// Advances the game.
        /// </summary>
        public static void AdvanceGame()
        {
            Stage++;
            //MessageBox.Show($"Game advanced to stage: { Stage }.");
            switch (Stage)
            {
                case 1:
                    MessageBox.Show("Place the first player's ships.");
                    Player1Board.EditMode = true;
                    Player2Board.EditMode = false;

                    Player1Board.PlayMode = false;
                    Player2Board.PlayMode = false;
                    break;
                case 2:
                    Player1Board.EditMode = false;
                    Player2Board.EditMode = true;
                    Enemy2.PlaceShips(Player2Board);
                    Stage++;

                    Player1Board.EditMode = false;
                    Player2Board.EditMode = false;
                    Player1Board.PlayMode = true;
                    Player2Board.PlayMode = false;
                    MessageBox.Show("The game has started.");
                    break;
                default:
                    if (CheckIfGameFinished())
                    {
                        MessageBox.Show("The Game is finished.");
                        Player1Board.PlayMode = true;
                        Player2Board.PlayMode = true;
                        //InitializeGame();
                        break;
                    }
                    if (Stage % 2 == 0)
                    {
                        Enemy2.MakeMove(out int x, out int y, Player1Board);
                        //EnemyOld.MakeMove(out int x, out int y, Player1Board);
                        Player1Board.HitCell(x, y, false);
                        Player1Board.PlayMode = true;
                        Player2Board.PlayMode = false;
                        Stage++;
                    }
                    if (CheckIfGameFinished())
                    {
                        MessageBox.Show("The Game is finished.");
                        Player1Board.PlayMode = true;
                        Player2Board.PlayMode = true;
                        //InitializeGame();
                        break;
                    }
                    else
                    {
                        Player1Board.PlayMode = true;
                        Player2Board.PlayMode = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// Checks if the game is finished.
        /// </summary>
        /// <returns></returns>
        static bool CheckIfGameFinished()
        {
            bool isFinished = true;
            foreach (ShipModel ship in Player1Board.Ships)
            {
                if (!ship.IsSunken)
                {
                    isFinished = false;
                }
            }
            if (isFinished)
            {
                IsFinished = true;
                return true;
            }
            isFinished = true;
            foreach (ShipModel ship in Player2Board.Ships)
            {
                if (!ship.IsSunken)
                {
                    isFinished = false;
                }
            }
            if (isFinished)
            {
                IsFinished = true;
                return true;
            }
            return false;
        }
    }
}
