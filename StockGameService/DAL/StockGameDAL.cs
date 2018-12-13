using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Capstone;

namespace Capstone
{
    public class StockGameDAL: IStockGameDAL
    {
        private string _connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        public StockGameDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddUserGame(int userId, int gameId)
        {
            
                bool result = false;

                string query = @"INSERT [User_Game] (UserId, GameId, IsReady) VALUES (@userid, @gameid, 1)";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@gameid", gameId);
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();
                    if (numberOfRowsAffected > 0)
                    {
                        result = true;
                    }
                }
                return result;
        }

        public bool AddUserStock(int userId, int stockId, int shares)
        {

            bool result = false;

            string checkQuery = @"Update [User_Stocks] Set NumberOfShares = (NumberOfShares + @shares), PurchasePrice = " +
                                        "(((Select PurchasePrice from [User_Stocks] Where UserId = @userId AND StockId = @stockId) " +
                                        "* (Select NumberOfShares from [User_Stocks] Where UserId = @userId AND StockId = @stockId)) + " +
                                        "(@shares * (Select CurrentPrice from Stock Where StockId = @stockId)))/((Select NumberOfShares from " +
                                        "[User_Stocks] where UserId = @userId AND StockId = @stockId) + @shares) WHERE UserId = @userId AND StockId = @stockid";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(checkQuery, conn);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@stockId", stockId);
                cmd.Parameters.AddWithValue("@shares", shares);
                int numberOfRowsAffected = cmd.ExecuteNonQuery();
                if (numberOfRowsAffected > 0)
                {
                    result = true;
                }
            }

            if (!result)
            {


                string query = @"INSERT [User_Stocks] (UserId, StockId, PurchasePrice, NumberOfShares) VALUES (@userId, @stockId, (Select CurrentPrice from Stock Where StockId = @stockId) , @shares)";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@stockId", stockId);
                    cmd.Parameters.AddWithValue("@shares", shares);
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();
                    if (numberOfRowsAffected > 0)
                    {
                        result = true;
                    }
                }
            }
                return result;
        }

        public List<Stock> AvailableStocks()
        {
            List<Stock> StockList = new List<Stock>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "select * from Stock";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Stock stockModel = new Stock();
                    stockModel.CompanyName = reader["CompanyName"].ToString();
                    stockModel.CurrentPrice = Convert.ToDouble(reader["CurrentPrice"]);
                    //double.Parse(reader["CurrentPrice"].ToString())
                    stockModel.StockID = (int)reader["StockID"];
                    stockModel.Symbol = reader["Symbol"].ToString();

                    StockList.Add(stockModel);
                }
            }
            return StockList;
        }

        public int NewGame(Game gameModel)
        {
                

                string query = @"INSERT INTO [Game] (Duration, TimeStarted) VALUES (@duration, @timestarted)";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@duration", gameModel.Duration);
                    cmd.Parameters.AddWithValue("@timestarted", gameModel.TimeStarted);
                    int numberOfRowsAffected = cmd.ExecuteNonQuery();
                    if (numberOfRowsAffected == 0)
                    {
                    throw new Exception();
                    }

                }


                string nextquery = @"Select GameId From [Game] where Duration = @duration AND TimeStarted = @timestarted";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(nextquery, conn);
                    cmd.Parameters.AddWithValue("@duration", gameModel.Duration);
                    cmd.Parameters.AddWithValue("@timestarted", gameModel.TimeStarted);
                    int GameID = (int)(cmd.ExecuteScalar());
                    if (GameID > 0)
                    {
                        return GameID;
                    }
                    else
                    {
                    throw new Exception();
                    }

                }
        }

        public bool SellStock(int userId, int stockId, int shares)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStocks()
        {
            Random rnd = new Random();
            double increase = rnd.Next(200);
            increase = ((increase - 100) / 1000) + 1;
            string query = "";
            for(int i = 1; i < 26; i++)
            {
                increase = rnd.Next(200);
                increase = ((increase - 100) / 1000) + 1;
                query += "Update [Stock] Set CurrentPrice = (CurrentPrice*" + increase + ") where StockId = " + i + "; "; 
            }
            int numberOfRowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                numberOfRowsAffected = cmd.ExecuteNonQuery();

            }
            if(numberOfRowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public List<UserItem> UsersPlaying(int gameId)
        {
            List<UserItem> UserList = new List<UserItem>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = "Select * From [User] " +
                                 "join [User_Game] on User_Game.UserId = User.Id " +
                                 "where GameId = @gameid";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gameid", gameId);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserItem item = new UserItem();
                    item.Id = Convert.ToInt32(reader["Id"]);
                    item.FirstName = Convert.ToString(reader["FirstName"]);
                    item.LastName = Convert.ToString(reader["LastName"]);
                    item.Username = Convert.ToString(reader["Username"]);
                    item.Email = Convert.ToString(reader["Email"]);
                    item.Salt = Convert.ToString(reader["Salt"]);
                    item.Hash = Convert.ToString(reader["Hash"]);
                    item.RoleId = Convert.ToInt32(reader["RoleId"]);
                  
                    UserList.Add(item);
                }
            }
            return UserList; 
        }

        public List<UserStockItem> UserStocks(int id)
        {
            
            throw new NotImplementedException();
        }

        public bool WipeUserGame(int gameId)
        {
            bool result = false;

            string query = @"Delete From [User_Game] where GameId = @gameId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@gameid", gameId);
                int numberOfRowsAffected = cmd.ExecuteNonQuery();
                if (numberOfRowsAffected > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool WipeUserStock()
        {
            //all user stock? or pass id?
            bool result = false;

            string checkQuery = @"DELETE From [User_Stock]";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(checkQuery, conn);
                int numberOfRowsAffected = cmd.ExecuteNonQuery();
                if (numberOfRowsAffected > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        #region UserItem Methods

        public int AddUserItem(UserItem item)
        {
            const string sql = "INSERT [User] (FirstName, LastName, Username, Email, Hash, Salt, RoleId) " +
                               "VALUES (@FirstName, @LastName, @Username, @Email, @Hash, @Salt, @RoleId);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                cmd.Parameters.AddWithValue("@RoleId", item.RoleId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateUserItem(UserItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [User] SET FirstName = @FirstName, " +
                                                 "LastName = @LastName, " +
                                                 "Username = @Username, " +
                                                 "Email = @Email, " +
                                                 "Hash = @Hash, " +
                                                 "Salt = @Salt " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Username", item.Username);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public void DeleteUserItem(int userId)
        {
            const string sql = "DELETE FROM [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.ExecuteNonQuery();
            }
        }

        public UserItem GetUserItem(int userId)
        {
            UserItem user = null;
            const string sql = "SELECT * From [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserItemFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        public List<UserItem> GetUserItems()
        {
            List<UserItem> users = new List<UserItem>();
            const string sql = "Select * From [User];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(GetUserItemFromReader(reader));
                }
            }

            return users;
        }

        public UserItem GetUserItem(string username)
        {
            UserItem user = null;
            const string sql = "SELECT * From [User] WHERE Username = @Username;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserItemFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        public UserItem GetUserItemFromReader(SqlDataReader reader)
        {
            UserItem item = new UserItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.FirstName = Convert.ToString(reader["FirstName"]);
            item.LastName = Convert.ToString(reader["LastName"]);
            item.Username = Convert.ToString(reader["Username"]);
            item.Email = Convert.ToString(reader["Email"]);
            item.Salt = Convert.ToString(reader["Salt"]);
            item.Hash = Convert.ToString(reader["Hash"]);
            item.RoleId = Convert.ToInt32(reader["RoleId"]);

            return item;
        }

        #endregion

        #region RoleItem

        public int AddRoleItem(RoleItem item)
        {
            const string sql = "INSERT RoleItem (Id, Name) " +
                               "VALUES (@Id, @Name);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.ExecuteNonQuery();
            }

            return item.Id;
        }

        public List<RoleItem> GetRoleItems()
        {
            List<RoleItem> roles = new List<RoleItem>();
            const string sql = "Select * From RoleItem;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(GetRoleItemFromReader(reader));
                }
            }

            return roles;
        }

        public RoleItem GetRoleItemFromReader(SqlDataReader reader)
        {
            RoleItem item = new RoleItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Name = Convert.ToString(reader["Name"]);

            return item;
        }

        #endregion
    }
}