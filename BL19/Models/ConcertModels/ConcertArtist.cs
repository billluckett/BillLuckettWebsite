using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class ConcertArtist
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public int ArtistId { get; set; }
        public int Rank { get; set; }

        public Concert Concert { get; set; }
        public Artist Artist { get; set; }

        public ConcertArtist()
        {
        }

        public ConcertArtist(Concert concert, Artist artist, int rank = 1)
        {
            ConcertId = concert.Id;
            ArtistId = artist.Id;
            Rank = rank;
        }
    }
}
