using BowlingAlleyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Services.Interfaces
{
    interface IMatchService
    {
        void CreateMatch(List<Player> players, int lane);
        List<Match> GetAllMatches();
        List<Player> GetPlayersInMatch(long matchID);
    }
}
