using FIT_Api_Examples.Modul0_Autentifikacija.Models;
using FIT_Api_Examples.Modul2.Models;
using FIT_Api_Examples.Modul3_MaticnaKnjiga.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace FIT_Api_Examples.Modul3_MaticnaKnjiga.ViewModels
{
    public class OvjeraGodinaVM
    {
        public int id { get; set; }
    
        public DateTime? datumOvjere { get; set; }
        public string? napomena { get; set; }

      
    }
}
