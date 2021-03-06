﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models.ConcertModels
{
    public class Era
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Concert> Concerts { get; set; }

        public Era()
        {
        }

        public Era(string name)
        {
            Name = name;
        }
    }
}
