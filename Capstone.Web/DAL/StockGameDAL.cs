using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
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