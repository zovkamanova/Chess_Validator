using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Movement
    {
        //Initialising the coordinates of a piece's movement
        public int StartX, StartY;
        public int EndX, EndY;

        public bool WithCaputure { get; internal set; }
    }
}
