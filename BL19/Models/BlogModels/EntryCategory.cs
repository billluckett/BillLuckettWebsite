using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class EntryCategory
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public int CategoryId { get; set; }

        public Entry Entry { get; set; }
        public Category Category { get; set; }

        public EntryCategory()
        {
        }

        public EntryCategory(Entry entry, Category category)
        {
            EntryId = entry.Id;
            CategoryId = category.Id;
        }

        public EntryCategory(int entryId, int categoryId)
        {
            EntryId = entryId;
            CategoryId = categoryId;
        }
    }
}
