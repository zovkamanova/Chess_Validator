using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Bishop : Piece
    {
        //Initialisation of the Bishop Аbbreviation
        public override string Аbbreviation { get { return "BS"; } }

        public Bishop(PieceColor color) : base(color) { }
        //Defining the rules of bishop' movement
        //The bishop can move any number of squares diagonally, but cannot leap over other pieces
        public override void InitializeRules()
        {
            //Using simple algoritms for a bishop's movement |X2-X1|=|Y2-Y1|.
            Rules.Add(new Rule(
                 r => r.StartX - r.EndX == r.StartY - r.EndY
                 ));
            //Defining  the rule with (-) because of the module
            Rules.Add(new Rule(
                        m => m.StartX - m.EndX == -(m.StartY - m.EndY)
                        ));
        }
    }
}
