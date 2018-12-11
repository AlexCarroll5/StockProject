﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;

namespace Capstone
{
    public interface IStockGameDAL
    {
        //Mchael Methods
        List<Stock> UserStocks(int id); // gets list of stocks of available stocks
        List<UserItem> UsersPlaying(int gameId); // gets list of users that have isReady True
        List<Stock> AvailableStocks(); // gets list of available stocks to buy
        int NewGame(Game gameModel); // creates a new game... adds a row to the game table. returns true if successful
        bool AddUserGame(int userId, int gameId); // adds a user_game row when a user logs in
        bool AddUserStock(int userId, int stockId); //adds a user_stock row when a user purchases a stock
        bool SellStock(int userId, int stockId); // updates the amount of shares a user has for a stock
        bool WipeUserGame(int gameId); // wipes all the rows from user_game when a game is complete
        bool WipeUserStock(); // wipes all the rows from user_stock when a game is complete
        bool UpdateStocks(); // updates the price of the stocks with new values

        //User Item Methods from Vending Machine
        #region UserItem Methods

        int AddUserItem(UserItem item);

        bool UpdateUserItem(UserItem item);

        void DeleteUserItem(int userId);

        UserItem GetUserItem(int userId);

        List<UserItem> GetUserItems();//used earlier, but different implementation?

        UserItem GetUserItem(string username);

        UserItem GetUserItemFromReader(SqlDataReader reader);

        #endregion

        //Role Item Methods from Vending Machine
        #region RoleItem

        int AddRoleItem(RoleItem item);

        List<RoleItem> GetRoleItems();

        RoleItem GetRoleItemFromReader(SqlDataReader reader);

        #endregion
    }
}
