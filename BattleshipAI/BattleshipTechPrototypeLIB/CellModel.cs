using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTechPrototypeLIB
{
    /// <summary>
    /// Represents a single cell on the board.
    /// </summary>
    public class CellModel
    {
        //
        // PROPERTIES
        //

        /// <summary>
        /// Position of the cell with respect to the board.
        /// </summary>
        public Point Position;

        /// <summary>
        /// Position of the cell with respect to the canvas.
        /// </summary>
        public Point CanvasPosition;

        /// <summary>
        /// Size of the cell.
        /// </summary>
        public Size CellSize = new Size();

        /// <summary>
        /// Color of the cell.
        /// </summary>
        public Color CellColor = Color.FromArgb(200, 0, 0, 255);

        /// <summary>
        /// The board that contains this cell.
        /// </summary>
        public BoardModel ParentBoard;

        /// <summary>
        /// The ship that this cell is a part of.
        /// </summary>
        public ShipModel ParentShip;

        /// <summary>
        /// An array of immediate neighbours. 
        /// North, East, South, West.
        /// </summary>
        public CellModel[] Neighbours = new CellModel[4];

        /// <summary>
        /// An array of corner neighbours.
        /// Upper Right, Bottom Right, Bottom Left, Upper Left.
        /// </summary>
        public CellModel[] CornerNeighbours = new CellModel[4];

        /// <summary>
        /// Determines whether the cell is connected to its neighbour.
        /// North, East, South, West.
        /// </summary>
        public bool[] Connections = new bool[4];

        /// <summary>
        /// Determines whether the cell is currently highlighted.
        /// </summary>
        public bool IsHighlighted = false;

        /// <summary>
        /// Determines whether the cell contains ship.
        /// </summary>
        public bool HasShip = false;

        /// <summary>
        /// Determines whether the cell has been hit.
        /// </summary>
        public bool IsHit = false;

        /// <summary>
        /// Determines whether the cell is valid.
        /// </summary>
        public bool IsValid = true;

        //
        // METHODS
        //

        /// <summary>
        /// Initializes an instance of a cell.
        /// </summary>
        /// <param name="position">Position of the cell on the board.</param>
        /// <param name="size">Size of the cell.</param>
        /// <param name="parentBoard">The board that contains this cell.</param>
        public CellModel(Point position, Size size, BoardModel parentBoard)
        {
            ParentBoard = parentBoard;
            Position = position;
            CellSize = size;
            CanvasPosition = new Point(ParentBoard.Position.X + Position.X * CellSize.Width,
                                       ParentBoard.Position.Y + Position.Y * CellSize.Height);
        }


        /// <summary>
        /// Calls all Update methods except for UpdateSurroundings and UpdateShipCells.
        /// </summary>
        public void UpdateCell()
        {
            UpdateCellConnections();
            ValidateCell();
            UpdateCellColor();
        }

        /// <summary>
        /// Updates the cell's color.
        /// </summary>
        public void UpdateCellColor()
        {
            CellColor = Color.FromArgb(200, 0, 0, 255);

            if (!IsValid)
            {
                CellColor = Color.FromArgb(200, 255, 0, 0);
            }

            if (IsHighlighted)
            {
                HighlightCell();
            }
        }

        /// <summary>
        /// Highlights the cell.
        /// </summary>
        public void HighlightCell()
        {
            CellColor = Color.FromArgb(100, CellColor.R, CellColor.G, CellColor.B);
        }

        /// <summary>
        /// Updates the cell's color.
        /// </summary>
        /// <param name="color"></param>
        public void UpdateCellColor(Color color)
        {
            CellColor = color;
        }

        /// <summary>
        /// Updates the cell and its neighbours.
        /// </summary>
        public void UpdateCellSurroundings()
        {
            UpdateCell();
            foreach (CellModel neighbour in Neighbours)
            {
                if (neighbour != null)
                {
                    neighbour.UpdateCell();
                }
            }
            foreach (CellModel cornerNeighbour in CornerNeighbours)
            {
                if (cornerNeighbour != null)
                {
                    cornerNeighbour.UpdateCell();
                }
            }
        }

        /// <summary>
        /// Updates all the cells that are part of this cell's parent ship.
        /// </summary>
        public void UpdateShipCells()
        {
            if (ParentShip != null)
            {
                ParentShip.UpdateCells();
            }
        }

        /// <summary>
        /// Updates the cell's connections.
        /// </summary>
        public void UpdateCellConnections()
        {
            //TODO - Remove hardcoded 4.
            for (int i = 0; i < 4; ++i)
            {
                if (Neighbours[i] != null && (this.HasShip && Neighbours[i].HasShip))
                {
                    Connections[i] = true;
                }
                else
                {
                    Connections[i] = false;
                }
            }
        }

        /// <summary>
        /// Determines whether the cell is valid.
        /// </summary>
        /// <returns>Whether the cell is valid.</returns>
        public bool ValidateCell()
        {
            IsValid = true;
            for (int i = 0; i < 4; ++i)
            {
                //TODO - See if this is actually necessary
                // if (Connections[i] && Connections[(i+1)%4])
                // {
                //     isValid = false;
                //     return isValid;
                // }
                if (CornerNeighbours[i] != null && (HasShip && CornerNeighbours[i].HasShip))
                {
                    IsValid = false;
                    return IsValid;
                }
            }
            return IsValid;
            //TODO - Remove hardcoded 4
        }

        /// <summary>
        /// Adds cell's neighbours to the list.
        /// </summary>
        public void AddNeighbours()
        {
            //TODO - Make this part better.
            Neighbours = new CellModel[4];
            CornerNeighbours = new CellModel[4];
            //Northen Cells
            if (Position.Y > 0)
            {
                Neighbours[0] = ParentBoard.Cells[Position.Y - 1, Position.X];
                if (Position.X < ParentBoard.NoCols - 1)
                {
                    CornerNeighbours[0] = ParentBoard.Cells[Position.Y - 1, Position.X + 1];
                }
                if (Position.X > 0)
                {
                    CornerNeighbours[3] = ParentBoard.Cells[Position.Y - 1, Position.X - 1];
                }
            }
            //Eastern Cells
            if (Position.X < ParentBoard.NoCols - 1)
            {
                Neighbours[1] = ParentBoard.Cells[Position.Y, Position.X + 1];
            }
            //Southern Cells
            if (Position.Y < ParentBoard.NoRows - 1)
            {
                Neighbours[2] = ParentBoard.Cells[Position.Y + 1, Position.X];
                if (Position.X < ParentBoard.NoCols - 1)
                {
                    CornerNeighbours[1] = ParentBoard.Cells[Position.Y + 1, Position.X + 1];
                }
                if (Position.X > 0)
                {
                    CornerNeighbours[2] = ParentBoard.Cells[Position.Y + 1, Position.X - 1];
                }
            }
            //Western Cells
            if (Position.X > 0)
            {
                Neighbours[3] = ParentBoard.Cells[Position.Y, Position.X - 1];
            }
        }
    }
}
