using System;

namespace TheCorcoranGroup.Models
{
    public class President
    {
        public String Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Birthplace { get; set; }
        public DateTime? DeathDay { get; set; }
        public string DeathPlace { get; set; }
    }
}
