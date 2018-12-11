using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime Duration { get; set; }
    }
}