using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    [MetadataType(typeof(FilmyModel))]
    public partial class Filmy
    {
        public IEnumerable<HttpPostedFileBase> files { get; set; }

    }
    public class FilmyModel
    {

        
        public short Id_Filmu { get; set; }
        [Required]
        [Display(Name = "Tytuł")]
        public string Tytul { get; set; }
        [Required]
        public string Gatunek { get; set; }
        [Required]
        [Display(Name = "Czas trawania")]
        public Nullable<byte> Czas_trwania { get; set; }
        [Required]
        [Range(1, 2100)]
        [Display(Name = "Rok Produkcji")]
        public short Rok_produkcji { get; set; }
        public string Kraj_produkcji { get; set; }
        public string Rezyser { get; set; }
        public string Obsada { get; set; }
        [Required]
        [Display(Name = "Opis")]
        public string Opis { get; set; }
        
        public string Rodzaj { get; set; }
        public string Ograniczenia { get; set; }
        
        //[Display(Name = "Zdjecie")]
        //public string Plakat { get; set; }
        [Required]
        [Range(0, 999.99)]
        [Display(Name = "Cena Ulgowy")]
        public byte Cena_ulgowy { get; set; }

        [Required]
        [Range(0, 999.99)]
        [Display(Name = "Cena Normalny")]
        public byte Cena_normalny { get; set; }
    }
}