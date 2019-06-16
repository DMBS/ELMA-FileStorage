using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTaskApplication.Controllers
{
    public class HomeController : Controller
    {
        //Redirect to DocumentController
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Document");
            }
            else
            {
                return View();
            }
                       
        }

    }
}
