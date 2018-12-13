using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone
{

    /// <summary>
    /// Testing our DAL Methods
    /// Getting data from database, "NPGeek"
    /// </summary>
    [TestClass]
    public class DALIntegrationTests
    {
        private TransactionScope _tran;
        private string _connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = StockGame; Integrated Security = True";

        /*
        * SETUP.
        */
        [TestInitialize]
        public void Initialize()
        {
            _tran = new TransactionScope();

            //using (SqlConnection conn = new SqlConnection(_connectionString))
            //{
            //    //SqlCommand cmd;
            //    conn.Open();
            //}
        }

        /*
        * CLEANUP.
        * Rollback the Transaction.  
        */
        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        /*
        * TEST.
        */
        [TestMethod]
        public void AddUserGame()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            int userId = 4;
            int gameId = 3;

            //Act
            bool test = _dal.AddUserGame(userId, gameId);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void AddUserStock()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            int userId = 4;
            int stockId = 1;
            int shares = 1;

            //Act
            bool test = _dal.AddUserStock(userId, stockId, shares);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void AvailableStocks()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);

            //Act
            List<Stock> test = _dal.AvailableStocks();

            //Assert
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void NewGame()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            Game game = new Game()
            {
                TimeStarted = DateTime.Now,
                Duration = 10
            };

            //Act
            int test = _dal.NewGame(game);

            //Assert
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void SellStock()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            int userId = 1;
            int stockId = 1;
            int shares = 1;

            //Act
            bool test = _dal.SellStock(userId, stockId, shares);

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void UpdateStocks()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);

            //Act
            bool test = _dal.UpdateStocks();

            //Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void UsersPlaying()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            int gameId = 3;

            //Act
            List<UserItem> test = _dal.UsersPlaying(gameId);

            //Assert
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void UserStocks()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            int id = 1;

            //Act
            List<UserStockItem> test = _dal.UserStocks(id);

            //Assert
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void WipeUserGame()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);
            int gameId = 1;

            //Act
            bool test = _dal.WipeUserGame(gameId);

            //Assert
            Assert.IsTrue(test);
        }
    }

}
