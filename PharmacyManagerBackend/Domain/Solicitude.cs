using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public enum State
    {
        ACCEPTED = 1,
        PENDING = 0,
        REJECTED = 2,
    }
    public class Solicitude
    {
        public int Id { get; set; }
        public State State { get; set; }
        public DateTime Date { get; set; }
        public List<SolicitudeItem> Items { get; set; }
        public User Employee { get; set; }
        
    }
}
