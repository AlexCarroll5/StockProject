﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public class StockGameDAL: IStockGameDAL
    {
        private string _connectionString;

        public StockGameDAL(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}