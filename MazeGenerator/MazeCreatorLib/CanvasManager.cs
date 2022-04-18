using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCreatorLIB
{
    public class CanvasManager
    {
        //
        // PROPERTIES
        //


        //
        // METHODS
        //

        /// <summary>
        /// Initializes an instance of a canvas manager class.
        /// </summary>
        public CanvasManager()
        {

        }

        /// <summary>
        /// Draws current maze (provided by GlobalData).
        /// </summary>
        /// <param name="g">Instance of Graphics class used to draw.</param>
        /// <param name="canvasSize">Size of the canvas used for scaling.</param>
        public void DrawMaze(Graphics g, Size canvasSize)
        {
            int scaleX = canvasSize.Width / Manager.Maze.Cols;
            int scaleY = canvasSize.Height / Manager.Maze.Rows;
            SolidBrush wallBrush = new SolidBrush(Color.White);
            SolidBrush currentNodeBrush = new SolidBrush(Color.Blue);
            SolidBrush startNodeBrush = new SolidBrush(Color.Green);
            SolidBrush endNodeBrush = new SolidBrush(Color.Red);
            SolidBrush junctionNodeBrush = new SolidBrush(Color.LightGreen);
            SolidBrush pathNodeBrush = new SolidBrush(Color.DeepSkyBlue);
            SolidBrush consideredNodeBrush = new SolidBrush(Color.Purple);
            for (int i = 0; i < Manager.Maze.Rows; i++)
            {
                for (int j = 0; j < Manager.Maze.Cols; j++)
                {
                    if (Manager.StartNode == Manager.Maze[i, j])
                    {
                        g.FillRectangle(startNodeBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }
                    else if (Manager.EndNode == Manager.Maze[i, j])
                    {
                        g.FillRectangle(endNodeBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }
                    else if (Manager.Maze[i, j].IsParthOfPath)
                    {
                        g.FillRectangle(pathNodeBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }
                    else if (Manager.Maze[i, j].WasConsidered)
                    {
                        g.FillRectangle(consideredNodeBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }
                    else if (Manager.Maze[i, j].IsJunction)
                    {
                        g.FillRectangle(junctionNodeBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }    
                    else if (Manager.Maze.CurrentNode == Manager.Maze[i, j])
                    {
                        g.FillRectangle(currentNodeBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }
                    else if (Manager.Maze[i, j].IsVisited)
                    {
                        g.FillRectangle(wallBrush, j * scaleX, i * scaleY, scaleX, scaleY);
                    }
                }
            }
        }
    }
}
