using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL19.Models.ConcertModels;
using Microsoft.EntityFrameworkCore;

namespace BL19.Data
{
    public class ConcertDbContext : DbContext
    {
        public ConcertDbContext(DbContextOptions<ConcertDbContext> options)
            : base(options)
        {
        }

        public DbSet<Concert> Cc_Concerts { get; set; }
        public DbSet<Artist> Cc_Artists { get; set; }
        public DbSet<Person> Cc_People { get; set; }
        public DbSet<Venue> Cc_Venues { get; set; }
        public DbSet<Era> Cc_Eras { get; set; }
        public DbSet<ConcertArtist> Cc_ConcertArtists { get; set; }
        public DbSet<ConcertPerson> Cc_ConcertPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
