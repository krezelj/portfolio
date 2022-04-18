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
    /// Represents a game board.
    /// </summary>
    public class BoardModel
    {
        //
        // PROPERTIES
        //

        /// <summary>
        /// Position of the board on the canvas.
        /// </summary>
        public Point Position;

        /// <summary>
        /// Array of cells that the board contains.
        /// </summary>
        public CellModel[,] Cells;

        /// <summary>
        /// List of all ships currently on the board.
        /// </summary>
        public List<ShipModel> Ships = new List<ShipModel>();

        /// <summary>
        /// Size of the board.
        /// </summary>
        public Size BoardSize;

        /// <summary>
        /// Size of a single cell;
        /// </summary>
        public Size CellSize;

        /// <summary>
        /// Number of rows that the board has.
        /// </summary>
        public int NoRows;

        /// <summary>
        /// Number of columns that the board has.
        /// </summary>
        public int NoCols;

        /// <summary>
        /// Currently highlighted cell.
        /// </summary>
        public CellModel HighlightedCell = null;

        /// <summary>
        /// Determines whether the board is in the edit mode.
        /// </summary>
        public bool EditMode = true;

        /// <summary>
        /// Determines whether the board is in the play mode.
        /// </summary>
        public bool PlayMode = false;

        //
        // METHODS
        //

        /// <summary>
        /// Initializes an instance of a BoardModel class.
        /// </summary>
        /// <param name="size">Size of the board.</param>
        /// <param name="postion">Position of the board on the canvas.</param>
        /// <param name="noRows">Number of rows.</param>
        /// <param name="noCols">Number of columns.</param>
        public BoardModel(Size size, Point postion, int noRows, int noCols)
        {
            //Assign values
            BoardSize = size;
            Position = postion;
            NoRows = noRows;
            NoCols = noCols;
            Cells = new CellModel[NoRows, NoCols];
            CellSize = new Size(BoardSize.Width / NoCols, BoardSize.Height / NoRows);
            //Create cells
            for (int i = 0; i < NoRows; ++i)
            {
                for (int j = 0; j < noCols; ++j)
                {
                    Cells[i, j] = new CellModel(new Point(j, i), CellSize, this);
                }
            }
            //Initialize cells
            for (int i = 0; i < NoRows; ++i)
            {
                for (int j = 0; j < noCols; ++j)
                {
                    Cells[i, j].AddNeighbours();
                }
            }
        }

        /// <summary>
        /// Clears all cells on the board and updates them.
        /// </summary>
        public void ClearBoard()
        {
            foreach (CellModel cell in Cells)
            {
                cell.HasShip = false;
                cell.UpdateCell();
            }
        }

        /// <summary>
        /// Determines whether the board is valid.
        /// </summary>
        ///<param name="validationMessage">The message that should be shown to the user after the validation.</param>
        /// <returns>Whether the board is valid.</returns>
        public bool SoftValidateBoard(out string validationMessage)
        {
            validationMessage = "Validation successful.";
            bool isValid = true;
            // Validate cells.
            foreach (CellModel cell in Cells)
            {
                if (!cell.IsValid)
                {
                    cell.ParentShip.InvalidateShip();
                    isValid = false;
                    validationMessage = "At least one of the cells is invalid.";
                }
            }
            return isValid;
        }

        public bool HardValidateBoard(out string validationMessage)
        {
            validationMessage = "";
            bool isValid = SoftValidateBoard(out validationMessage);
            // Count ships.
            int[] shipCount = new int[4];
            foreach (ShipModel ship in Ships)
            {
                switch (ship.Lenght)
                {
                    case 1:
                        shipCount[0]++;
                        break;
                    case 2:
                        shipCount[1]++;
                        break;
                    case 3:
                        shipCount[2]++;
                        break;
                    case 4:
                        shipCount[3]++;
                        break;
                    default:
                        ship.InvalidateShip();
                        validationMessage = "At least one of the ships is too big.";
                        isValid = false;
                        break;
                }
            }
            //Validate ship count.
            if (shipCount[0] != 4)
            {
                validationMessage = "You need to have 4 one cell ships.";
                isValid = false;
            }
            if (shipCount[1] != 3)
            {
                validationMessage = "You need to have 3 two cell ships.";
                isValid = false;
            }
            if (shipCount[2] != 2)
            {
                validationMessage = "You need to have 2 three cell ships.";
                isValid = false;
            }
            if (shipCount[3] != 1)
            {
                validationMessage = "You need to have 1 four cell ships.";
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Creates ships based on cells' status.
        /// </summary>
        /// <returns>Whether the ships have been created successfully</returns>
        public void CreateShips()
        {
            ClearShips();
            for (int i = 0; i < NoRows; ++i)
            {
                for (int j = 0; j < NoCols; ++j)
                {
                    if (Cells[i, j].HasShip)
                    {
                        if (Cells[i, j].Neighbours[0] != null && Cells[i, j].Neighbours[0].HasShip)
                        {
                            Cells[i, j].Neighbours[0].ParentShip.AddCell(Cells[i, j]);
                        }
                        else if (Cells[i, j].Neighbours[3] != null && Cells[i, j].Neighbours[3].HasShip)
                        {
                            Cells[i, j].Neighbours[3].ParentShip.AddCell(Cells[i, j]);
                        }
                        else
                        {
                            ShipModel newShip = new ShipModel(this);
                            Ships.Add(newShip);
                            newShip.AddCell(Cells[i, j]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears all ships from the board and deletes their info.
        /// </summary>
        public void ClearShips()
        {
            foreach (CellModel cell in Cells)
            {
                cell.ParentShip = null;
            }
            foreach (ShipModel ship in Ships)
            {
                //TODO - Leave it like this or create method in ShipModel????
                ship.Cells.Clear();
            }
            Ships.Clear();
        }

        /// <summary>
        /// Updates the currently highlighted cell.
        /// </summary>
        /// <param name="e">Mouse data on update.</param>
        /// <returns>Whether or not the canvas should be redrawn.</returns>
        public bool UpdateHighlightedCell(int x, int y, bool mouseInput)
        {
            int mousePosX;
            int mousePosY;
            if (mouseInput)
            {
                mousePosX = (int)Math.Floor((double)(x - Position.X) / CellSize.Width);
                mousePosY = (int)Math.Floor((double)(y - Position.Y) / CellSize.Height);
            }
            else
            {
                mousePosX = x;
                mousePosY = y;
            }
            bool redrawCanvas = true;

            //TODO - Make this part better.
            if ((mousePosX >= 0 && mousePosX < NoCols) && (mousePosY >= 0 && mousePosY < NoRows))
            {
                if (Cells[mousePosY, mousePosX] == HighlightedCell)
                {
                    redrawCanvas = false;
                }
                else
                {
                    if (HighlightedCell != null)
                    {
                        HighlightedCell.IsHighlighted = false;
                        HighlightedCell.UpdateCellColor();
                    }
                    HighlightedCell = Cells[mousePosY, mousePosX];
                    HighlightedCell.IsHighlighted = true;
                    HighlightedCell.UpdateCellColor();
                }
            }
            else if (HighlightedCell != null)
            {
                HighlightedCell.IsHighlighted = false;
                HighlightedCell.UpdateCellColor();
                HighlightedCell = null;
            }
            else
            {
                redrawCanvas = false;
            }
            return redrawCanvas;
        }

        /// <summary>
        /// Updates ships and cells' connections on the board.
        /// </summary>
        /// <param name="e">Mouse data on update.</param>
        public void UpdateShips(int x, int y, bool mouseInput)
        {
            int mousePosX;
            int mousePosY;
            if (mouseInput)
            {
                mousePosX = (int)Math.Floor((double)(x - Position.X) / CellSize.Width);
                mousePosY = (int)Math.Floor((double)(y - Position.Y) / CellSize.Height);
            }
            else
            {
                mousePosX = x;
                mousePosY = y;
            }
            if ((mousePosX >= 0 && mousePosX < NoCols) && (mousePosY >= 0 && mousePosY < NoRows))
            {
                CellModel cellToUpdate = Cells[mousePosY, mousePosX];
                cellToUpdate.HasShip = !cellToUpdate.HasShip;
                cellToUpdate.UpdateCellSurroundings();
                CreateShips();
                foreach (ShipModel ship in Ships)
                {
                    ship.UpdateCells();
                }
                HardValidateBoard(out string validationMessage);
            }
        }

        /// <summary>
        /// Hits the cell at specified position.
        /// </summary>
        /// <param name="x">X component of the position.</param>
        /// <param name="y">Y component of the position.</param>
        /// <param name="mouseInput">Whether the input comes from mouse.</param>
        /// <returns>Whether the action was valid.</returns>
        public bool HitCell(int x, int y, bool mouseInput)
        {
            int mousePosX;
            int mousePosY;
            if (mouseInput)
            {
                mousePosX = (int)Math.Floor((double)(x - Position.X) / CellSize.Width);
                mousePosY = (int)Math.Floor((double)(y - Position.Y) / CellSize.Height);
            }
            else
            {
                mousePosX = x;
                mousePosY = y;
            }
            bool isValid = true;
            if ((mousePosX >= 0 && mousePosX < NoCols) && (mousePosY >= 0 && mousePosY < NoRows))
            {
                CellModel cellToHit = Cells[mousePosY, mousePosX];
                if (cellToHit.IsHit)
                {
                    isValid = false;
                }
                else
                {
                    cellToHit.IsHit = true;
                    if (cellToHit.HasShip)
                    {
                        cellToHit.ParentShip.CheckIfSunken();
                    } 
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Shows the cell's value to the user.
        /// </summary>
        public void ShowValue(int x, int y, bool mouseInput, int board)
        {
            int mousePosX;
            int mousePosY;
            if (mouseInput)
            {
                mousePosX = (int)Math.Floor((double)(x - Position.X) / CellSize.Width);
                mousePosY = (int)Math.Floor((double)(y - Position.Y) / CellSize.Height);
            }
            else
            {
                mousePosX = x;
                mousePosY = y;
            }
            if ((mousePosX >= 0 && mousePosX < NoCols) && (mousePosY >= 0 && mousePosY < NoRows))
            {
                CellModel cellToShow = Cells[mousePosY, mousePosX];
                MessageBox.Show(GameManager.Enemy2.ValueBoards[cellToShow.Position.Y, cellToShow.Position.X, board].ToString());
            }
        }

    }
}
