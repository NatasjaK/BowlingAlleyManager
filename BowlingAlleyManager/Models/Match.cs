﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    public class Match
    {
        public int MatchID { get; set; }
        public List<Player> Players { get; set; }
        public int Lane { get; set; }
        public Result? MatchResult { get; set; }

        public Match(int id, List<Player> players, int lane)
        {
            MatchID = id;
            Players = players;
            Lane = lane;
            MatchResult = null;
        }
    }
}
