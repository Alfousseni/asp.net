using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M1GL2023.App_Start;

namespace M1GL2023.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Envoie d'email
            //GMailer.senMail("coulybaly.alf@gmail.com", "test mail M1GL", "Test Reussi");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}