using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellPhoneService.Controllers
{
    public class NewsCollectingController : Controller
    {
        private NewsCollecting service = new NewsCollecting();
        public ActionResult Index()
        {
            service.Update();

            return View();
        }

    }
}
