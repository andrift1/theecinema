using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class BiletDataModel
    {
        public int Idseansu { get; set; }

        public short? IdMiejsca { get; set; }
        public string Cena { get; set; }
        public string Rodzaj { get; set; }
        public int Idrezerwacji { get; set; }
    }
}