using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL19.Models;

namespace BL19.Data
{
    public static class BlogDbInit
    {
        public static void Init(BlogDbContext context)
        {
            return;
            context.Database.EnsureCreated();

            if (context.BlogEntries.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category() { Name = "Sports", Color = "darkgreen" },
                new Category() { Name = "Politics", Color = "darkblue" },
                new Category() { Name = "WebDev", Color = "brown" },
                new Category() { Name = "Science", Color = "midnightblue" },
                new Category() { Name = "Parenthood", Color = "darkred" },
            };

            foreach (var c in categories)
            {
                context.BlogCategories.Add(c);
            }
            context.SaveChanges();

            var entries = new Entry[]
            {
                new Entry("Uno", "Here's text No. 1 for my blog initializer. It gets categories 'Sports,' 'Politics,' and 'WebDev.'", DateTime.Now),
                new Entry("Dos", "Here's text No. 2. Init me. This one's in 'Sports.'", DateTime.Now),
                new Entry("Tres", "Here's 3. It's in 'Sports' too.", DateTime.Now),
                new Entry("Cuatro", "Here's No. 4, which is a family entry. So, 'Parenthood.'", DateTime.Now),
                new Entry("Cinco", "NUM-BER-FIVE-IS-A-LIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIVE! No specific categories, however. And I guess 'Science' just stays empty. For now.", DateTime.Now),
            };

            foreach (var e in entries)
            {
                context.BlogEntries.Add(e);
            }
            context.SaveChanges();

            var entryCategoriess = new EntryCategory[]
            {
                CreateEntryCategory(context, "Uno", "Sports"),
                CreateEntryCategory(context, "Uno", "Politics"),
                CreateEntryCategory(context, "Uno", "WebDev"),
                CreateEntryCategory(context, "Dos", "Sports"),
                CreateEntryCategory(context, "Tres", "Sports"),
                CreateEntryCategory(context, "Cuatro", "Parenthood"),
            };

            foreach (var ec in entryCategoriess)
            {
                context.BlogEntryCategories.Add(ec);
            }
            context.SaveChanges();
        }

        private static EntryCategory CreateEntryCategory(BlogDbContext context, string title, string category)
        {
            return new EntryCategory()
            {
                EntryId = context.BlogEntries.Where(e => e.Title.Equals(title)).First().Id,
                CategoryId = context.BlogCategories.Where(c => c.Name.Equals(category)).First().Id
            };
        }
    }
}
