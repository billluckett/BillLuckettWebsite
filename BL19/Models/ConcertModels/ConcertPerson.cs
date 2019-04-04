using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class ConcertPerson
    {
        public int Id { get; set; }
        public int ConcertId { get; set; }
        public int PersonId { get; set; }
        public int Rank { get; set; }

        public Concert Concert { get; set; }
        public Person Person { get; set; }

        public ConcertPerson()
        {
        }

        public ConcertPerson(Concert concert, Person person, int rank = 1)
        {
            ConcertId = concert.Id;
            PersonId = person.Id;
            Rank = rank;
        }
    }
}
