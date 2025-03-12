using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    public class Result
    {
        public long MatchID { get; set; }
        public long PlayerID { get; set; }
        public long Score { get; set; }

        public Result(long matchID, long playerID, long score)
        {
            MatchID = matchID;
            PlayerID = playerID;
            Score = score;
        }
    }
}
