using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            var userStocks = new UserHoldings(_dal.UserStocks(userId));
            var jsonResult = Json(userStocks, JsonRequestBehavior.AllowGet);
            return jsonResult;
            
        }

        [HttpPost]
        [Route("api/BuyStock")]
        public ActionResult BuyStock(int userId, int stockId, int shares)
        {
            bool isSuccess = false;
            if (shares > 0)
            {
                isSuccess = _dal.AddUserStock(userId, stockId, shares);
            }
            else if(shares < 0)
            {
                isSuccess = _dal.SellStock(userId, stockId, shares*-1);
            }
            JsonResult jsonResult = null;
            if (isSuccess)
            {
                bool didWork = _dal.UpdateStocks();
                var availStocks = new AvailableStocks(_dal.AvailableStocks());
                jsonResult = Json(availStocks, JsonRequestBehavior.AllowGet);
            }
            return jsonResult;
        }

        [HttpGet]
        [Route("api/Update")]
        public ActionResult UpdateStocks()
        {
            bool didWork = _dal.UpdateStocks();
            var availStocks = new AvailableStocks(_dal.AvailableStocks());
            var jsonResult = Json(availStocks, JsonRequestBehavior.AllowGet);
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