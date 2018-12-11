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

        public int AddUser(User userModel)
        {
            throw new NotImplementedException();
        }

        public bool AddUserGame(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public bool AddUserStock(int userId, int stockId)
        {
            throw new NotImplementedException();
        }

        public List<Stock> AvailableStocks()
        {
            throw new NotImplementedException();
        }

        public bool NewGame(Game gameModel)
        {
            throw new NotImplementedException();
        }

        public bool SellStock(int userId, int stockId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStocks()
        {
            throw new NotImplementedException();
        }

        public List<User> UsersPlaying(int gameId)
        {
            throw new NotImplementedException();
        }

        public List<Stock> UserStocks(int id)
        {
            throw new NotImplementedException();
        }

        public bool WipeUserGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public bool WipeUserStock()
        {
            throw new NotImplementedException();
        }
    }
}