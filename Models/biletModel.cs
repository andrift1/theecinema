using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class biletModel
    {
        public IEnumerable<string> bilety { get; set; }
        public IEnumerable<string> numerbiletow { get; set; }
        public string idseansu { get; set; }

        public SeansInfoModel film { get; set; }
        public string[] biletythis { get; set; }
        public enum biletrodzaj
        {
            Normalny,
            Ulgowy
        }
    }
}