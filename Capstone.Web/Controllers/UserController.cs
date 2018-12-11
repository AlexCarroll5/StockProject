using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone
{
    public class UserController : StockGameBaseController
    {
        private IStockGameDAL _dal;

        public UserController(IStockGameDAL dal) : base(dal)
        {
            _dal = dal;
        }
        // GET: User
        public ActionResult Landing()
        {
            return View();
        }
        public ActionResult Register()
        {

        }
        public ActionResult Login()
        {

        }
    }
}