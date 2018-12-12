﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone;
using StockGameService.Models;

namespace StockGameService.Mock
{
    public class MockStockGameDal : IStockGameDAL
    {
        public List<Stock> _stocks = new List<Stock>();
        public List<UserStockItem> userStocks = new List<UserStockItem>();
        public MockStockGameDal()
        {
        }
        public int AddRoleItem(RoleItem item)
        {
            throw new NotImplementedException();
        }

        public bool AddUserGame(int userId, int gameId)
        {
            throw new NotImplementedException();
        }

        public int AddUserItem(UserItem item)
        {
            throw new NotImplementedException();
        }

        public bool AddUserStock(int userId, int stockId, int shares)
        {
            bool isAlreadyPurchased = false;
            foreach(UserStockItem stock in userStocks)
            {
                if(stock.UserStock.StockID == stockId)
                {
                    stock.Shares += shares;
                    isAlreadyPurchased = true;
                }
            }
            Stock currentStock = new Stock();
            foreach(Stock stock in _stocks)
            {
                if(stock.StockID == stockId)
                {
                    currentStock = stock;
                }
            }

            if(!isAlreadyPurchased)
            {
                userStocks.Add(new UserStockItem
                {
                    Shares = shares,
                    UserStock = currentStock
                });
            }
            return true;
        }

        public List<Stock> AvailableStocks()
        {
            Stock stock1 = new Stock()
            {
                CompanyName = "Stock One",
                CurrentPrice = 45.69,
                StockID = 1,
                Symbol = "S1"
            };
            Stock stock2 = new Stock()
            {
                CompanyName = "Stock Two",
                CurrentPrice = 45.32,
                StockID = 2,
                Symbol = "STWO"
            };

            _stocks.Add(stock1);
            _stocks.Add(stock2);
            return _stocks;
        }

        public void DeleteUserItem(int userId)
        {
            throw new NotImplementedException();
        }

        public RoleItem GetRoleItemFromReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public List<RoleItem> GetRoleItems()
        {
            throw new NotImplementedException();
        }

        public UserItem GetUserItem(int userId)
        {
            throw new NotImplementedException();
        }

        public UserItem GetUserItem(string username)
        {
            throw new NotImplementedException();
        }

        public UserItem GetUserItemFromReader(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public List<UserItem> GetUserItems()
        {
            throw new NotImplementedException();
        }

        public int NewGame(Game gameModel)
        {
            throw new NotImplementedException();
        }

        public bool SellStock(int userId, int stockId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStocks()
        {
            foreach(Stock stock in _stocks)
            {
                stock.CurrentPrice += 1;
            }
            return true;
        }

        public bool UpdateUserItem(UserItem item)
        {
            throw new NotImplementedException();
        }

        public List<UserItem> UsersPlaying(int gameId)
        {
            throw new NotImplementedException();
        }

        public List<UserStockItem> UserStocks(int id)
        {
            return userStocks;
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