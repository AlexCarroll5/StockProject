using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockGameService.Models
{
    public class UserCash
    {
        public double CurrentCash { get; set; }
        public double TotalCash { get; set; }
        public int IdOfUser { get; set; }
    }
}
