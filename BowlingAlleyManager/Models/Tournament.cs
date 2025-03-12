using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    class Tournament
    {
        public int TournamentID { get; set; }
        public string Name { get; set; }
        public List<Match> Matches { get; set; }

        public Tournament(int id, string name)
        {
            TournamentID = id;
            Name = name;
            Matches = new List<Match>();
        }
    }
}
