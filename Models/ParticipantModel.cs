using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nullamsoftware.Models

{   // Participant datamodel
    public class ParticipantModel
    {  
        //Primary key
        public int Id {get;set;}
        // Person type
        public int Isikut {get;set;}
        // First name
        public string Eesnimi {get;set;}
        // Last name
        public string Perenimi {get;set;}
        // Social security number
        public string Isikukood {get;set;}
        // Payment method
        public int Maksmisviis {get;set;}
        // Additional information
        public string? Lisainfoisik {get;set;}
        // Foreign key of the participant table that matches with the Events primary key
        public int Fk_Participant { get; set; }
    }
}
