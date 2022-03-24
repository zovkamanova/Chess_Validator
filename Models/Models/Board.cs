using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Board
    {
        //Creating an array of chars that represents the 8 colums of the chess board. They are denoted from A to H,according to White's perspective
        public static char[] Letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        //Creating a dictionary where on every char(letter/column) responds a number (thar represents the rows of the chess board (1-8), from White's perspective)
        public static Dictionary<char, int> Columns = new Dictionary<char, int>() {
                { 'A', 1 }, { 'B', 2 }, { 'C', 3 }, { 'D', 4 }, { 'E', 5 }, { 'F', 6 }, { 'G', 7 }, { 'H', 8 }
        };
        //Creating an object with all of the cases that are going to occur in the game
        private Object[,] cases;
        //Generating a list of all pieces in the game
        private IList<Piece> pieces;

        public object[,] Cases() => cases;

        private Board()
        {
            cases = new Object[8, 8];
            pieces = new List<Piece>();
        }
        //Initialising a new Board and Initialise the game
        public static Board NewGame()
        {
            var board = new Board();

            board.InitGame();

            return board;
        }

        public static Board NewBoard() => new Board();
        //GetPiece() method returns a piece, based on its position on the board, depending on its column and row.
        public Piece GetPiece(char column, int row) => cases[row - 1, Columns[column] - 1] as Piece;

        //SetWhitePiece sets a column, a row and white color of a piece, adds it to the list of pieces and returns it as a result
        //We use generic, because cannot create an instance of the abstract class Piece
        public T SetWhitePiece<T>(char column, int row) where T : Piece
        {
            T piece = Activator.CreateInstance(typeof(T), PieceColor.White) as T;
            pieces.Add(piece);

            SetPiece(piece, column, row);

            return piece;
        }
        //SetBlackPiece sets a column, a row and black color of a piece,adds it to the list of pieces and returns, and returns it as a result
        //We use generic, because cannot create an instance of the abstract class Piece
        public T SetBlackPiece<T>(char column, int row) where T : Piece
        {
            T piece = Activator.CreateInstance(typeof(T), PieceColor.Black) as T;
            pieces.Add(piece);

            SetPiece(piece, column, row);

            return piece;
        }

        //This is the method that initialises the current game, set all the white and black pieces
        public void InitGame()
        {
            foreach (var column in Columns.Keys)
            {

                SetWhitePiece<Pawn>(column, 2);
                SetBlackPiece<Pawn>(column, 7);
            }

            SetAllWhitePieces();
            SetAllBlackPieces();

        }

        //In this methos we set all of the white pieces
        public void SetAllWhitePieces()
        {
            SetWhitePiece<Rook>('A', 1);
            SetWhitePiece<Rook>('H', 1);

            SetWhitePiece<Knight>('B', 1);
            SetWhitePiece<Knight>('G', 1);

            SetWhitePiece<Bishop>('C', 1);
            SetWhitePiece<Bishop>('F', 1);

            SetWhitePiece<Queen>('D', 1);

            SetWhitePiece<King>('E', 1);
        }
        //In this method we set all of the black pieces
        public void SetAllBlackPieces()
        {

            SetBlackPiece<Rook>('A', 8);
            SetBlackPiece<Rook>('H', 8);

            SetBlackPiece<Knight>('B', 8);
            SetBlackPiece<Knight>('G', 8);

            SetBlackPiece<Bishop>('C', 8);
            SetBlackPiece<Bishop>('F', 8);

            SetBlackPiece<Queen>('D', 8);

            SetBlackPiece<King>('E', 8);
        }

        //Initialising a method that moves a piece and returns the result with all of its properties
        public Result MovePiece(char column, int row, char targetColumn, int targetRow, PieceColor? color = null)
        {
            var result = new Result();

            if (Columns[targetColumn] < 1 || Columns[targetColumn] > 8 || targetRow < 1 || targetRow > 8)
            {
                result.IsValid = false;
                result.ResultDescription = "Target position is out of bounds.";

                return result;
            }

            if ((column == targetColumn) && (row == targetRow))
            {
                result.IsValid = false;
                result.ResultDescription = "Current position and target position are equal.";

                return result;
            }

            var startPiece = GetPiece(column, row);

            // check if there is a piece at start position
            if (startPiece == null)
            {
                result.IsValid = false;
                result.ResultDescription = $"The piece was not found at position {column}{row.ToString()}";

                return result;
            }

            // check color of piece
            if (color.HasValue)
            {
                if ((color == PieceColor.White && startPiece.Color == 'B') ||
                    (color == PieceColor.Black && startPiece.Color == 'W'))
                {
                    result.IsValid = false;
                    result.ResultDescription = $"The selected piece is the wrong color.";

                    return result;
                }
            }

            var endPiece = GetPiece(targetColumn, targetRow);

            // check it is a valid movement for piece (rules piece validator)
            if (!startPiece.IsValidMovement(
                (endPiece != null && !startPiece.Color.Equals(endPiece.Color)),
                row - 1, Columns[column] - 1, targetRow - 1, Columns[targetColumn] - 1))
            {
                result.IsValid = false;
                result.ResultDescription =
                    String.Format("Wrong move: {0}{1} - {2}{3}",

                    column, row.ToString(), targetColumn, targetRow.ToString());

                return result;
            }

            // check if the path is free if piece is not a knight
            if (!(startPiece is Knight) && !CheckIfPathIsFree(column, row, targetColumn, targetRow))
            {
                result.IsValid = false;
                result.ResultDescription =
                    String.Format("The path from {0}{1} to {2}{3} for {4}{5} is not available.",
                     column, row.ToString(), targetColumn, targetRow.ToString(),
                     startPiece.Color.ToString(), startPiece.GetType().Name);

                return result;
            }

            // check if target position there is already present a piece with same color
            if (endPiece != null && startPiece.Color.Equals(endPiece.Color))
            {
                result.IsValid = false;
                result.ResultDescription =
                    String.Format(" A {0} piece at position {1}{2} already exists!",
                    startPiece.Color.ToString(), targetColumn, targetRow);

                return result;
            }

            // set result information after capture
            result.Capture = (endPiece != null && !startPiece.Color.Equals(endPiece.Color));
            if (result.Capture)
            {
                result.CapturedPiece = endPiece;
                endPiece.IsAlive = false;
            }

            // change position of piece
            SetPiece(startPiece, targetColumn, targetRow);
            ClearPiecePosition(column, row);

            return result;
        }

        //Setting a piece in a position
        private void SetPiece(Piece piece, char column, int row)
        {
            cases[row - 1, Columns[column] - 1] = piece;
        }

        //After the piece's movement, we have to clear the piece previous position
        private void ClearPiecePosition(char column, int row)
        {
            cases[row - 1, Columns[column] - 1] = null;
        }

        //Checking if the path of the selected piece is free
        private bool CheckIfPathIsFree(char startColumn, int startRow, char targetColumn, int targetRow)
        {
            bool isFree = true;

            int stepX = (Columns[targetColumn] - 1).CompareTo(Columns[startColumn] - 1);

            int stepY = targetRow.CompareTo(startRow);

            // start position
            int startColumnPosition = Columns[startColumn] - 1;
            int startRowPosition = startRow - 1;

            // next position
            startColumnPosition += stepX;
            startRowPosition += stepY;

            while (!(startColumnPosition == (Columns[targetColumn] - 1) && startRowPosition == (targetRow - 1)))
            {
                var piece = cases[startRowPosition, startColumnPosition];

                if (piece != null)
                {
                    isFree = false;
                    break;
                }

                // next position
                startColumnPosition += stepX;
                startRowPosition += stepY;
            }

            return isFree;
        }

    }
}

