using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class EntryViewModel
    {
        public Entry Entry { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<int> ActiveCategoryIds { get; set; }
        public ICollection<int> ActiveTagIds { get; set; }

        public EntryViewModel()
        {
        }

        public EntryViewModel(Entry entry, ICollection<Category> categories)
        {
            Entry = entry;
            Categories = categories;
        }
    }
}
