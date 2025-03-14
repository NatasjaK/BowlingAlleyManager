using BowlingAlleyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Services.Interfaces
{
    interface IPlayerService
    {
        void RegisterPlayer(string name, string email, string phoneNr);
        List<Player> GetAllPlayers();
    }
}
