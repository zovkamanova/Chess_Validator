using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Result
    {
        public Result()
        {
            IsValid = true;
            ResultDescription = "The movement is correct.";
        }
        //Initialising a property that defines if a player's movement is valid
        public bool IsValid { get; set; }
        //Defing a property to describe player's movement result
        public string ResultDescription { get; set; }
        //Initialising a property to define if the movement is with a capture
        public bool Capture { get; set; }
        //Initialising the captured piece
        public Piece CapturedPiece { get; set; }
    }
}
