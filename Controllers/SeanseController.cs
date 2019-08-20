using cinema.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cinema.linq;
using System.Drawing;
using System.IO;
using System.Data.Entity.Core.Objects;

namespace cinema.Controllers
{
    public class SeanseController : Controller
    {
        cinemaEntities db = new cinemaEntities();
        
        // GET: Seanse
        public ActionResult Index(int id)
        {

            var model = db.getSeanse(id);



            return View(model);
        }


        [Authorize]
        public ActionResult Miejsca(int id)
        {
            var miejsca = db.Miejsca.Where(m => m.Id_Seansu == id);

            //var info = db.getSeanse(id).Take(1);
            var iloscmiejsc = miejsca.Where(a => a.Status == false).Count();



            var info = (from s in db.Seanse
                        join f in db.Filmy on s.Id_Filmu equals f.Id_Filmu
                        where s.Id_Seansu == id
                        select new SeansInfoModel { Tytul = f.Tytul, Plakat = f.Plakat }).ToList();


            var model = new MiejscaViewModel();
            model.miejsca = miejsca;
            model.film = info[0];
            model.wolnemiejsca = iloscmiejsc;
            model.idseansu = id;

            return View(model);
        }
        [Authorize]

        public ActionResult finalizujBilet()
        {
            string miejsca = Request.QueryString["miejsca"];
            string idseansu = Request.QueryString["idseansu"];


            int idseansu123 = Convert.ToInt16(idseansu);
            var info = (from s in db.Seanse
                        join f in db.Filmy on s.Id_Filmu equals f.Id_Filmu
                        where s.Id_Seansu == idseansu123
                        select new SeansInfoModel { Tytul = f.Tytul, Plakat = f.Plakat, cenanormalny = f.Cena_normalny, cenaulgowy = f.Cena_ulgowy }).ToList();



            string[] miejsceID = miejsca.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            TempData["miejsca"] = miejsceID;


            var lista = new List<string>();
            foreach (var item in miejsceID)
            {
                lista.Add(item);
            }


            var model = new biletModel();
            model.bilety = lista;
            model.idseansu = idseansu;
            model.numerbiletow = lista;
            model.biletythis = miejsceID;
            model.film = info[0];
            Session["Film"] = info[0].Tytul;
            Session["Plakat"] = info[0].Plakat;
            Session["ulgowycena"] = info[0].cenaulgowy;
            Session["normalnycena"] = info[0].cenanormalny;


            return View(model);
        }

        [Authorize]

        [HttpPost]
        public ActionResult finalizujBilet(biletModel biletmodel)
        {
            var tempmiejsce = TempData["miejsca"] as string[];
            foreach (var jednomiejsce in tempmiejsce)
            {
                int n;
                bool isNumeric = int.TryParse(jednomiejsce, out n);
                if (!isNumeric)
                    return RedirectToAction("index", "Home", new { msg = "Cos poszlo nie tak 1001" });
            }





            var cos = biletmodel.bilety;
            var seans = Convert.ToInt16(biletmodel.idseansu);
            var info = (from s in db.Seanse
                        join f in db.Filmy on s.Id_Filmu equals f.Id_Filmu
                        where s.Id_Seansu == seans
                        select new CenyModel { nazwa = f.Tytul, plakat = f.Plakat, normalny = f.Cena_normalny, ulgowy = f.Cena_ulgowy }).ToList();
            var infooo = info[0];


            var rezerwacje = new Rezerwacje();
            rezerwacje.Id_Seansu = seans;
            rezerwacje.Czy_zaplacone = false;
            db.Rezerwacje.Add(rezerwacje);
            db.SaveChanges();

            int numberrezerwacji = rezerwacje.Id_Rezerwacji;


            // bilety

            //string[] arraymiejsca = biletmodel.numerbiletow.ToArray();
            //string[] arraymiejsca



            //string[] arraymiejsce;
            //int sd = 0;
            //foreach (var item in biletmodel.numerbiletow)
            //{
            //    arraymiejsce[sd] = item;
            //    sd++;
            //}



            int cena = 0;
            int asd = 0;
            foreach (var item in biletmodel.bilety)
            {
                Bilety bilety = new Bilety();
                if (item == "Ulgowy")
                {
                    bilety.Rodzaj = item;
                    bilety.Cena = infooo.ulgowy.ToString();
                    cena += infooo.ulgowy;
                }
                else
                {
                    bilety.Rodzaj = item;
                    bilety.Cena = infooo.normalny.ToString();
                    cena += infooo.normalny;
                }

                bilety.Id_Miejsca = Convert.ToInt16(tempmiejsce[asd]);
                bilety.Id_Uzytkownika = User.Identity.GetUserId();
                bilety.Id_Rezerwacji = Convert.ToInt16(numberrezerwacji);
                asd++;

                db.Bilety.Add(bilety);
                db.SaveChanges();


                var idseansu = Convert.ToInt16(biletmodel.idseansu);



                foreach (var jednomiejsce in tempmiejsce)
                {
                    int miejsce = Convert.ToInt16(jednomiejsce);
                    var queryone = db.Miejsca.FirstOrDefault(da => da.Id_Seansu == idseansu && da.Nr_Miejsca == miejsce);

                    if (queryone != null)
                    {
                        queryone.Status = true;
                    }
                    
                }

                db.SaveChanges();


            }





            //return View(biletmodel);
            return RedirectToAction("drukujBilet", "Seanse", new { idrezerwacji = numberrezerwacji });
        }

        [Authorize]
        public ActionResult drukujBilet(int idrezerwacji)
        {
            var userid = User.Identity.GetUserId();
            var userInfo = db.AspNetUsers.Where(a => a.Id == userid).FirstOrDefault();


             var info = (from r in db.Rezerwacje
                       join b in db.Bilety on r.Id_Rezerwacji equals b.Id_Rezerwacji
                      where b.Id_Uzytkownika == userid && r.Id_Rezerwacji == idrezerwacji
                         select new BiletDataModel { Idseansu = r.Id_Seansu, IdMiejsca=b.Id_Miejsca,Cena = b.Cena,Rodzaj= b.Rodzaj,Idrezerwacji = r.Id_Rezerwacji}).ToList();
            

            var seansid = info[0].Idseansu;


            var Filminfo = (from s in db.Seanse
                            join f in db.Filmy on s.Id_Filmu equals f.Id_Filmu
                            where s.Id_Seansu == seansid
                            select new FilmInfoModel { nazwafilmu = f.Tytul }).ToList() ;


            string firstText = userInfo.FirstName + " " + userInfo.LastName;
            //string secondText = "World";
            string idrezerwacjiText = "NR: " + info[0].Idrezerwacji.ToString();

            PointF firstLocation = new PointF(500f, 950f);
            PointF secondLocation = new PointF(885F, 762f);
            PointF idrezerwacjiLocation = new PointF(460F, 431F);
            PointF biletLocation = new PointF(1258F, 762f);
            PointF CenaLocation = new PointF(501F, 904f);
            PointF FilminfoLocation = new PointF(704F, 711F);

            // string imageFilePath = Application.StartupPath + @"img\\biletdesign.png";
            //string saveToPath = @"~\img\bilety";
            string guid = Guid.NewGuid().ToString();

            string url = guid + ".png";
            ViewBag.urlBiletu = url;
            string savePath = Path.Combine(Server.MapPath("~/img/bilety/"), url);


            string imageFilePath = Path.Combine(Server.MapPath("~/img/"), "biletdesign.png");
            Bitmap newBitmap;
            using (var bitmap = (Bitmap)Image.FromFile(imageFilePath))//load the image file
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 42))
                    {
                        graphics.DrawString(firstText, arialFont, Brushes.Black, firstLocation);
                        graphics.DrawString(idrezerwacjiText, arialFont, Brushes.Black, idrezerwacjiLocation);

                        var liczba = 762f;
                        foreach (var item in info)
                        {
                            //secondLocation = new PointF(885F, 762f);
                            var jednoMiejsce = "Nr Miejsca: " + item.IdMiejsca.ToString();
                            graphics.DrawString(jednoMiejsce, arialFont, Brushes.Black, secondLocation);
                            liczba = liczba + 55;
                            secondLocation = new PointF(885F, liczba);
                        }

                        var liczba2 = 762f;
                        int cena = 0;
                        string biletText;
                        foreach (var item in info)
                        {
                            cena += Convert.ToInt16(item.Cena);
                            biletText = item.Rodzaj + " " + item.Cena + "zł";
                            graphics.DrawString(biletText, arialFont, Brushes.Black, biletLocation);
                            liczba2 = liczba2 + 55;
                            biletLocation = new PointF(1258F, liczba2);
                        }

                        string fullcena = "Do Zapłaty: " + cena.ToString() +"zł";
                        graphics.DrawString(fullcena, arialFont, Brushes.Red, CenaLocation);

                        string nameFilm = Filminfo[0].nazwafilmu;
                        graphics.DrawString(nameFilm, arialFont, Brushes.Blue, FilminfoLocation);


                        //graphics.DrawString(secondText, arialFont, Brushes.Black, secondLocation);
                    }
                }
                newBitmap = new Bitmap(bitmap);
            }

            newBitmap.Save(savePath);//save the image file
            newBitmap.Dispose();


            return View();

        }
    }
}