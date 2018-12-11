﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;

namespace StockGameService.Models
{
    public class UserStockItem
    {
        public Stock UserStock { get; set; }
        public int Shares { get; set; }
        public double Value {
            get
            {
                return UserStock.CurrentPrice * Shares;
            }
        }
    }
}
