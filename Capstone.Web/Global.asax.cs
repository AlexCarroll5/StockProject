using Capstone.Web;
using Capstone.Web.DAL;
using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VendingWeb
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            // Bind Database
            //kernel.Bind<IVendingService>().To<MockVendingDBService>();
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string connectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            kernel.Bind<IStockGameDAL>().To<StockGameDAL>().WithConstructorArgument("connectionString", connectionString);



            return kernel;
        }
    }
}