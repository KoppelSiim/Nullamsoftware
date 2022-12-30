using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nullamsoftware.Models
{
    public class ParticipantModel
    {
        public int Id {get;set;}
        public int Isikut {get;set;}
        public string Eesnimi {get;set;}
        public string Perenimi {get;set;}
        public string Isikukood {get;set;}
        public int Maksmisviis {get;set;}
        public string? Lisainfoisik {get;set;}

    }
}
