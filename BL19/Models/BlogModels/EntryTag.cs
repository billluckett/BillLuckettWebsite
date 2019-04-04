using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class EntryTag
    {
        public int Id { get; set; }
        public int EntryId { get; set; }
        public int TagId { get; set; }

        public Entry Entry { get; set; }
        public Tag Tag { get; set; }

        public EntryTag()
        {
        }

        public EntryTag(Entry entry, Tag tag)
        {
            EntryId = entry.Id;
            TagId = tag.Id;
        }
    }
}
