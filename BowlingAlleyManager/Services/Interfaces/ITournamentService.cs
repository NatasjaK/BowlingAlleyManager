using BowlingAlleyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Services.Interfaces
{
    interface ITournamentService
    {
        void CreateTournament(string name, DateTime startDate, DateTime endDate);
        List<Tournament> GetAllTournaments();
    }
}
