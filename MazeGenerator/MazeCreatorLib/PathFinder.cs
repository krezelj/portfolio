using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeCreatorLIB
{
    /// <summary>
    /// Represents a path finder between two specified nodes in the maze.
    /// </summary>
    public class PathFinder
    {
        //
        // PROPERTIES
        //

        public Stopwatch SW = new Stopwatch();

        /// <summary>
        /// The calculated path between the starting and ending point.
        /// </summary>
        public List<Node> Path { get; set; } = new List<Node>();

        /// <summary>
        /// The maze current maze.
        /// </summary>
        Maze maze;

        /// <summary>
        /// The starting point of the path.
        /// </summary>
        public Node StartNode { get; set; } = null;

        /// <summary>
        /// The ending point of the path.
        /// </summary>
        public Node EndNode { get; set; } = null;

        /// <summary>
        /// A list of junctions in the current maze.
        /// </summary>
        public List<Node> Junctions { get; } = new List<Node>();

        /// <summary>
        /// A list of currently considered nodes.
        /// </summary>
        public List<Node> openSet = new List<Node>();

        /// <summary>
        /// A dictionary that describes a path between two nodes.
        /// </summary>
        public Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        /// <summary>
        /// A dictionary of g score values of every junction node.
        /// </summary>
        public Dictionary<Node, int> gScore = new Dictionary<Node, int>();

        /// <summary>
        /// A dictionary of f score values of every junction node.
        /// </summary>
        public Dictionary<Node, int> fScore = new Dictionary<Node, int>();


        //
        // METHODS
        //

        public bool FindPath()
        {
            maze = Manager.Maze;
            for (int i = 0; i < maze.Rows - 1; i++)
            {
                for (int j = 0; j < maze.Cols - 1; j++)
                {
                    maze[i, j].IsJunction = false;
                    maze[i, j].IsParthOfPath = false;
                    maze[i, j].WasConsidered = false;
                }
            }
            SW.Restart();
            CreateJunctions();
            if (CalculatePath())
            {
                CreatePath();
                SW.Stop();
                return true;
            }
            SW.Stop();
            return false;
        }

        /// <summary>
        /// Finds and creates junctions in the current maze.
        /// </summary>
        public void CreateJunctions()
        {
            Junctions.Clear();
            StartNode = Manager.StartNode;
            EndNode = Manager.EndNode;
            StartNode.IsJunction = true;
            EndNode.IsJunction = true;
            for (int i = 1; i < maze.Rows - 1; i+=2)
            {
                for (int j = 1; j < maze.Cols - 1; j+=2)
                {
                    if (maze[i, j].IsVisited && maze[i, j].FindNeighbours().Count > 2)
                    {
                        maze[i, j].IsJunction = true;
                        Junctions.Add(maze[i, j]);
                    }
                    else if (maze[i, j].IsVisited && maze[i, j].FindNeighbours().Count == 2)
                    {
                        if (!(maze[i, j].Neighbours[0].Row == maze[i, j].Neighbours[1].Row) && 
                            !(maze[i, j].Neighbours[0].Col == maze[i, j].Neighbours[1].Col))
                        {
                            maze[i, j].IsJunction = true;
                            Junctions.Add(maze[i, j]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the h score of a given node.
        /// </summary>
        /// <param name="n">The node which h score will be calculated.</param>
        /// <returns>The h score of a node.</returns>
        public int HScore(Node n)
        {
            return Math.Abs(EndNode.Row - n.Row) + Math.Abs(EndNode.Col - n.Col);
        }

        /// <summary>
        /// Calculates the f score of a given node.
        /// </summary>
        /// <param name="n">The node which f score will be calculated.</param>
        /// <returns>The f score of a node.</returns>
        public int FScore(Node n)
        {
            return gScore[n] + HScore(n);
        }

        /// <summary>
        /// Finds the node with the lowest f score in the openset.
        /// </summary>
        /// <returns></returns>
        public Node FindMinFScore()
        {
            int minF = 2000000000;
            Node minFNode = StartNode;
            foreach(Node n in openSet)
            {
                if (FScore(n) < minF)
                {
                    minFNode = n;
                    minF = FScore(n); 
                }
            }
            return minFNode;
        }

        /// <summary>
        /// Adds a node to the open set at the correct position.
        /// </summary>
        /// <param name="n"></param>
        public void AddToOpenSet(Node n)
        {
            if (openSet.Count() == 0)
            {
                openSet.Add(n);
                return;
            }
            int mid = 0;
            int min = 0;
            int max = openSet.Count() - 1;
            if (FScore(n) > FScore(openSet[max]))
            {
                openSet.Add(n);
            }
            else if (FScore(n) < FScore(openSet[min]))
            {
                openSet.Insert(0, n);
            }
            while (max - min > 1)
            {
                mid = (max + min) / 2;
                min = FScore(n) > FScore(openSet[mid]) ? mid : min;
                max = FScore(n) <= FScore(openSet[mid]) ? mid : max;
            }
            openSet.Insert(max, n);
        }

        /// <summary>
        /// Calculates the path between the starting node and the ending node
        /// using the A* search algorithm.
        /// </summary>
        /// <returns>Whether the search succeeded.</returns>
        public bool CalculatePath()
        {
            if (!Junctions.Contains(StartNode))
            {
                Junctions.Add(StartNode);
            }
            if (!Junctions.Contains(EndNode))
            {
                Junctions.Add(EndNode);
            }
            openSet.Clear();
            gScore.Clear();
            fScore.Clear();
            foreach (Node n in Junctions)
            {
                gScore.Add(n, int.MaxValue);
                fScore.Add(n, int.MaxValue);
            }

            AddToOpenSet(StartNode);
            // openSet.Add(StartNode);

            gScore[StartNode] = 0;
            fScore[StartNode] = HScore(StartNode);

            // Connect Junctions
            foreach (Node n in Junctions)
            {
                n.FindNeighbourJunctions();
            }
            // Find Path
            Node currentNode;
            int newGScore;

            while (openSet.Count > 0)
            {
                currentNode = openSet.First();
                // currentNode = FindMinFScore();

                currentNode.WasConsidered = true;
                if (currentNode == EndNode)
                {
                    return true;
                }
                openSet.Remove(currentNode);
                foreach (Node n in currentNode.NeighbourJunctions)
                {
                    newGScore = gScore[currentNode] + currentNode.DistanceTo(n);
                    if (newGScore < gScore[n])
                    {
                        cameFrom[n] = currentNode;
                        gScore[n] = newGScore;
                        fScore[n] = gScore[n] + HScore(n);
                        if (!openSet.Contains(n))
                        {
                            AddToOpenSet(n);
                            // openSet.Add(n);
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Constructs the path using cameFrom dictionary.
        /// </summary>
        public void CreatePath()
        {
            Path.Clear();
            Node currentNode = EndNode;
            do
            {
                //currentNode.IsParthOfPath = true;
                for (int i = 0; i < currentNode.DistanceTo(cameFrom[currentNode]); i++)
                {
                    if (currentNode.Row != cameFrom[currentNode].Row)
                    {
                        maze[currentNode.Row - i * Math.Sign(currentNode.Row-cameFrom[currentNode].Row), currentNode.Col].IsParthOfPath = true;
                    }
                    else
                    {
                        maze[currentNode.Row, currentNode.Col - i * Math.Sign(currentNode.Col - cameFrom[currentNode].Col)].IsParthOfPath = true;
                    }
                }
                Path.Add(currentNode);
                currentNode = cameFrom[currentNode];
            } while (currentNode != StartNode);
            Path.Add(currentNode);
            Path.Reverse();
        }

    }
}
