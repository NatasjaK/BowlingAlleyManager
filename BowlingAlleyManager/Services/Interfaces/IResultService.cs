using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Services.Interfaces
{
    interface IResultService
    {
        void RegisterMatchResult(long matchID, Dictionary<long, int> playerScores);
        long DetermineMatchWinner(long matchID);
    }
}
