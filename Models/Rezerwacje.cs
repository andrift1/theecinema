//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cinema.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rezerwacje
    {
        public short Id_Rezerwacji { get; set; }
        public short Id_Seansu { get; set; }
        public bool Czy_zaplacone { get; set; }
    }
}
