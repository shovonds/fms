using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dsm.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AppMenu()
        {
            return PartialView("_UserMenu");
        }

        public ActionResult Directory()
        {
            return PartialView("_Directory");
        }


    }
}