using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nullamsoftware.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string Yritusenimi { get; set; }
        public DateTime Toimumisaeg { get; set; }
        public string Koht { get; set; }
        public string? Lisainfo { get; set; }
        

    }
  

}
