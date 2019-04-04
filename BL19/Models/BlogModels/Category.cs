using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string HeaderImage { get; set; }
        public int CategoryNum { get; set; }


        public ICollection<EntryCategory> EntryCategories { get; set; }
    }
}
