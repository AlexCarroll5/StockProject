using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone
{
    public class UserController : StockGameBaseController
    {
        // GET: User
        public ActionResult Landing()
        {
            return View();
        }
    }
}