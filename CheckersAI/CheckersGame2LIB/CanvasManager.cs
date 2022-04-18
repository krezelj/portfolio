using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersGame2LIB
{
    public static class CanvasManager
    {
        public static Form BoardCanvas { get; set; }

        public static bool InvalidationPending { get; set; } = true;

        public static BoardModel Board { get { return Manager.CurrentBoard; } }

        // Board properties

        public static Point BoardPositionOnCanvas { get; set; } = new Point(80, 80);

        public static Size BoardSize { get; set; } = new Size(640, 640);

        // Cells properties

        public static Size CellSize { get; set; } = new Size(80, 80);

        private static SolidBrush _whiteCellBrush = new SolidBrush(Color.FromArgb(230, 230, 230));

        private static SolidBrush _blackCellBrush = new SolidBrush(Color.FromArgb(80, 160, 80));

        private static SolidBrush _highlightedCellBrush = new SolidBrush(Color.FromArgb(200, 180, 50));

        private static SolidBrush _currentCellBrush;

        private static SolidBrush _availableCellIndicatorBrush = new SolidBrush(Color.FromArgb(200, 200, 75));

        private static double _indicatorMargin = 0.3;

        // Checkers properties

        public static CheckerModel AnimatedChecker { get; set; } = null;

        public static Point AnimatedCheckerPositionOnCanvas { get; set; }

        private static double _margin = 0.125;

        public static Size CheckerSize { get; set; } = new Size((int)(CellSize.Width * (1 - 2 * _margin)), (int)(CellSize.Height * (1 - 2 * _margin)));

        private static SolidBrush _whiteCheckerBrush = new SolidBrush(Color.FromArgb(255, 240, 180));

        private static SolidBrush _blackCheckerBrush = new SolidBrush(Color.FromArgb(10, 10, 30));

        private static SolidBrush _currentCheckerBrush;

        private static Pen _checkerCanMoveIndicatorPen = new Pen(Color.FromArgb(120, 230, 20), 6);

        private static SolidBrush _checkerIsKingIndicatorBrush = new SolidBrush(Color.FromArgb(230, 200, 50));


        public static double Smoothstep(double x)
        {
            return x * x * (3 - 2 * x);
        }

        public static void AnimateMove(MoveModel moveToAnimate)
        {
            AnimatedChecker = Board.Checkers[moveToAnimate.OriginCell.Column, moveToAnimate.OriginCell.Row];
            double newX;
            double newY;
            int dX = (moveToAnimate.TargetCell.Column - moveToAnimate.OriginCell.Column) * CellSize.Width;
            int dY = (moveToAnimate.TargetCell.Row - moveToAnimate.OriginCell.Row) * CellSize.Height;
            int frameCount = 100;
            for (int i = 0; i < frameCount; i++)
            {
                newX = CellSize.Width * (moveToAnimate.OriginCell.Column + _margin) + BoardPositionOnCanvas.X + dX * Smoothstep((double)i / frameCount);
                newY = CellSize.Height * (moveToAnimate.OriginCell.Row + _margin) + BoardPositionOnCanvas.Y + dY * Smoothstep((double)i / frameCount);
                AnimatedCheckerPositionOnCanvas = new Point((int)newX, (int)newY);
                try
                {
                    BoardCanvas.Invoke((MethodInvoker)delegate { BoardCanvas.Invalidate(); });
                }
                catch (Exception) { }
                Thread.Sleep(Manager.AnimationLength / frameCount);
            }
            AnimatedChecker = null;
        }

        public static void RedrawAll(Graphics g)
        {
            // Draw board
            // Draw board cells
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == Board.HighlightedCell.Column && j == Board.HighlightedCell.Row)
                    {
                        _currentCellBrush = _highlightedCellBrush;
                    }
                    else if ((i + j) % 2 == 0)
                    {
                        _currentCellBrush = _blackCellBrush;
                    }
                    else
                    {
                        _currentCellBrush = _whiteCellBrush;
                    }
                    g.FillRectangle(_currentCellBrush,
                                        i * CellSize.Width + BoardPositionOnCanvas.X,
                                        j * CellSize.Height + BoardPositionOnCanvas.Y,
                                        CellSize.Width, CellSize.Height);
                    //
                }
            }
            // Draw checkers
            bool canMove = false;
            bool isKing = false;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    canMove = false;
                    isKing = false;
                    if (Board.Checkers[i, j] != null && !Board.Checkers[i, j].IsActive && Board.Checkers[i, j] != AnimatedChecker)
                    {
                        // Pick color
                        if (Board.Checkers[i, j].IsWhite)
                        {
                            _currentCheckerBrush = _whiteCheckerBrush;
                        }
                        else
                        {
                            _currentCheckerBrush = _blackCheckerBrush;
                        }
                        // Determine indicators
                        if (Board.AvailableMoves.ContainsKey(Board.Checkers[i, j]) &&
                           ((Board.IsWhitePlaying && Manager.PlayerOneType == PlayerType.Human) ||
                           (!Board.IsWhitePlaying && Manager.PlayerTwoType == PlayerType.Human)))
                        {
                            canMove = true;
                        }
                        if (Board.Checkers[i, j].IsKing)
                        {
                            isKing = true;
                        }
                        // Draw checker
                        DrawChecker(g, _currentCheckerBrush,
                                   (int)(CellSize.Width * (i + _margin) + BoardPositionOnCanvas.X),
                                   (int)(CellSize.Height * (j + _margin) + BoardPositionOnCanvas.Y),
                                   canMove, isKing);
                    }
                }
            }
            // Draw available moves for the active checker
            isKing = false;
            canMove = false;
            if (Board.ActiveChecker != null)
            {
                foreach (MoveModel m in Board.AvailableMoves[Board.ActiveChecker])
                {
                    g.FillEllipse(_availableCellIndicatorBrush,
                                  (int)(CellSize.Width * (m.TargetCell.Column + _indicatorMargin) + BoardPositionOnCanvas.X),
                                  (int)(CellSize.Height * (m.TargetCell.Row + _indicatorMargin) + BoardPositionOnCanvas.Y),
                                  (int)(CellSize.Width * (1 - 2 * _indicatorMargin)),
                                  (int)(CellSize.Height * (1 - 2 * _indicatorMargin)));
                }
            }      
            // Draw active checker
            if (Board.ActiveChecker != null)
            {
                // Pick color
                if (Board.ActiveChecker.IsWhite)
                {
                    _currentCheckerBrush = _whiteCheckerBrush;
                }
                else
                {
                    _currentCheckerBrush = _blackCheckerBrush;
                }
                // Determine indicators
                if (Board.ActiveChecker.IsKing)
                {
                    isKing = true;
                }
                canMove = true;
                // Draw checker
                DrawChecker(g, _currentCheckerBrush,
                           (BoardCanvas.PointToClient(Cursor.Position).X - CheckerSize.Width / 2),
                           (BoardCanvas.PointToClient(Cursor.Position).Y - CheckerSize.Height / 2),
                           canMove, isKing);
            }
            // Draw animated checker
            isKing = false;
            canMove = false;
            if (AnimatedChecker != null)
            {
                // Pick color
                if (AnimatedChecker.IsWhite)
                {
                    _currentCheckerBrush = _whiteCheckerBrush;
                }
                else
                {
                    _currentCheckerBrush = _blackCheckerBrush;
                }
                // Determine indicators
                if (AnimatedChecker.IsKing)
                {
                    isKing = true;
                }
                canMove = true;
                // Draw checker
                DrawChecker(g, _currentCheckerBrush,
                           AnimatedCheckerPositionOnCanvas.X,
                           AnimatedCheckerPositionOnCanvas.Y,
                           canMove, isKing);
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            InvalidationPending = false;
        }

        public static void DrawChecker(Graphics g, SolidBrush checkerBrush, int posX, int posY, bool canMove, bool isKing)
        {
            g.FillEllipse(checkerBrush, posX, posY, CheckerSize.Width, CheckerSize.Height);
            if (canMove)
            {
                g.DrawEllipse(_checkerCanMoveIndicatorPen,
                              posX, 
                              posY,
                              CheckerSize.Width, CheckerSize.Height);
            }
            if (isKing)
            {
                g.FillEllipse(_checkerIsKingIndicatorBrush,
                              posX + (int)(CheckerSize.Width * _indicatorMargin),
                              posY + (int)(CheckerSize.Height * _indicatorMargin),
                              (int)(CheckerSize.Width * (1 - 2 * _indicatorMargin)),
                              (int)(CheckerSize.Height * (1 - 2 * _indicatorMargin)));
            }
        }

    }
}
