using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone
{
    public class StockGameController : StockGameBaseController
    {
        private IStockGameDAL _dal;

        public StockGameController(IStockGameDAL dal) : base(dal)
        {
            _dal = dal;
        }
        // GET: Home
        public ActionResult Game()
        {
            var Model = Session[CurrentUserSession] as UserItem;

            if(Model != null)
            { 
                return View("Game", Model);
            }
            else
            {
                return View("../User/Login");
            }
        }

        public ActionResult About()
        {
            return View();
        }
    }
}