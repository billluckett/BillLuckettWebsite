using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => (this.FirstName.Trim() + " " + this.LastName.Trim()).Trim();

        public ICollection<ConcertPerson> ConcertPeople { get; set; }

        public Person()
        {
        }

        public Person(string fname, string lname)
        {
            FirstName = fname;
            LastName = lname;
        }

        public Person(string name)
        {
            FirstName = name;
            LastName = "";
        }
    }
}
