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
        // GET: StockGameApi
        //public ActionResult Index()
        //{
        //    return Json();
        //}
    }
}