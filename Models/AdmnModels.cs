using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class Przywileje
    {
        [Required]
        [Display(Name ="Email")]
        public string email { get; set; }
    }
}