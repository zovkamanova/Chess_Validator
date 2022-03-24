using System;
using System.Text.RegularExpressions;
using ChessValidator;

namespace Chess.Player
{
    class Program
    {
        //Creating a basic method that matches the user input when a movement is made, in case a dummy expression is entered
        public static bool MatchInputTemplate(String input)
        {
            string pattern = @"[a-zA-Z][0-9]+-[a-zA-Z][0-9]+";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }
        //This is the entry point of the appcilation. After running the program, the game starts.
        static void Main(string[] args)
        {
            Console.WriteLine("Chess game started! ");
            //Creating a new game on every run of the application
            var game = new Game();
            //Creating a variable that indicates if the game is finished or not
            var finished = false;
        
            //Creating a list with wrong results
            List<string> WrongResults = new List<string>();
            //While the game is not finished, the player has to make a choice - to make another move or to finish the game implicitly
            while (!finished)
            {
                Console.WriteLine("Make a choice (m -> make a move or f->finish game) ?");
                //Initialising the command that has to be entered through the console
                var command = Console.ReadLine();

                switch (command)
                {
                    //if the player presses m, they have to make a move incitating the selected piece and the target pieve (For example: E2-E4)
                    case "m":
                        Console.WriteLine("Move a piece on Chessboard:");

                        Console.Write("Example(E2-E4): ");

                        var source = Console.ReadLine();
                        //Calling the MatchInputTemplate to ensure that the movement expression is valid
                        bool matchTemplate = MatchInputTemplate(source);
                        if (matchTemplate != true)
                        {
                            Console.WriteLine("Invalid movement! Try again with the right template. Example: (E2-E4)");
                            source = Console.ReadLine();
                        }

                        //from the source, we take the first charachter and make it upper, to avoid errors related to the size of the letters. The first character represents the selected column
                        var fromColumn = source.ToUpper()[0];

                        //Get the second character that represents the row of the selected piece
                        var fromRow = Convert.ToInt32(source.Substring(1, 1));

                        //from the source, we take the fourth charachter and make it upper, to avoid errors related to the size of the letters. The fourth character represents the target column
                        var toColumn = source.ToUpper()[3];
                        //Get the fifth character that represents the row of the target piece
                        var toRow = Convert.ToInt32(source.Substring(4, 1));

                        //Creating the actual string of the startPosition
                        string startPosition = fromColumn + fromRow.ToString();

                        //If the length of the start position is not equal to 2, this means that the selected posiotion is not valid
                        if (startPosition.Length != 2)
                        {
                            Console.WriteLine("The start position is not valid: Please see the example (E2)!");
                            break;
                        }

                        //   //Creating the actual string of the endPosition
                        string entPosition = toColumn + toRow.ToString();

                        //If the length of the end position is not equal to 2, this means that the end posiotion is not valid
                        if (entPosition.Length != 2)
                        {
                            Console.WriteLine("The start position is not valid: Please see the example (E4)!");
                            break;
                        }

                        //Making the actual move and returning the result
                        var result = game.Move(fromColumn, fromRow, toColumn, toRow);
                       
                        //If the movee is not valid, we return an error message
                        if (result.Contains("Wrong move"))
                        {
                            WrongResults.Add(result);
                            Console.WriteLine(result);
                            Console.WriteLine("You have to enter a valid move! Otherwise you cannot continue!");
                            Console.WriteLine($"The next player is {game.ShowNextPlayer()} again!");
                        }
                        else
                        {
                            //Otherwise, we call the other player
                            Console.WriteLine($"The next player is {game.ShowNextPlayer()}");
                        }
                        break;
                    //if the player presses f, the game finished
                    case "f":
                        finished = true;
                        if (WrongResults.Count == 0)
                        {
                            Console.WriteLine("All moves are valid!");
                        }
                        break;
                    //If a user enter a command, different from f(finish) and m(make a move), the program returns an error message.
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }

            Console.WriteLine("End of program.");
        }

    }
}
