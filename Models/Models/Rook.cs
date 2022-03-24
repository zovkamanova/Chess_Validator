using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Rook : Piece
    {
        //Initialising the rook's abbreviation
        public override string Аbbreviation { get { return "RK"; } }

        public Rook(PieceColor color) : base(color) { }
        //Initialising the rules of the rook's movements.
        //The rook can move any number of squares along a rank or file
        //but cannot leap over other pieces. Along with the king,
        //a rook is involved during the king's castling move.

        public override void InitializeRules()
        {
            //Using simple algorithms for a rook's movements X2=X1 or Y2=Y1.
            Rules.Add(new Rule(
                      m => m.EndX == m.StartX
                      ));

            Rules.Add(new Rule(
                      m => m.EndY == m.StartY
                      ));
        }
    }
}
