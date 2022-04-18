using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipTechPrototypeLIB
{
    public static class EnemyOld
    {
        //
        // PROPERTIES
        //

        static Random Rng = new Random();

        public static int[,] ValueBoard = new int[GameManager.Player1Board.NoRows, GameManager.Player1Board.NoCols];

        static int biasMax = 2;

        static int Bias = Rng.Next(0, biasMax);

        //
        // METHODS
        //

        public static void MakeMove(out int x, out int y, BoardModel board)
        {
            EvaluateBoard(board);
            int maxValue = -100;
            x = 0;
            y = 0;
            for (int i = 0; i < board.NoRows; i++)
            {
                for (int j = 0; j < board.NoCols; j++)
                {
                    if (ValueBoard[i, j] > maxValue)
                    {
                        x = j;
                        y = i;
                        maxValue = ValueBoard[i, j];
                    }
                }
            }
        }

        public static void PlaceShips(BoardModel board)
        {
            int x;
            int y;
            do
            {
                board.ClearBoard();
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 4 - i; j <= 4; j++)
                    {
                        bool isValid;
                        int shipSize = 4 - i;
                        if (Rng.Next(0, 2) == 0)
                        {
                            do
                            {
                                isValid = true;
                                x = Rng.Next(0, board.NoCols - shipSize);
                                y = Rng.Next(0, board.NoRows - shipSize);
                                for (int k = 0; k < shipSize; k++)
                                {
                                    if (board.Cells[y, x + k].HasShip)
                                    {
                                        isValid = false;
                                    }
                                    board.UpdateShips(x + k, y, false);
                                }
                                if (!board.SoftValidateBoard(out string validationMessage) || !isValid)
                                {
                                    isValid = false;
                                    //MessageBox.Show(validationMessage);
                                    for (int k = 0; k < shipSize; k++)
                                    {
                                        board.UpdateShips(x + k, y, false);
                                    }
                                }
                            } while (!isValid);
                        }
                        else
                        {
                            do
                            {
                                isValid = true;
                                x = Rng.Next(0, board.NoCols - shipSize);
                                y = Rng.Next(0, board.NoCols - shipSize);
                                for (int k = 0; k < shipSize; k++)
                                {
                                    if (board.Cells[y + k, x].HasShip)
                                    {
                                        isValid = false;
                                    }
                                    board.UpdateShips(x, y + k, false);
                                }
                                if (!board.SoftValidateBoard(out string validationMessage) || !isValid)
                                {
                                    isValid = false;
                                    //MessageBox.Show(validationMessage);
                                    for (int k = 0; k < shipSize; k++)
                                    {
                                        board.UpdateShips(x, y + k, false);
                                    }
                                }
                            } while (!isValid);
                        }
                    }
                }
            } while (!board.HardValidateBoard(out string vm));
        }

        public static void EvaluateBoard(BoardModel board)
        {
            ValueBoard = new int[board.NoRows, board.NoCols];
            int multiplier = 1;
            for (int i = 0; i < board.NoRows; ++i)
            {
                for (int j = 0; j < board.NoCols; ++j)
                {
                    // if ((i * board.NoCols + j + Bias) % biasMax == 0)
                    // {
                    //     ValueBoard[i, j] += 2;
                    // }
                    // ValueBoard[i, j] += biasMax - (i * board.NoCols + j + Bias) % biasMax;
                    if (i % 2 == j % 2)
                    {
                        ValueBoard[i, j] += 2;
                    }
                    if (board.Cells[i, j].IsHit)
                    {
                        ValueBoard[i, j] -= 10;
                        if (board.Cells[i, j].HasShip)
                        { 
                            if (board.Cells[i, j].ParentShip.IsSunken)
                            {
                                multiplier = -2;
                            }
                            else
                            {
                                multiplier = 1;
                            }
                            //=========================================
                            if (i > 0)
                            {
                                ValueBoard[i - 1, j] += 4 * multiplier;
                            }
                            //=========================================
                            if (i < board.NoRows - 1)
                            {
                                ValueBoard[i + 1, j] += 5 * multiplier;
                            }
                            //=========================================
                            if (j > 0)
                            {
                                ValueBoard[i, j - 1] += 4 * multiplier;
                            }
                            //=========================================
                            if (j < board.NoCols - 1)
                            {
                                ValueBoard[i, j + 1] += 5 * multiplier;
                            }
                            //=========================================
                            if (i > 0 && j > 0)
                            {
                                ValueBoard[i - 1, j - 1] -= 6;
                            }
                            //=========================================
                            if (i < board.NoRows - 1 && j < board.NoCols - 1)
                            {
                                ValueBoard[i + 1, j + 1] -= 6;
                            }
                            //=========================================
                            if (i > 0 && j < board.NoCols - 1)
                            {
                                ValueBoard[i - 1, j + 1] -= 6;
                            }
                            //=========================================
                            if (i < board.NoRows - 1 && j > 0)
                            {
                                ValueBoard[i + 1, j - 1] -= 6;
                            }
                        }
                    }
                }
            }
        }

        public static void ShowValues()
        {
            EvaluateBoard(GameManager.Player1Board);
            int minVal = 100;
            int maxVal = -100;
            double val = 0;
            for (int i = 0; i < GameManager.Player1Board.NoRows; i++)
            {
                for (int j = 0; j < GameManager.Player1Board.NoCols; j++)
                {
                    if (ValueBoard[i, j] < minVal)
                    {
                        minVal = ValueBoard[i, j];
                    }
                    if (ValueBoard[i, j] > maxVal)
                    {
                        maxVal = ValueBoard[i, j];
                    }
                }
            }
            for (int i = 0; i < GameManager.Player1Board.NoRows; i++)
            {
                for (int j = 0; j < GameManager.Player1Board.NoCols; ++j)
                {
                    val = (double)(ValueBoard[i, j] - minVal) / (maxVal - minVal);
                    val *= 255;
                    int val2 = (int)(Math.Round(val));
                    GameManager.Player1Board.Cells[i, j].UpdateCellColor(Color.FromArgb(200, 255 - val2, val2, 0));
                    if (GameManager.Player1Board.Cells[i, j].IsHighlighted)
                    {
                        GameManager.Player1Board.Cells[i, j].HighlightCell();
                    }
                }
            }
        }
    }
}
