using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone;

namespace Capstone
{
    public class StockGameDAL: IStockGameDAL
    {
        private string _connectionString;

        public StockGameDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Stock> UserStocks(int id)
        {
            throw new NotImplementedException();
        }
    }
}