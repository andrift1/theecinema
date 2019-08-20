using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cinema.Models
{
    [MetadataType(typeof(SeanseModel))]
    public partial class Seanse
    {
        [Required]
        [DataType(DataType.Time)]
        [Display(Name ="godzina")]
        [RegularExpression(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Nie poprawna godzina.")]

        public string godzina { get; set; }

        public IEnumerable<SelectListItem> filmylist { get; set; }

    }

    public class SeanseModel
    {
        public short Id_Seansu { get; set; }
        [Display(Name ="Film")]
        
        public short Id_Filmu { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data")]
        [Range(typeof(DateTime), "1-Jan-1940", "1-Jan-2099")]
        public System.DateTime Data_i_godzina { get; set; }

    }
}