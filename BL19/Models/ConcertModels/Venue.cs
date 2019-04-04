using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public ICollection<Concert> Concerts { get; set; }

        public Venue()
        {
        }

        public Venue(string name, string city, string state)
        {
            Name = name;
            City = city;
            State = state;
        }
    }
}
