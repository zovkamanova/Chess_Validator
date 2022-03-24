using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Pawn : Piece
    {
        //Initialising the Pawn's abbreviation
        public override string Аbbreviation { get { return "PA"; } }

        public Pawn(PieceColor color) : base(color) { }
        //Defining the rules of a pawn's momevements.
        //the pawn can move forward to the unoccupied square
        //immediately in front of it on the same file, or on its first move it can
        //advance two squares along the same file, provided both squares are
        //unoccupied(black dots in the diagram); or the pawn can capture an
        //opponent's piece on a square diagonally in front of it on an adjacent
        //file, by moving to that square (black "x"s).
        //A pawn has two special moves: the en passant capture and promotion.

        public override void InitializeRules()
        {
            //Using simple rules for a pawn's movement X2=X1 and Y2-Y1=1
            Rules.Add(new Rule(

                     m => m.EndX == m.StartX,
                     m => m.EndY == m.StartY + 1
                  ));

            Rules.Add(new Rule(
                        m => m.StartY == 1,
                        m => m.EndX == m.StartX,
                        m => m.EndY == m.StartY + 2
                     ));

            Rules.Add(new Rule(

                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY + 1
                     ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY + 1
                     ));

            Rules.Add(new Rule(

                   m => m.EndX == m.StartX,
                   m => m.EndY == m.StartY - 1
                ));

            Rules.Add(new Rule(
                        m => m.StartY == 6,
                        m => m.EndX == m.StartX,
                        m => m.EndY == m.StartY - 2
                     ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX + 1,
                        m => m.EndY == m.StartY - 1
                     ));

            Rules.Add(new Rule(
                        m => m.EndX == m.StartX - 1,
                        m => m.EndY == m.StartY - 1
                     ));

        }
    }
}
