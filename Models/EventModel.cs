using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace Nullamsoftware.Models
{
    public class EventModel
    {
        //public int Id { get; set; }
        public string? Yritusenimi { get; set; }
        public DateTime Toimumisaeg { get; set; }
        public string? Koht { get; set; }
        public string? Lisainfo { get; set; }
    }
}
