using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone
{
    public class StockGameBaseController : Controller
    {
        private IStockGameDAL _dal;

        public StockGameBaseController(IStockGameDAL dal)
        {
            _dal = dal;
        }

        // GET: StockGameBase
        public ActionResult Index()
        {
            return View();
        }
    }
}