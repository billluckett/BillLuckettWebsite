using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL19.Models.ConcertModels;

namespace BL19.Data
{
    public static class ConcertDbInit
    {
        public static void Init(ConcertDbContext ctx)
        {
            return;
            //ctx.Database.EnsureCreated();

            if (!ctx.Cc_Venues.Any())
            {
                var venues = new Venue[]
                {
                    new Venue("Casper Events Center", "Casper", "Wyoming"),
                    new Venue("Utah State Fairpark Coliseum", "Salt Lake City", "Utah"),
                    new Venue("Worcester Centrum", "Worcester", "Massachusetts"),
                    new Venue("WPI (Pub? Gompei's Place?)", "Worcester", "Massachusetts"),
                    new Venue("Worcester Artist Group", "Worcester", "Massachusetts"),
                    new Venue("Heart of the Commonwealth Theater", "Worcester", "Massachusetts"),
                    new Venue("Foxboro Stadium", "Foxboro", "Massachusetts"),
                    new Venue("Great Woods Center for the Performing Arts", "Mansfield", "Massachusetts"),
                    new Venue("Hatch Shell", "Boston", "Massachusetts"),
                    new Venue("Mile High Stadium", "Denver", "Colorado"),
                    new Venue("Fiddler's Green", "Denver", "Colorado"),
                    new Venue("A&S Auditorium", "Laramie", "Wyoming"),
                    new Venue("Wyoming Union", "Laramie", "Wyoming"),
                    new Venue("Autzen Stadium", "Eugene", "Oregon"),
                    new Venue("Montana St. U. Field House", "Bozeman", "Montana"),
                    new Venue("Denver Coliseum", "Denver", "Colorado"),
                    new Venue("Red Rocks", "Denver", "Colorado"),
                    new Venue("McNichols Arena", "Denver", "Colorado"),
                    new Venue("UW Fine Arts Concert Hall", "Laramie", "Wyoming"),
                    new Venue("Folsom Field", "Boulder", "Colorado"),
                    new Venue("Pepsi Center", "Denver", "Colorado"),
                    new Venue("Frontier Park", "Cheyenne", "Wyoming"),
                    new Venue("Fillmore", "Denver", "Colorado"),
                    new Venue("Moda Center", "Portland", "Oregon"),
                };

                foreach (var v in venues)
                {
                    ctx.Cc_Venues.Add(v);
                }
                ctx.SaveChanges();
            }

            if (!ctx.Cc_People.Any())
            {
                var people = new Person[]
                {
                    new Person("Bert", "Weidt"),
                    new Person("Jason", "Marsden"),
                    new Person("Marcus", "Bracke"),
                    new Person("some Zeta Psis"),
                    new Person("Rob", "Weitz"),
                    new Person("other I2s"),
                    new Person("Erin", "Fowler"),
                    new Person("some other TKEs"),
                    new Person("Vince", "Schutte"),
                    new Person("Kim the waitress"),
                    new Person("Tim the cook"),
                    new Person("Sarah", "Frazer"),
                    new Person("Brianna", "Johnson"),
                    new Person("Krista", "Patten"),
                    new Person("Jake", "Bendrick"),
                    new Person("Scott", "Brown"),
                    new Person("Jason", "Horsley"),
                    new Person("Dan", "Leonard"),
                    new Person("Vanessa", "Hastings"),
                    new Person("Melissa", "Davidson"),
                    new Person("Ryan", "Johnson"),
                    new Person("lots from the CST"),
                    new Person("Jeff", "Tollefson"),
                    new Person("Zach", "Snyder"),
                    new Person("others"),
                    new Person("Kathy", "McManus"),
                    new Person("Bernie", "McManus"),
                    new Person("Kelly", "Milner"),
                    new Person("Tom", "Morton"),
                    new Person("Brian", "Fowler"),
                    new Person("Jim", "Diehl"),
                    new Person("Junior", "Diehl"),
                    new Person("Junior Diehl's girlfriend"),
                    new Person("Ron", "Roden"),
                    new Person("Kari", "Keating"),
                    new Person("Ryan", "Roden?"),
                    new Person("Laura", "Gervais"),
                    new Person("Jen", "Cousins"),
                    new Person("Phil", "Ellsworth"),
                    new Person("Chelsea", "Robirds"),
                    new Person("Jenni", "Luckett"),
                };

                foreach (var p in people)
                {
                    ctx.Cc_People.Add(p);
                }
                ctx.SaveChanges();
            }

            if (!ctx.Cc_Artists.Any())
            {
                var artists = new Artist[]
                {
                    new Artist("Bon Jovi"),
                    new Artist("Skid Row"),
                    new Artist("They Might Be Giants"),
                    new Artist("Van Halen"),
                    new Artist("Alice in Chains"),
                    new Artist("Mighty Mighty Bosstones"),
                    new Artist("Antfarm"),
                    new Artist("Steve Miller Band"),
                    new Artist("Kinks"),
                    new Artist("Midnight Oil"),
                    new Artist("Violent Femmes"),
                    new Artist("Indigo Girls"),
                    new Artist("Metallica"),
                    new Artist("B-52s"),
                    new Artist("U2"),
                    new Artist("Primus"),
                    new Artist("Dinosaur Jr."),
                    new Artist("Arrested Development"),
                    new Artist("Fishbone"),
                    new Artist("Big Head Todd and the Monsters"),
                    new Artist("Samples"),
                    new Artist("Grateful Dead"),
                    new Artist("Cracker"),
                    new Artist("Phish"),
                    new Artist("Beastie Boys"),
                    new Artist("John Spencer Blues Explosion"),
                    new Artist("R.E.M."),
                    new Artist("Sonic Youth"),
                    new Artist("Pearl Jam"),
                    new Artist("Sarah McLaughlin"),
                    new Artist("Sheryl Crow"),
                    new Artist("Blues Traveler"),
                    new Artist("Moody Blues"),
                    new Artist("Def Leppard"),
                    new Artist("Joan Jett"),
                    new Artist("Bob Dylan"),
                    new Artist("Dave Matthews Band"),
                    new Artist("Styx"),
                    new Artist("REO Speedwagon"),
                    new Artist("Tom Petty"),
                    new Artist("Staind"),
                    new Artist("3 Doors Down"),
                    new Artist("Wilco"),
                    new Artist("Ed Harcourt"),
                    new Artist("White Stripes"),
                    new Artist("Pixies"),
                    new Artist("Counting Crows"),
                    new Artist("Goo Goo Dolls"),
                    new Artist("Steve Winwood"),
                };

                foreach (var a in artists)
                {
                    ctx.Cc_Artists.Add(a);
                }
                ctx.SaveChanges();
            }

            if (!ctx.Cc_Eras.Any())
            {
                var eras = new Era[]
                {
                    new Era("High School"),
                    new Era("College WPI"),
                    new Era("College Sheridan"),
                    new Era("College UW"),
                    new Era("Casper"),
                    new Era("Cheyenne"),
                    new Era("Forest Grove"),
                };

                foreach (var e in eras)
                {
                    ctx.Cc_Eras.Add(e);
                }
                ctx.SaveChanges();
            }

            if (!ctx.Cc_Concerts.Any())
            {
                var concerts = new Concert[]
                {
                    MakeConcert(ctx, "High School", "9/1/1989", "Casper Events Center", "New Jersey Tour", ""),
                    MakeConcert(ctx, "High School", "8/10/1990", "Utah State Fairpark Coliseum", "", ""),
                    MakeConcert(ctx, "College UW", "6/18/1994", "Autzen Stadium", "", ""),
                    MakeConcert(ctx, "College UW", "6/19/1994", "Autzen Stadium", "", ""),
                    MakeConcert(ctx, "Forest Grove", "8/12/2014", "Moda Center", "", ""),
                };

                foreach (var c in concerts)
                {
                    ctx.Cc_Concerts.Add(c);
                }
                ctx.SaveChanges();

                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[0], ctx.Cc_Artists.First(a => a.Name == "Bon Jovi")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[0], ctx.Cc_Artists.First(a => a.Name == "Skid Row")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[1], ctx.Cc_Artists.First(a => a.Name == "They Might Be Giants")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[2], ctx.Cc_Artists.First(a => a.Name == "Grateful Dead")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[2], ctx.Cc_Artists.First(a => a.Name == "Cracker")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[3], ctx.Cc_Artists.First(a => a.Name == "Grateful Dead")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[3], ctx.Cc_Artists.First(a => a.Name == "Cracker")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[4], ctx.Cc_Artists.First(a => a.Name == "Tom Petty")));
                ctx.Cc_ConcertArtists.Add(new ConcertArtist(concerts[4], ctx.Cc_Artists.First(a => a.Name == "Steve Winwood")));

                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[0], ctx.Cc_People.First(p => p.FirstName == "Bert")));
                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[1], ctx.Cc_People.First(p => p.FirstName == "Jason")));
                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[2], ctx.Cc_People.First(p => p.FirstName == "Kim the waitress")));
                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[2], ctx.Cc_People.First(p => p.FirstName == "Tim the cook")));
                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[3], ctx.Cc_People.First(p => p.FirstName == "Kim the waitress")));
                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[3], ctx.Cc_People.First(p => p.FirstName == "Tim the cook")));
                ctx.Cc_ConcertPeople.Add(new ConcertPerson(concerts[4], ctx.Cc_People.First(p => p.FirstName == "Jenni")));

                ctx.SaveChanges();
            }
        }

        private static Concert MakeConcert(ConcertDbContext ctx, string era, string date, string venue, string tour, string notes)
        {
            return new Concert(
                ctx.Cc_Eras.First(e => e.Name == era),
                DateTime.Parse(date),
                ctx.Cc_Venues.First(v => v.Name == venue),
                tour,
                notes);
        }
    }
}
