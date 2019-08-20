using cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cinema.Controllers
{
    public class desktopController : ApiController
    {
        cinemaEntities db = new cinemaEntities();

        [Route("api/GetInfo/{idrezerwacji}")]
        [HttpGet]
        public IHttpActionResult info(int idrezerwacji)
        {
            var info = (from r in db.Rezerwacje
                        join b in db.Bilety on r.Id_Rezerwacji equals b.Id_Rezerwacji join u in db.AspNetUsers on b.Id_Uzytkownika equals  u.Id
                        where  r.Id_Rezerwacji == idrezerwacji
                        select new ApiModel
                        { Idseansu = r.Id_Seansu, IdMiejsca = b.Id_Miejsca, Cena = b.Cena, Rodzaj = b.Rodzaj, Idrezerwacji = r.Id_Rezerwacji,name = u.FirstName,secondname=u.LastName,czyZaplacone=r.Czy_zaplacone }).ToList();
            if(info.Count == 0)
            {
                return NotFound();
            }

            return Ok(info);
        }


        [HttpPost]
        [Route("api/ustawStatus")]
        public IHttpActionResult ustawStatus([FromBody]int idrezerwacji)
        {
            var rezerwacja = db.Rezerwacje.Where(w => w.Id_Rezerwacji == idrezerwacji).FirstOrDefault();
            if(rezerwacja == null)
            {
                return Ok(new Msg { message = "Niema takiej rezerwacji" });
            }
            rezerwacja.Czy_zaplacone = true;
            db.SaveChangesAsync();


            return Ok(new Msg { message = "Zaktualizowano" });
        }
    }
}
