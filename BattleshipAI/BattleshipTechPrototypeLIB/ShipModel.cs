using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTechPrototypeLIB
{
    public class ShipModel
    {
        //
        // PROPERTIES
        //

        /// <summary>
        /// The board that contains this cell.
        /// </summary>
        public BoardModel ParentBoard;

        /// <summary>
        /// List of cells that make up this ship.
        /// </summary>
        public List<CellModel> Cells = new List<CellModel>();

        public Color ShipColor = Color.Black;

        /// <summary>
        /// The lenght of the ship in cells.
        /// </summary>
        public int Lenght = 0;

        /// <summary>
        /// Determines whether the ship is sunken.
        /// </summary>
        public bool IsSunken = false;

        //
        // METHODS
        //

        /// <summary>
        /// Initializes an instance of a ship.
        /// </summary>
        /// <param name="parentBoard">The board that this ship belongs to.</param>
        public ShipModel(BoardModel parentBoard)
        {
            ParentBoard = parentBoard;
            //ShipColor = Color.FromArgb(Manager.rng.Next(0, 257), Manager.rng.Next(0, 257), Manager.rng.Next(0, 257));
        }

        /// <summary>
        /// Adds a cell to the ship.
        /// </summary>
        /// <param name="cellToAdd"></param>
        public void AddCell(CellModel cellToAdd)
        {
            Cells.Add(cellToAdd);
            cellToAdd.ParentShip = this;
            ++Lenght;
        }

        /// <summary>
        /// Invalidates all cells that are part of the ship.
        /// </summary>
        public void InvalidateShip()
        {
            foreach (CellModel cell in Cells)
            {
                cell.IsValid = false;
                cell.UpdateCellColor();
            }
        }

        /// <summary>
        /// Checks if the ship is sunken and updates its status.
        /// </summary>
        /// <returns>Whether the ship is sunken.</returns>
        public bool CheckIfSunken()
        {
            foreach (CellModel cell in Cells)
            {
                if (!cell.IsHit)
                {
                    IsSunken = false;
                    return false;
                }
            }
            IsSunken = true;
            return true;
        }

        /// <summary>
        /// Updates all the cells that are part of this ship.
        /// </summary>
        public void UpdateCells()
        {
            foreach (CellModel cell in Cells)
            {
                cell.UpdateCell();
            }
        }
    }
}
