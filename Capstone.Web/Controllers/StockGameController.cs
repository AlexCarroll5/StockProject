﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone
{
    public class StockGameController : StockGameBaseController
    {

        // GET: Home
        public ActionResult Game()
        {
            return View();
        }
    }
}