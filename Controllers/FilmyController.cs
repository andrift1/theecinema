using cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cinema.Controllers
{
    public class FilmyController : Controller
    {
        cinemaEntities db = new cinemaEntities();
        // GET: Filmy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Film(int id)
        {
            
            var model = db.Filmy.Where(a => a.Id_Filmu == id).Take(1).FirstOrDefault();
            
            return View(model);
        }

        public ActionResult Filmy()
        {

            var model = db.Filmy.OrderByDescending(a => a.Id_Filmu).ToList();

            return View(model);
        }
    }
}