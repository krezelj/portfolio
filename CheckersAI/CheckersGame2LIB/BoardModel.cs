using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame2LIB
{
    public class BoardModel
    {
        public CheckerModel[,] Checkers { get; set; } = new CheckerModel[8, 8];
        public Dictionary<CheckerModel, List<MoveModel>> AvailableMoves { get; set; } = new Dictionary<CheckerModel, List<MoveModel>>();
        public CheckerModel ActiveChecker { get; set; } = null;
        public Coordinates HighlightedCell { get; set; } = new Coordinates(-1, -1);
        public bool IsWhitePlaying { get; set; } = true;

        Stopwatch sw = new Stopwatch();

        /// <summary>
        /// Initializes an instance of a board that is not a child (The current board).
        /// </summary>
        public BoardModel()
        {
            SetUpNewCheckers();
            FindAllAvailableMoves();
        }

        public BoardModel(BoardModel parentBoard, MoveModel moveToApply)
        {
            CheckerModel copiedChecker;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    copiedChecker = parentBoard.Checkers[i, j];
                    if (copiedChecker != null)
                    {
                        Checkers[i, j] = new CheckerModel(copiedChecker.IsWhite, copiedChecker.IsKing);
                    }
                }
            }
            bool becameKing = ApplyMove(moveToApply);
            if (moveToApply.CaptureCell != null && CheckForBonus(moveToApply.TargetCell.Column, moveToApply.TargetCell.Row) && !becameKing)
            {
                IsWhitePlaying = parentBoard.IsWhitePlaying;
            }
            else
            {
                IsWhitePlaying = !parentBoard.IsWhitePlaying;
            }
        }

        public void SetUpNewCheckers()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        if (j < 3)
                        {
                            Checkers[i, j] = new CheckerModel(false);
                        }
                        else if (j > 4)
                        {
                            Checkers[i, j] = new CheckerModel(true);
                        }
                    }
                    
                }
            }
        }

        public void FindAllAvailableMoves()
        {
            AvailableMoves.Clear();
            List<MoveModel> checkerMoves = new List<MoveModel>();
            bool captureAvailable = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Checkers[i, j] != null && Checkers[i, j].IsWhite == IsWhitePlaying)
                    {
                        checkerMoves = new List<MoveModel>();
                        checkerMoves = FindCheckerAvailableMoves(i, j);
                        if (checkerMoves.Count > 0)
                        {
                            AvailableMoves.Add(Checkers[i, j], checkerMoves);
                            foreach (MoveModel m in checkerMoves)
                            {
                                if (m.CaptureCell != null)
                                {
                                    captureAvailable = true;
                                    goto CaptureIsAvailableBreakPoint; //line 77
                                }
                            }
                        }
                    }
                }
            }
            CaptureIsAvailableBreakPoint:;
            if (captureAvailable)
            {
                AvailableMoves.Clear();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Checkers[i, j] != null && Checkers[i, j].IsWhite == IsWhitePlaying)
                        {
                            checkerMoves = new List<MoveModel>();
                            checkerMoves = FindCheckerAvailableMoves(i, j, true);
                            if (checkerMoves.Count > 0)
                            {
                                AvailableMoves.Add(Checkers[i, j], checkerMoves);
                            }
                        }
                    }
                }
            }
        }

        public List<MoveModel> GetRawAvailableMoves()
        {
            List<MoveModel> rawMoves = new List<MoveModel>();
            foreach (KeyValuePair<CheckerModel, List<MoveModel>> kvp in AvailableMoves)
            {
                rawMoves.AddRange(kvp.Value);
            }
            return rawMoves;
        }

        // TODO - make this thing more compact.
        public List<MoveModel> FindCheckerAvailableMoves(int col, int row, bool captureOnly = false)
        {
            CheckerModel checker = Checkers[col, row];
            CheckerModel potentialTarget = null;
            List<MoveModel> moves = new List<MoveModel>();
            if (checker.IsWhite || checker.IsKing)
            {
                if (row - 1 >= 0)
                {
                    if (col - 1 >= 0)
                    {
                        potentialTarget = Checkers[col - 1, row - 1];
                        if (potentialTarget == null)
                        {
                            if (!captureOnly)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col - 1, row - 1)));
                            }
                        }
                        else if (potentialTarget.IsWhite != checker.IsWhite)
                        {
                            if (row - 2 >= 0 && col - 2 >= 0 && Checkers[col - 2, row - 2] == null)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col - 2, row - 2), new Coordinates(col - 1, row - 1)));
                            }
                        }
                    }
                    if (col + 1 < 8)
                    {
                        potentialTarget = Checkers[col + 1, row - 1];
                        if (potentialTarget == null)
                        {
                            if (!captureOnly)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col + 1, row - 1)));
                            }
                        }
                        else if (potentialTarget.IsWhite != checker.IsWhite)
                        {
                            if (row - 2 >= 0 && col + 2 < 8 && Checkers[col + 2, row - 2] == null)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col + 2, row - 2), new Coordinates(col + 1, row - 1)));
                            }
                        }
                    }
                }
            }
            if (!checker.IsWhite || checker.IsKing)
            {
                if (row + 1 < 8)
                {
                    if (col - 1 >= 0)
                    {
                        potentialTarget = Checkers[col - 1, row + 1];
                        if (potentialTarget == null)
                        {
                            if (!captureOnly)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col - 1, row + 1)));
                            }
                        }
                        else if (potentialTarget.IsWhite != checker.IsWhite)
                        {
                            if (row + 2 < 8 && col - 2 >= 0 && Checkers[col - 2, row + 2] == null)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col - 2, row + 2), new Coordinates(col - 1, row + 1)));
                            }
                        }
                    }
                    if (col + 1 < 8)
                    {
                        potentialTarget = Checkers[col + 1, row + 1];
                        if (potentialTarget == null)
                        {
                            if (!captureOnly)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col + 1, row + 1)));
                            }
                        }
                        else if (potentialTarget.IsWhite != checker.IsWhite)
                        {
                            if (row + 2 < 8 && col + 2 < 8 && Checkers[col + 2, row + 2] == null)
                            {
                                moves.Add(new MoveModel(new Coordinates(col, row), new Coordinates(col + 2, row + 2), new Coordinates(col + 1, row + 1)));
                            }
                        }
                    }
                }
            }
            return moves;
        }

        public bool CheckForBonus(int col, int row)
        {
            AvailableMoves.Clear();
            AvailableMoves.Add(Checkers[col, row], FindCheckerAvailableMoves(col, row, true));
            if (AvailableMoves.First().Value.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckIfBecameKing(int col, int row)
        {
            if ((Checkers[col, row].IsWhite && row == 0) || (!Checkers[col, row].IsWhite && row == 7))
            {
                if (!Checkers[col, row].IsKing)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ApplyMove(MoveModel moveToApply)
        {
            int originCol = moveToApply.OriginCell.Column;
            int originRow = moveToApply.OriginCell.Row;
            int targetCol = moveToApply.TargetCell.Column;
            int targetRow = moveToApply.TargetCell.Row;

            Checkers[targetCol, targetRow] = Checkers[originCol, originRow];
            Checkers[originCol, originRow] = null;

            if (moveToApply.CaptureCell != null)
            {
                CheckerCapture((int)(moveToApply.CaptureCell?.Column), (int)(moveToApply.CaptureCell?.Row));
            }
            if (CheckIfBecameKing(targetCol, targetRow))
            {
                Checkers[targetCol, targetRow].IsKing = true;
                return true;
            }
            return false;
            // TODO - Idk if this should return becameKing.
        }

        public void CheckerCapture(int col, int row)
        {
            Checkers[col, row] = null;
        }

        public int Evaluate()
        {
            int score = 0;
            int multiplier;
            int pieceScore = 0;
            int whiteCount = 0;
            int blackCount = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pieceScore = 0;
                    if (Checkers[i, j] != null)
                    {
                        if (Checkers[i, j].IsWhite)
                        {
                            whiteCount++;
                            multiplier = 1;
                            // Point if in Home Row.
                            if (j == 7)
                            {
                                pieceScore += 2 * multiplier;
                            }
                            // Check if in Enemy Territory
                            if (j < 3)
                            {
                                pieceScore += 2 * multiplier;
                            }
                        } 
                        else
                        {
                            // Points if in Home Row.
                            blackCount++;
                            multiplier = -1;
                            if (j == 0)
                            {
                                pieceScore += 2 * multiplier;
                            }
                            // Check if in Enemy Territory
                            if (j > 4)
                            {
                                pieceScore += 2 * multiplier;
                            }
                        }
                        // Standard points
                        pieceScore += 1 * multiplier;
                        // Points if a king
                        if (Checkers[i, j].IsKing)
                        {
                            pieceScore += 10 * multiplier;
                        }
                        // Apply pieceScore
                        score += pieceScore;
                    }
                }
            }
            if (blackCount == 0)
            {
                score += 100;
            }
            else if (whiteCount == 0)
            {
                score -= 100;
            }
            return score;
        }

        public void TurnEnd()
        {
            IsWhitePlaying = !IsWhitePlaying;
            FindAllAvailableMoves(); 
        }

        //=====================================================

        public Coordinates GetLocalCoordinates(int x, int y)
        {
            x = (int)Math.Floor((double)(x - CanvasManager.BoardPositionOnCanvas.X) / CanvasManager.CellSize.Width);
            y = (int)Math.Floor((double)(y - CanvasManager.BoardPositionOnCanvas.Y) / CanvasManager.CellSize.Height);
            return new Coordinates(x, y);
        }

        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Coordinates mousePosition = GetLocalCoordinates(e.X, e.Y);
                // Check if there is no checker in hand.
                if (ActiveChecker == null)
                {     
                    // Check if the mouse is inside the board.
                    if (mousePosition.Column >= 0 && mousePosition.Column < 8 && mousePosition.Row >= 0 && mousePosition.Row < 8)
                    {
                        // Check if the pressed cell contains a checker and it's current player's color.
                        CheckerModel clickedChecker = Checkers[mousePosition.Column, mousePosition.Row];
                        if (clickedChecker != null && clickedChecker.IsWhite == IsWhitePlaying)
                        {
                            // Check if player controls the checker (or computer).
                            if ((IsWhitePlaying && Manager.PlayerOneType == PlayerType.Human) || 
                                (!IsWhitePlaying && Manager.PlayerTwoType == PlayerType.Human))
                            {
                                // Check if checker can be moved.
                                if (AvailableMoves.ContainsKey(clickedChecker))
                                {
                                    ActiveChecker = clickedChecker;
                                    ActiveChecker.IsActive = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (MoveModel m in AvailableMoves[ActiveChecker])
                    {
                        if (m.TargetCell.Column == mousePosition.Column && m.TargetCell.Row == mousePosition.Row)
                        {
                            bool becameKing = ApplyMove(m);
                            if (m.CaptureCell != null)
                            {
                                if (!becameKing && !CheckForBonus(m.TargetCell.Column, m.TargetCell.Row))
                                {
                                    TurnEnd();
                                }
                                else if (becameKing)
                                {
                                    TurnEnd();
                                }
                            }
                            else
                            {
                                TurnEnd();
                            }
                            ActiveChecker.IsActive = false;
                            ActiveChecker = null;
                            break;
                        }
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (ActiveChecker != null)
                {
                    ActiveChecker.IsActive = false;
                    ActiveChecker = null;
                }

            }
            CanvasManager.InvalidationPending = true;
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            Coordinates mousePosition = GetLocalCoordinates(e.X, e.Y);
            if (mousePosition.Column >= 0 && mousePosition.Column < 8 && mousePosition.Row >= 0 && mousePosition.Row < 8)
            {
                if (mousePosition.Column != HighlightedCell.Column || mousePosition.Row != HighlightedCell.Row)
                {
                    HighlightedCell = mousePosition;
                    CanvasManager.InvalidationPending = true;
                }
            }
            else
            {
                if (HighlightedCell.Row != -1)
                {
                    HighlightedCell = new Coordinates(-1, -1);
                    CanvasManager.InvalidationPending = true;
                }
            }
            if (ActiveChecker != null)
            {
                CanvasManager.InvalidationPending = true;
            }
        }


    }
}
