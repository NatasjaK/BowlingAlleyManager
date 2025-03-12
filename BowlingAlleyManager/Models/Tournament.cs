using System;
using System.Collections.Generic;

namespace BowlingAlleyManager.Models
{
    public class Tournament
    {
        public long TournamentID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Match> Matches { get; set; } = new List<Match>();

        public Tournament() { }

        public Tournament(long id, string name, DateTime startDate, DateTime endDate)
        {
            TournamentID = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
