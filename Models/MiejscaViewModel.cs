using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cinema.Models
{
    public class MiejscaViewModel
    {

        public IEnumerable<Miejsca> miejsca { get; set; }
        public SeansInfoModel film { get; set; }
        public int wolnemiejsca { get; set; }
        public int idseansu { get; set; }
  
    }
}