using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class BlogViewModel
    {
        public List<Entry> Entries { get; set; }
        public string HeaderImage { get; set; }
        public string RightSide { get; set; }
    }
}
