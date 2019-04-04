using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public string Link { get; set; }
        public string Github { get; set; }
        public string ImageFull { get; set; }
        public string ImageMobile { get; set; }
        public string Description { get; set; }
    }
}
