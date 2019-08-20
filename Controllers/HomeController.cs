using cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace cinema.Controllers
{
    public class HomeController : Controller
    {
        cinemaEntities db = new cinemaEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult najnowzeFilmy()
        {
            

            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 30)]
        public async Task<ActionResult> najnowszefilmy()
        {
            ViewBag.nazwa = "Najnowsze Filmy";

            var model = db.Filmy.OrderByDescending(a => a.Id_Filmu).Take(6);
            var modelready = new NajnowszeFimyView();
            modelready.Films = model;

            return PartialView(modelready);
        }


        [ChildActionOnly]
        [OutputCache(Duration = 1)]
        public async Task<ActionResult> losowefilmy()
        {
            ViewBag.nazwa = "Losowe Filmy";

            var model = db.Filmy.OrderBy(g => Guid.NewGuid()).Take(6);
            var modelready = new NajnowszeFimyView();
            modelready.Films = model;

            return PartialView("najnowszefilmy",modelready);
        }

    }
}