using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CellPhoneService.Controllers
{
    [OauthAuthorize]
    public class PriceCollectingController : Controller
    {
        //
        // GET: /PriceCollecting/

        public ActionResult Index()
        {
            PriceCollecting pc = new PriceCollecting();
            pc.Update();

            return View();
        }

    }
}
