using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ConcertArtist> ConcertArtists { get; set; }

        public Artist()
        {
        }

        public Artist(string name)
        {
            Name = name;
        }
    }
}
