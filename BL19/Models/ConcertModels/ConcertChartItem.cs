using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class ConcertChartItem
    {
        public string Era { get; set; }
        public DateTime Date { get; set; }
        public Venue Venue { get; set; }
        public string State { get; set; }
        public ICollection<Artist> Artists { get; set; }
        public ICollection<PersonChartItem> People { get; set; }

        public ConcertChartItem()
        {
        }

        public ConcertChartItem(string era, DateTime date, Venue venue, string state, List<Artist> artists, List<PersonChartItem> people)
        {
            Era = era;
            Date = date;
            Venue = venue;
            State = state;
            Artists = artists;
            People = people;
        }
    }
}
