using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Rule
    {
        //Initialising a predicate of a piece's movement
        private readonly Predicate<Movement>[] _predicates;

        internal Rule(params Predicate<Movement>[] predicates)
        {
            _predicates = predicates;
        }
        //Generating a metod that validates a piece's movement
        public bool Validate(Movement movement)
        {
            return !_predicates.Where(p => !p(movement)).Any();
        }
    }
}
