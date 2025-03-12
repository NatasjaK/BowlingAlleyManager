using BowlingAlleyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Factories
{
    class MatchFactory
    {
        public static Match CreateMatch(int matchID, List<Player> players, int lane)
        {
            return new Match(matchID, players, lane);
        }
    }
}
