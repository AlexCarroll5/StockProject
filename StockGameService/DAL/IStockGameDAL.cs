using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;

namespace Capstone
{
    public interface IStockGameDAL
    {
        List<Stock> UserStocks(int id); // gets list of stocks of available stocks
        List<User> UsersPlaying(int gameId); // gets list of users that have isReady True
        List<Stock> AvailableStocks(); // gets list of available stocks to buy
        int AddUser(User userModel); // adds a user to the database and returns the user id
        int NewGame(Game gameModel); // creates a new game... adds a row to the game table. returns true if successful
        bool AddUserGame(int userId, int gameId); // adds a user_game row when a user logs in
        bool AddUserStock(int userId, int stockId); //adds a user_stock row when a user purchases a stock
        bool SellStock(int userId, int stockId); // updates the amount of shares a user has for a stock
        bool WipeUserGame(int gameId); // wipes all the rows from user_game when a game is complete
        bool WipeUserStock(); // wipes all the rows from user_stock when a game is complete
        bool UpdateStocks(); // updates the price of the stocks with new values
    }
}
