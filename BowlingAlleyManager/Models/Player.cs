using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNr { get; set; }

        public Player(string name, string email, string phoneNr)
        {
            Name = name;
            Email = email;
            PhoneNr = phoneNr;
        }
    }
}
