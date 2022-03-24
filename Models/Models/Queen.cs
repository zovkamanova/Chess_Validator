using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Queen : Piece
    {
        //Initialising the Queen's Abbreviation
        public override string Аbbreviation { get { return "QN"; } }

        public Queen(PieceColor color) : base(color) { }
        //Defininng the rules of the queen's movement
        //the queen combines the power of a rook and bishop and can move
        //any number of squares along a rank, file, or diagonal,
        //but cannot leap over other pieces.
        public  override void InitializeRules()
        {
            Rules.Add(new Rule(
                       m => m.StartX - m.EndX == m.StartY - m.EndY
                     ));

            Rules.Add(new Rule(
                      m => m.StartX - m.EndX == -(m.StartY - m.EndY)
                      ));

            Rules.Add(new Rule(
                       m => m.EndX == m.StartX
                       ));

            Rules.Add(new Rule(
                        m => m.EndY == m.StartY
                        ));
        }
    }
}
