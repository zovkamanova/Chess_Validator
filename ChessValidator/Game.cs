using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessValidator
{
    /// <summary>
    /// This is a class that represents the actual chess game
    /// </summary>
    public class Game
    {
        private PieceColor nextPlayerColor;

        public Board ChessBoard { get; private set; }
        //In the constructor, a new game board is created and the first player is always with the White color.
        public Game()
        {
            ChessBoard = Board.NewGame();

            nextPlayerColor = PieceColor.White;
        }
        //Defining a method that represents the actual piece's movement from its start position with (start colums and start row) to its target position (with target column and row)
        //The columns and the rows are the coordinates of a piece position
        public string Move(char fromColumn, int fromRow, char toColumn, int toRow)
        {
            //Calling the method MovePiece with its parameters which are start colum and start row and move the piece to target column and row.
            var movement = ChessBoard.MovePiece(fromColumn, fromRow, toColumn, toRow, nextPlayerColor);
            //If the movement is valid, changing the player and the color of the pieces
            if (movement.IsValid)
            {
                nextPlayerColor = (nextPlayerColor == PieceColor.White ? PieceColor.Black : PieceColor.White);
            }
            //Returning a description of the piece's movement
            return movement.ResultDescription;
        
        }
        //When movement is over, depending of the result, we call the player with its color.
        //If the movement is valid, we call the player with the opposite color, if it is not, we call the player with the same color again
        public string ShowNextPlayer()
        {
            return (nextPlayerColor == PieceColor.White ? "WHITE" : "BLACK");
        }

    }
}

