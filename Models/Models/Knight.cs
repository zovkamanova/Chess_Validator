using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Knight : Piece
    {
        //Initialising the Knight's abbreviation
        public override string Аbbreviation { get { return "KN"; } }

        public Knight(PieceColor color) : base(color) { }
        //Defining the rules of knight's movements. The knight moves to any of the closest squares that are not on the same rank, file, or diagonal. 
        public override void InitializeRules()
        {
            //Using simple algorithms for a knight's movements (|X2-X1|=1 and |Y2-Y1|=2) or (|X2-X1|=2 and |Y2-Y1|=1).
            Rules.Add(new Rule(
                       m => m.EndX == m.StartX + 1,
                       m => m.EndY == m.StartY + 2
                       ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY + 2
                        ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY - 2
                        ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY - 2
                        ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX + 2,
                        m => m.EndY == m.StartY + 1
                        ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX + 2,
                        m => m.EndY == m.StartY - 1
                        ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX - 2,
                        m => m.EndY == m.StartY + 1
                        ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX - 2,
                        m => m.EndY == m.StartY - 1
                        ));
        }
    }
}
