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
        public ActionResult GetReadyUsers()
        {
            var jsonResult = Json(_dal, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpGet]
        [Route("api/ListOfAvailableStocks")]
        public ActionResult GetStocks()
        {
            var jsonResult = Json(_dal, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }
    }
}