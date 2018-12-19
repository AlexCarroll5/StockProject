using StockGameService.Models;
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

        [HttpGet]
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
        public ActionResult Settings()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Settings(Settings model)
        {
            if (!ModelState.IsValid)
            {
                return View("Settings");
            }

            return RedirectToAction("Game");
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Results()
        {
            List<UserCash> model = _dal.GetCashAmounts();
            return View("Results", model);
        }
    }
}