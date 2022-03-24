using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    /// <summary>
    /// The abstract class Piece is the base class of all the pieces in the game
    /// </summary>
    public abstract class Piece
    {
        //Assigning what type of piece is this
        public abstract string Аbbreviation { get; }
        //Assinging the Color of the piece - White or Black
        public char Color { get; private set; }
        //Assining a property that indicates if a piece is still alive or not
        public Boolean IsAlive { get; set; }
        //Setting a colleection of pieces' movements rules
        protected Collection<Rule> Rules;
      
        //Creating a parametrized constructor for a piece
        public Piece(PieceColor color)
        {
            Rules = new Collection<Rule>();

            Color = (color == PieceColor.White ? 'W' : 'B');

            IsAlive = true;

            InitializeRules();
        }
        //Creating a method that indicates if a piece's movement is valid or not
        public bool IsValidMovement(bool withCaputure, int startRow, int startColumn, int endRow, int endColumn)
        {
            var movement = new Movement
            {
                WithCaputure = withCaputure,
                StartX = startColumn,
                StartY = startRow,
                EndX = endColumn,
                EndY = endRow
            };
            //Here we validate the rules and return a boolean result
            return Rules.Where(r => r.Validate(movement)).Any();
        }
        //Initializing the method that is going to be overrired in the other pieces' classes. The rules are going to be implemented here
        public abstract void InitializeRules();
    }
}
