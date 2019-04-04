using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Html)]
        [StringLength(10000)]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public ICollection<EntryCategory> EntryCategories { get; set; }
        public ICollection<EntryTag> EntryTags { get; set; }

        public Entry()
        {
        }

        public Entry(string title, string content, DateTime createdOn)
        {
            Title = title;
            Content = content;
            CreatedOn = createdOn;
            ModifiedOn = CreatedOn;
        }
    }
}
