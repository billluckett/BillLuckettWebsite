using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class Concert
    {
        public int Id { get; set; }
        public int EraId { get; set; }
        public DateTime Date { get; set; }
        public int VenueId { get; set; }
        public string City => this.Venue.City;
        public string State => this.Venue.State;
        public string Tour { get; set; }
        public string Notes { get; set; }

        public Era Era { get; set; }
        public Venue Venue { get; set; }
        public ICollection<ConcertArtist> ConcertArtists { get; set; }
        public ICollection<ConcertPerson> ConcertPeople { get; set; }

        public Concert()
        {
        }

        public Concert(Era era, DateTime date, Venue venue, string tour, string notes)
        {
            Era = era;
            Date = date;
            Venue = venue;
            Tour = tour;
            Notes = notes;
        }

        public IEnumerable<Artist> GetArtists()
        {
            if (ConcertArtists == null)
            {
                return new List<Artist>();
            }

            return ConcertArtists.Where(ca => ca.ConcertId == this.Id).OrderBy(o => o.Rank).Select(s => s.Artist);
        }

        public IEnumerable<Person> GetPeople()
        {
            if (ConcertPeople == null)
            {
                return new List<Person>();
            }

            return ConcertPeople.Where(cp => cp.ConcertId == this.Id).OrderBy(o => o.Rank).Select(s => s.Person);
        }
    }
}
