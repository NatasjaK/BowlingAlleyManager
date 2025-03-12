using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    class MatchParticipation
    {
        public long MatchID { get; set; }
        public long PlayerID { get; set; }

        public MatchParticipation(long matchId, long playerId)
        {
            MatchID = matchId;
            PlayerID = playerId;
        }
    }
}
