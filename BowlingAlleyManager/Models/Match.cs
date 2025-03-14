using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    public class Match
    {
        public long MatchID { get; set; }
        public List<Player> Players { get; set; }
        public int Lane { get; set; }
        public Result? MatchResult { get; set; }
        public long? WinnerID { get; set; }

        public Match()
        {
            Players = new List<Player>(); 
        }

        public Match(long id, List<Player> players, int lane)
        {
            MatchID = id;
            Players = players;
            Lane = lane;
            MatchResult = null;
            WinnerID = null;
        }
    }
}
