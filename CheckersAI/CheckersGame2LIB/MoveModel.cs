using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame2LIB
{
    

    public class MoveModel
    {
        public Coordinates OriginCell { get; set; }
        public Coordinates TargetCell { get; set; }
        public Coordinates? CaptureCell { get; set; }
        public int MoveValue { get; set; } = 0;

        public MoveModel(Coordinates origin, Coordinates target, Coordinates? capture = null)
        {
            OriginCell = origin;
            TargetCell = target;
            CaptureCell = capture;
        }
    }

    public struct Coordinates
    {
        public int Column;
        public int Row;

        public Coordinates(int col, int row)
        {
            Column = col;
            Row = row;
        }

    }
}
