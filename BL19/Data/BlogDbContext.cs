using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL19.Models;
using Microsoft.EntityFrameworkCore;

namespace BL19.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<Entry> BlogEntries { get; set; }
        public DbSet<Category> BlogCategories { get; set; }
        public DbSet<Tag> BlogTags { get; set; }
        public DbSet<EntryCategory> BlogEntryCategories { get; set; }
        public DbSet<EntryTag> BlogEntryTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
