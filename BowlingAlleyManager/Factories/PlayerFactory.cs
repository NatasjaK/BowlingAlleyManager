using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BowlingAlleyManager.Models;

namespace BowlingAlleyManager.Factories
{
    class PlayerFactory
    {
        public static Player CreatePlayer(long id, string name, string email, string phoneNr)
        {
            return new Player (id, name, email, phoneNr);
        }
    }
}
