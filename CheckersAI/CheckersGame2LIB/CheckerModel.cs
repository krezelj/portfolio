using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame2LIB
{
    public class CheckerModel
    {
        public bool IsWhite { get; set; }
        public bool IsKing { get; set; } = false;
        public bool IsActive { get; set; } = false;

        public CheckerModel(bool isWhite, bool isKing = false)
        {
            IsWhite = isWhite;
            IsKing = isKing;
        }

    }
}
