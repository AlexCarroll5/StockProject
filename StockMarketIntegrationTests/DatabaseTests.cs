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
        public void AvailableStocks()
        {
            //Arrange
            StockGameDAL _dal = new StockGameDAL(_connectionString);

            //Act
            List<Stock> test = _dal.AvailableStocks();

            //Assert
            Assert.IsNotNull(test);
        }

        
    }

}
