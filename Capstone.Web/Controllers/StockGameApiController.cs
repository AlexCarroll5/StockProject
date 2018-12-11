using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockGameService.Models;

namespace Capstone
{
    public class StockGameApiController : StockGameBaseController
    {
        private IStockGameDAL _dal;

        public StockGameApiController(IStockGameDAL dal) : base(dal)
        {
            _dal = dal;
        }

        [HttpGet]
        [Route("api/ReadyUsers")]
        public ActionResult GetReadyUsers(int gameId)
        {
            var readyUsers = new ReadyUsers(_dal.UsersPlaying(gameId));
            var jsonResult = Json(readyUsers, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpGet]
        [Route("api/ListOfAvailableStocks")]
        public ActionResult GetStocks()
        {
            var availStocks = new AvailableStocks(_dal.AvailableStocks());
            var jsonResult = Json(availStocks, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpGet]
        [Route("api/UserStocks")]
        public ActionResult GetStocksByUser(int userId)
        {
            var userStocks = new AvailableStocks(_dal.UserStocks(userId), true);
            var jsonResult = Json(userStocks, JsonRequestBehavior.AllowGet);
            return jsonResult;
            
        }

        [HttpPost]
        [Route("api/BuyStock")]
        public ActionResult BuyStock(int userId, int stockId)
        {
            bool isSuccess = _dal.AddUserStock(userId, stockId);
            JsonResult jsonResult = null;
            if (isSuccess)
            {
                var availStocks = new AvailableStocks(_dal.AvailableStocks());
                jsonResult = Json(availStocks, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }
        
        //[HttpPost]
        //[Route("api/ImReady")]
        //public ActionResult ImReady(int userId, int gameId)
        //{
        //    bool isSuccess = _dal.
        //}
    }
}