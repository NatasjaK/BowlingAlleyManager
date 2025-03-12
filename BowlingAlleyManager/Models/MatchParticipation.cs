using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    class MatchParticipation
    {
        public int MatchID { get; set; }
        public int PlayerID { get; set; }

        public MatchParticipation(int matchId, int playerId)
        {
            MatchID = matchId;
            PlayerID = playerId;
        }
    }
}
