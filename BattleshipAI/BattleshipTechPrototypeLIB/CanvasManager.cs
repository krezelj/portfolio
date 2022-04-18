using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipTechPrototypeLIB
{
    /// <summary>
    /// A static instance of the CanvasManager class. Manages the canvas seen by the user.
    /// </summary>
    public static class CanvasManager
    {
        //
        // PROPERTIES
        //

        /// <summary>
        /// Ship's size relative to the cell.
        /// </summary>
        static double ShipSizeRatio = 0.5;

        //
        // METHODS
        //

        /// <summary>
        /// Draws the given board on the form.
        /// </summary>
        /// <param name="g">Graphics class instance used to draw objects.</param>
        /// <param name="board">The board to draw.</param>
        public static void DrawBoard(Graphics g, BoardModel board, DisplayMode displayMode)
        {
            foreach (CellModel cell in board.Cells)
            {
                DrawCell(g, cell);
            }
            DrawGrid(g, board.Position, board.BoardSize, board.NoRows, board.NoCols);
            foreach (CellModel cell in board.Cells)
            {
                if (cell.HasShip)
                {
                    if (displayMode == DisplayMode.ShowAll || (displayMode == DisplayMode.ShowHit && cell.IsHit))
                    {
                        DrawShipPart(g, cell);
                        DrawShipConnections(g, cell, displayMode);
                    }
                }
                if (cell.IsHit)
                {
                    DrawHit(g, cell);
                }
            }
        }

        /// <summary>
        /// Draws a grid on the form.
        /// </summary>
        /// <param name="g">Graphics class instance used to draw objects.</param>
        /// <param name="originPoint">The position of the upper-left corner of the grid.</param>
        /// <param name="size">Size of the grid.</param>
        /// <param name="noRows">Number of rows.</param>
        /// <param name="noCols">Number of columns.</param>
        public static void DrawGrid(Graphics g, Point originPoint, Size size, int noRows, int noCols)
        {
            Pen pen = new Pen(Color.Black, 3);
            // Vertical lines
            for (int i = 0; i < noCols + 1; ++i)
            {
                g.DrawLine(pen, new Point(i * size.Width / noCols + originPoint.X, 0 + originPoint.Y),
                                new Point(i * size.Width / noCols + originPoint.X, size.Height + originPoint.Y));
            }
            //Horizontal lines
            for (int j = 0; j < noRows + 1; ++j)
            {
                g.DrawLine(pen, new Point(0 + originPoint.X, j * size.Height / noRows + originPoint.Y),
                                new Point(size.Width + originPoint.X, j * size.Height / noRows + originPoint.Y));
            }
        }

        /// <summary>
        /// Draws a single cell on the canvas.
        /// </summary>
        /// <param name="g">Graphics class instance used to draw objects.</param>
        /// <param name="originPoint">The position of the upper-left corner of the cell.</param>
        /// <param name="cell">Cell to draw.</param>
        public static void DrawCell(Graphics g, CellModel cell)
        {
            SolidBrush cellBrush = new SolidBrush(cell.CellColor);
            g.FillRectangle(cellBrush, cell.CanvasPosition.X, cell.CanvasPosition.Y, cell.CellSize.Width, cell.CellSize.Height);
        }

        /// <summary>
        /// Draws a ship part on the canvas.
        /// </summary>
        /// <param name="g">Graphics class instance used to draw objects.</param>
        /// <param name="parentCell">Cell that contains ship info.</param>
        public static void DrawShipPart(Graphics g, CellModel parentCell)
        {
            //SolidBrush shipBrush = new SolidBrush(Color.Black);
            SolidBrush shipBrush = new SolidBrush(parentCell.ParentShip.ShipColor);
            if (parentCell.ParentShip.IsSunken)
            {
                shipBrush.Color = Color.DarkRed;
            }
            int marginX = (int)((1.0 - ShipSizeRatio) / 2 * parentCell.CellSize.Width);
            int marginY = (int)((1.0 - ShipSizeRatio) / 2 * parentCell.CellSize.Height);
            g.FillRectangle(shipBrush, parentCell.CanvasPosition.X + marginX, parentCell.CanvasPosition.Y + marginY,
                            parentCell.CellSize.Width - 2 * marginX, parentCell.CellSize.Height - 2 * marginY);
        }

        /// <summary>
        /// Draws connections between cells on the canvas.
        /// </summary>
        /// <param name="g">Graphics class instance used to draw objects.</param>
        /// <param name="parentCell">Cell that contains connections info.</param>
        public static void DrawShipConnections(Graphics g, CellModel parentCell, DisplayMode displayMode)
        {
            //SolidBrush shipBrush = new SolidBrush(Color.Black);
            SolidBrush shipBrush = new SolidBrush(parentCell.ParentShip.ShipColor);
            if (parentCell.ParentShip.IsSunken)
            {
                shipBrush.Color = Color.DarkRed;
            }
            int marginX = (int)((1.0 - ShipSizeRatio) / 2 * parentCell.CellSize.Width);
            int marginY = (int)((1.0 - ShipSizeRatio) / 2 * parentCell.CellSize.Height);
            //North
            if (parentCell.Connections[0] && (displayMode == DisplayMode.ShowAll || parentCell.Neighbours[0].IsHit))
            {
                g.FillRectangle(shipBrush, parentCell.CanvasPosition.X + marginX, parentCell.CanvasPosition.Y,
                                parentCell.CellSize.Width - 2 * marginX, marginY);
            }
            //East
            if (parentCell.Connections[1] && (displayMode == DisplayMode.ShowAll || parentCell.Neighbours[1].IsHit))
            {
                g.FillRectangle(shipBrush, parentCell.CanvasPosition.X + parentCell.CellSize.Width - marginX, parentCell.CanvasPosition.Y + marginY,
                                marginX, parentCell.CellSize.Height - 2 * marginY);
            }
            //South
            if (parentCell.Connections[2] && (displayMode == DisplayMode.ShowAll || parentCell.Neighbours[2].IsHit))
            {
                g.FillRectangle(shipBrush, parentCell.CanvasPosition.X + marginX, parentCell.CanvasPosition.Y + parentCell.CellSize.Height - marginY,
                                parentCell.CellSize.Width - 2 * marginX, marginY);
            }
            //West
            if (parentCell.Connections[3] && (displayMode == DisplayMode.ShowAll || parentCell.Neighbours[3].IsHit))
            {
                g.FillRectangle(shipBrush, parentCell.CanvasPosition.X, parentCell.CanvasPosition.Y + marginY,
                                marginX, parentCell.CellSize.Height - 2 * marginY);
            }
        }

        /// <summary>
        /// Draws a hit mark on the cell.
        /// </summary>
        /// <param name="g">Graphics class instance used to draw objects.</param>
        /// <param name="parentCell">Cell that contains hit info.</param>
        public static void DrawHit(Graphics g, CellModel parentCell)
        {
            SolidBrush hitBrush = new SolidBrush(Color.Red);
            int marginX = (int)((1.0 - ShipSizeRatio / 2) / 2 * parentCell.CellSize.Width);
            int marginY = (int)((1.0 - ShipSizeRatio / 2) / 2 * parentCell.CellSize.Height);
            g.FillRectangle(hitBrush, parentCell.CanvasPosition.X + marginX, parentCell.CanvasPosition.Y + marginY,
                            parentCell.CellSize.Width - 2 * marginX, parentCell.CellSize.Height - 2 * marginY);
        }
    }
}
