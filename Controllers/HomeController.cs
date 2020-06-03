using Childrens_Toy_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Childrens_Toy_Store.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public async Task<ActionResult> Index()
        {
            return View(await db.Produkty.ToListAsync());
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