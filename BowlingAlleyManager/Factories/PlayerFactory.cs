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
        /// <summary>
        /// The Factory Pattern is used here to create Player objects.
        /// This centralizes the creation logic, making it easier to modify
        /// without affecting other parts of the code.
        /// Instead of directly calling new Player(...), PlayerFactory.CreatePlayer()
        /// </summary>
        public static Player CreatePlayer(long id, string name, string email, string phoneNr)
        {
            return new Player (id, name, email, phoneNr);
        }
    }
}
