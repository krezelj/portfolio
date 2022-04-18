using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipTechPrototypeLIB
{
    public class Enemy
    {
        //
        // PROPERTIES
        //

        public double[,,] ValueBoards;

        Random rng = Manager.rng;

        public BoardModel Board;

        int[] shipsLeft;

        //
        // METHODS
        //

        public Enemy(BoardModel board)
        {
            ValueBoards = new double[board.NoRows, board.NoCols, 4];
            Board = board;
        }

        public void MakeMove(out int x, out int y, BoardModel board)
        {
            x = 0;
            y = 0;
            double maxValue = -2;
            EvaluateBoard();
            CountShipsLeft();
            int searchStep = 1;
            // Search for two cell ships
            if (shipsLeft[1] > 0)
            {
                searchStep = 1;
            }
            else if (shipsLeft[2] > 0)
            {
                searchStep = 2;
            }
            else if (shipsLeft[0] > 0)
            {
                searchStep = 0;
            }
            else
            {
                searchStep = 3;
            }
            for (int i = 0; i < board.NoRows; i++)
            {
                for (int j = 0; j < board.NoCols; j++)
                {
                    if (ValueBoards[i, j, searchStep] > maxValue)
                    {
                        x = j;
                        y = i;
                        maxValue = ValueBoards[i, j, searchStep];
                    }
                }
            }
        }

        public void CountShipsLeft()
        {
            shipsLeft = new int[] { 4, 3, 2, 1 };
            List<ShipModel> foundShips = new List<ShipModel>();
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    if (Board.Cells[i, j].IsHit && Board.Cells[i, j].ParentShip != null)
                    {
                        ShipModel ship = Board.Cells[i, j].ParentShip;
                        if (ship.IsSunken)
                        {
                            if (!foundShips.Contains(Board.Cells[i, j].ParentShip))
                            {
                                ship = Board.Cells[i, j].ParentShip;
                                foundShips.Add(ship);
                                shipsLeft[ship.Lenght - 1]--;
                            }
                        }
                    }
                }
            }
        }

        public void EvaluateBoard()
        {
            // Calculate standard bias
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    for (int s = 0; s < 4; s++)
                    {
                        double value = Math.Max(CountCells(true, i, j, minY: -s, maxY: s) - s, 0) + Math.Max(CountCells(true, i, j, minX: -s, maxX: s) - s, 0);
                        ValueBoards[i, j, s] = value;
                    }
                }
            }
            // Apply bias
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    double sum = 0;
                    for (int s = 0; s < 4; s++)
                    {
                        sum += ValueBoards[i, j, s];
                    }
                    sum /= 40;
                    for (int s = 0; s < 4; s++)
                    {
                        ValueBoards[i, j, s] = sum;
                    }
                }
            }
            // Apply probability cells modifiers
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    for (int s = 0; s < 4; s++)
                    {
                        double value = Math.Max(CountCells(false, i, j, minY: -s, maxY: s) - s, 0) + Math.Max(CountCells(false, i, j, minX: -s, maxX: s) - s, 0);
                        ValueBoards[i, j, s] += value;
                    }
                }
            }
            // Apply ship modifiers
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    if (Board.Cells[i, j].IsHit)
                    {
                        AddGlobally(i, j, -100);
                        if (Board.Cells[i, j].HasShip)
                        {
                            int multiplier = Board.Cells[i, j].ParentShip.IsSunken ? -1 : 1;
                            //=========================================
                            if (i > 0)
                            {
                                AddGlobally(i - 1, j, 50 * multiplier);
                            }
                            //=========================================
                            if (i < Board.NoRows - 1)
                            {
                                AddGlobally(i + 1, j, 50 * multiplier);
                            }
                            //=========================================
                            if (j > 0)
                            {
                                AddGlobally(i, j - 1, 50 * multiplier);
                            }
                            //=========================================
                            if (j < Board.NoCols - 1)
                            {
                                AddGlobally(i, j + 1, 50 * multiplier);
                            }
                            //=========================================
                            if (i > 0 && j > 0)
                            {
                                AddGlobally(i - 1, j - 1, -100);
                            }
                            //=========================================
                            if (i < Board.NoRows - 1 && j < Board.NoCols - 1)
                            {
                                AddGlobally(i + 1, j + 1, -100);
                            }
                            //=========================================
                            if (i > 0 && j < Board.NoCols - 1)
                            {
                                AddGlobally(i - 1, j + 1, -100);
                            }
                            //=========================================
                            if (i < Board.NoRows - 1 && j > 0)
                            {
                                AddGlobally(i+ 1, j - 1, -100);
                            }
                        }
                    }
                }
            }
            // Limit values
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    for (int s = 0; s < 4; s++)
                    {
                        ValueBoards[i, j, s] = Math.Max(Math.Min(ValueBoards[i, j, s], 30), -1);
                    }
                }
            }
        }

        void AddGlobally(int y, int x, double val)
        {
            for (int s = 0; s < 4; s++)
            {
                ValueBoards[y, x, s] += val;
            }
        }

        int CountCells(bool ignoreHit, int y, int x, int minX = 0, int maxX = 0, int minY = 0, int maxY = 0)
        {
            int count = 0;
            int minI = minY;
            int maxI = maxY;
            int minJ = minX;
            int maxJ = maxX;
            for (int i = -1; i >= minY; i--)
            {
                if (y + i < 0 || (Board.Cells[y + i, x].IsHit && !ignoreHit))
                {
                    minI = i + 1;
                    break;
                }
            }
            for (int i = 1; i <= maxY; i++)
            {
                if (y + i >= Board.NoRows || (Board.Cells[y + i, x].IsHit && !ignoreHit))
                {
                    maxI = i - 1;
                    break;
                }
            }
            for (int j = -1; j >= minX; j--)
            {
                if (x + j < 0 || (Board.Cells[y, x + j].IsHit && !ignoreHit))
                {
                    minJ = j + 1;
                    break;
                }
            }
            for (int j = 1; j <= maxX; j++)
            {
                if (x + j >= Board.NoCols || (Board.Cells[y, x + j].IsHit && !ignoreHit))
                {
                    maxJ = j - 1;
                    break;
                }
            }
            count += (maxI - minI + 1) + (maxJ - minJ + 1) - 1;
            return count;
        }

        public void ShowValues(int valueMode)
        {
            EvaluateBoard();
            double minVal = 31;
            double maxVal = -31;
            double val = 0;
            for (int i = 0; i < Board.NoRows; i++)
            {
                for (int j = 0; j < Board.NoCols; j++)
                {
                    if (ValueBoards[i, j, valueMode] < minVal)
                    {
                        minVal = ValueBoards[i, j, valueMode];
                    }
                    if (ValueBoards[i, j, valueMode] > maxVal)
                    {
                        maxVal = ValueBoards[i, j, valueMode];
                    }
                }
            }
            for (int i = 0; i < GameManager.Player1Board.NoRows; i++)
            {
                for (int j = 0; j < GameManager.Player1Board.NoCols; ++j)
                {
                    val = (double)(ValueBoards[i, j, valueMode] - minVal) / (maxVal - minVal);
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

        public void PlaceShips(BoardModel board)
        {
            int x = 0;
            int y = 0;
            int[,,] validationBoard = new int[board.NoRows, board.NoCols, 4];
            board.ClearBoard();
            for (int i = 0; i < board.NoRows; i++)
            {
                for (int j = 0; j < board.NoCols; j++)
                {
                    for (int s = 0; s < 4; s++)
                    {
                        validationBoard[i, j, s] = 3;
                        if (i >= board.NoRows - s)
                        {
                            validationBoard[i, j, s] -= 1;
                        }
                        if (j >= board.NoCols - s)
                        {
                            validationBoard[i, j, s] -= 2;
                        }
                    }
                }
            }
            for (int s = 3; s >= 0; s--)
            {
                for (int i = s + 1; i <= 4; i++)
                {
                    do
                    {
                        x = rng.Next(0, board.NoCols);
                        y = rng.Next(0, board.NoRows);
                    } while (validationBoard[y, x, s] <= 0);
                    //MessageBox.Show($"x: { x }, y: { y }, s: { s }, val: { validationBoard[y, x, s] }");
                    if (validationBoard[y, x, s] == 3)
                    {
                        validationBoard[y, x, s] -= rng.Next(1, 3);
                    }
                    //MessageBox.Show($"x: { x }, y: { y }, s: { s }, val: { validationBoard[y, x, s] }");
                    if (validationBoard[y, x, s] == 1)
                    {
                        // vertical
                        for (int j = 0; j <= s; j++)
                        {
                            board.UpdateShips(x, y + j, false);
                        }
                        // update validation
                        for (int k = 0; k < 4; k++)
                        {
                            for (int l = Math.Max(y - (k+1), 0); l <= Math.Min(y + s + 1, board.NoRows - 1); l++)
                            {
                                for (int m = Math.Max(x - (k+1), 0); m <= Math.Min(x + 1, board.NoCols - 1); m++)
                                {
                                    if (x - m >= 2 && y - l >= 2)
                                    {
                                        // do nothing
                                    }
                                    if (x - m < 2)
                                    {
                                        validationBoard[l, m, k] -= 1;
                                    }
                                    if (y - l < 2)
                                    {
                                        validationBoard[l, m, k] -= 2;
                                    }
                                }
                            }
                        }
                    }
                    else if (validationBoard[y, x, s] == 2)
                    {
                        // horizontal
                        for (int j = 0; j <= s; j++)
                        {
                            board.UpdateShips(x + j, y, false);
                        }
                        // update validation
                        for (int k = 0; k < 4; k++)
                        {
                            for (int l = Math.Max(y - (k+1), 0); l <= Math.Min(y + 1, board.NoRows - 1); l++)
                            {
                                for (int m = Math.Max(x - (k+1), 0); m <= Math.Min(x + s + 1, board.NoCols - 1); m++)
                                {
                                    if (x - m >= 2 && y - l >= 2)
                                    {
                                        //do nothing
                                    }
                                    if (x - m < 2)
                                    {
                                        validationBoard[l, m, k] -= 1;
                                    }
                                    if (y - l < 2)
                                    {
                                        validationBoard[l, m, k] -= 2;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
