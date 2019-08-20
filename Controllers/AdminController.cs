using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cinema.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using cinema.Models;
using System.IO;

namespace cinema.Controllers
{
  //  [Authorize(Roles = "admin")]

    public class AdminController : Controller
    {
        cinemaEntities db = new cinemaEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(Filmy filmy,HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError(string.Empty, "Nie Przesłałes zdjecia");
            }


                if (!ModelState.IsValid)
            {
                return View(filmy);
            }
            
            string _FileName = Path.GetFileName(file.FileName);
            string nazwa = Guid.NewGuid().ToString() + _FileName;
            var path = Path.Combine(Server.MapPath("~/img/filmy"), nazwa);
            file.SaveAs(path);
            
            filmy.Plakat = nazwa;
            db.Filmy.Add(filmy);
            db.SaveChangesAsync();


            ViewData["Message"] = "Dodany";

            return RedirectToAction("Index","Admin",new { msg="Dodales Film."});
        }


        public ActionResult Przywileje()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Przywileje(Przywileje przywileje)
        {
            if (!ModelState.IsValid)
            {
                return View(przywileje);
            }

            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ApplicationUser currentUser = context.Users.FirstOrDefault(x => x.Email == przywileje.email);
            if (currentUser == null)
            {
                ModelState.AddModelError(string.Empty, "Niema takiego użytkownika");
                return View();
            }


            var result1 = UserManager.AddToRole(currentUser.Id, "admin");
            ViewData["Message"] = "Success";


            //context.Roles.Add(new IdentityRole()
            //{
            //    Name = "admin",
            //    NormalizedName = "admin"
            //});
            //context.SaveChanges();

            return RedirectToAction("Przywileje", "Admin", new { msg = "Nadales prawa Administratora" });
        }

        public ActionResult Usunfilmy()
        {
            return View();
        }

        public ActionResult Dodajseans()
        {

            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "Action", Value = "0" });

            //items.Add(new SelectListItem { Text = "Drama", Value = "1" });

            //items.Add(new SelectListItem { Text = "Comedy", Value = "2", Selected = true });

            //items.Add(new SelectListItem { Text = "Science Fiction", Value = "3" });

            var filmy = db.Filmy.ToList().Select(u => new SelectListItem
            {
                Text = u.Tytul,
                Value = u.Id_Filmu.ToString()
            });

            var model = new Seanse();
            model.filmylist = filmy;
           


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Dodajseans(Seanse seanse)
        {

            

            if (!ModelState.IsValid)
            {
                var filmy = db.Filmy.ToList().Select(u => new SelectListItem
                {
                    Text = u.Tytul,
                    Value = u.Id_Filmu.ToString()
                });

                seanse.filmylist = filmy;


                return View(seanse);
            }

            DateTime datefinish = seanse.Data_i_godzina + TimeSpan.Parse(seanse.godzina);
            Console.WriteLine(datefinish);
            seanse.Data_i_godzina = datefinish;

            //var dbtable = new Seanse();
            //dbtable.Id_Filmu = seanse.Id_Filmu;
            //dbtable.Data_i_godzina = datefinish;


            db.Seanse.Add(seanse);
            await db.SaveChangesAsync();

            var idseansu = seanse.Id_Seansu;

            for (int i = 1; i <= 144; i++)
            {
                var miejsca = new Miejsca();
                miejsca.Id_Seansu = idseansu;
                miejsca.Nr_Miejsca = Convert.ToByte(i);
                miejsca.Status = false;
                db.Miejsca.Add(miejsca);

            }
            


            await db.SaveChangesAsync();

            return RedirectToAction("Dodajseans", "Admin",new { msg="Dodałeś nowy seans"});
        }
    }
}