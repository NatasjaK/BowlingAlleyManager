using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    class Result
    {
        public Dictionary<Player, int> Scores { get; set; }
        public Player Winner { get; set; }

        public Result(Dictionary<Player, int> scores)
        {
            Scores = scores;
            Winner = DetermineWinner(scores);
        }

        private Player DetermineWinner(Dictionary<Player, int> scores)
        {
            return scores.OrderByDescending(s => s.Value).First().Key;
        }
    }
}
