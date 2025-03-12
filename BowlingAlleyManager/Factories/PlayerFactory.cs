using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Factories
{
    class PlayerFactory
    {
        public static Player CreatePlayer(string name, string email)
        {
            return new Player { Name = name, Email = email };
        }
    }
}
