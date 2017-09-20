using Firma1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Firma1.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Heslo heslo = new Heslo();
            return View(heslo);
        }

        [HttpPost]
        public ActionResult Index(Heslo heslo)
        {

            
             if(heslo.ZvalidujHeslo())   
            return RedirectToAction("Index","Home");
            return View(heslo);
        }
    }
}