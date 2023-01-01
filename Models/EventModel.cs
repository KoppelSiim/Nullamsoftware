


namespace Nullamsoftware.Models
{   // Event data model
    public class EventModel

    {   // Primary key of the event
        public int Id {get; set;}
        // Event name
        public string Yritusenimi {get; set;}
        // Event time
        public DateTime Toimumisaeg {get; set;}
        // Place of the event
        public string Koht {get; set;}
        // Additional information
        public string? Lisainfo {get; set;}
        
    }
  
}
