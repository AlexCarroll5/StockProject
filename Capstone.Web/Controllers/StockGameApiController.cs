using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone
{
    public class StockGameApiController : StockGameBaseController
    {
        // GET: StockGameApi
        public ActionResult Index()
        {
            return View();
        }
    }
}