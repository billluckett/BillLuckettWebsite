using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL19.Data;
using BL19.Models;
using BL19.Models.ConcertModels;
using BL19.SessionManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BL19.Controllers
{
    public class ConcertsController : Controller
    {
        private readonly ConcertDbContext _ctx;
        private readonly IHostingEnvironment _env;

        public ConcertsController(ConcertDbContext context, IHostingEnvironment env)
        {
            _ctx = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            await _ctx.Cc_Eras.ToListAsync();
            await _ctx.Cc_Venues.ToListAsync();
            await _ctx.Cc_Artists.ToListAsync();
            await _ctx.Cc_People.ToListAsync();
            await _ctx.Cc_ConcertArtists.ToListAsync();
            await _ctx.Cc_ConcertPeople.ToListAsync();

            var concerts = await _ctx.Cc_Concerts.ToListAsync();

            return View("Concerts", concerts);
        }

        public IActionResult Charts()
        {
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            var vm = new Dictionary<string, List<object>>
            {
                { "ConcertPeople", await _ctx.Cc_ConcertPeople.ToListAsync<object>() },
                { "ConcertArtists", await _ctx.Cc_ConcertArtists.ToListAsync<object>() },
                { "Concerts", await _ctx.Cc_Concerts.ToListAsync<object>() },
                { "Eras", await _ctx.Cc_Eras.ToListAsync<object>() },
                { "Venues", await _ctx.Cc_Venues.ToListAsync<object>() },
                { "Artists", await _ctx.Cc_Artists.ToListAsync<object>() },
                { "People", await _ctx.Cc_People.ToListAsync<object>() }
            };

            ViewData["Title"] = "Admin";
            return View("Admin", vm);
        }

        // Returns relevant Concert data for charts
        [HttpPost]
        public async Task<JsonResult> GetConcertData()
        {
            await _ctx.Cc_Eras.ToListAsync();
            await _ctx.Cc_Venues.ToListAsync();
            await _ctx.Cc_Artists.ToListAsync();
            await _ctx.Cc_People.ToListAsync();
            await _ctx.Cc_ConcertArtists.ToListAsync();

            var concertPeople = await _ctx.Cc_ConcertPeople.ToListAsync();
            var concerts = await _ctx.Cc_Concerts.ToListAsync();

            var concertItems = new List<ConcertChartItem>();

            foreach (var c in concerts)
            {
                var item = new ConcertChartItem(
                    c.Era.Name,
                    c.Date,
                    new Venue() { Name = c.Venue.Name },
                    c.Venue.State,
                    new List<Artist>(),
                    new List<PersonChartItem>()
                );

                foreach (var a in c.ConcertArtists)
                {
                    var artist = new Artist(a.Artist.Name);
                    item.Artists.Add(artist);
                }

                foreach (var p in concertPeople)
                {
                    if (p.ConcertId == c.Id)
                    {
                        var person = new PersonChartItem() { Name = p.Person.Name };
                        item.People.Add(person);
                    }
                }

                // THIS doesn't work for reasons involving c.ConcertPeople
                //foreach (var p in c.ConcertPeople)
                //{
                //    //var person = new Person(p.Person.FirstName, p.Person.LastName);
                //    //item.People.Add(person);
                //}

                concertItems.Add(item);
            }

            return Json(concertItems);
        }


        #region temp

        //public IActionResult Test()
        //{
        //    LoadLinesFromFile("AppData", "ConcertsIHaveSeen.txt");
        //    //MakeConcertsFromLines(Lines);
        //    return RedirectToAction("Index");
        //}

        private void MakeConcertsFromLines(Dictionary<int, string[]> lines)
        {
            // line = Era, Date, Notes, Band ... Band5, Arena, City, St, Tour, With ... With5
            for (var i = 0; i < Lines.Count; i++)
            {
                var line = Lines[i];

                // make concert
                var concert = MakeConcert(line[0], line[1], line[8], line[11], line[2]);
                _ctx.Cc_Concerts.Add(concert);
                _ctx.SaveChanges();

                // make concertartists
                for (var j = 3; j < 8; j++)
                {
                    if (!string.IsNullOrEmpty(line[j]))
                    {
                        var artist = _ctx.Cc_Artists.First(a => a.Name == line[j]);
                        _ctx.Cc_ConcertArtists.Add(new ConcertArtist(concert, artist, j - 2));
                    }
                }

                // make concertpeople
                for (var j = 12; j < 17; j++)
                {
                    if (!string.IsNullOrEmpty(line[j]))
                    {
                        var person = _ctx.Cc_People.First(a => a.Name == line[j]);
                        _ctx.Cc_ConcertPeople.Add(new ConcertPerson(concert, person, j - 11));
                    }
                }

                _ctx.SaveChanges();
            }
        }

        private Concert MakeConcert(string era, string date, string venue, string tour, string notes)
        {
            return new Concert(
                _ctx.Cc_Eras.First(e => e.Name == era),
                DateTime.Parse(date),
                _ctx.Cc_Venues.First(v => v.Name == venue),
                tour,
                notes);
        }

        public Dictionary<int, string[]> Lines
        {
            get { return AppSession.Lines; }
            set { AppSession.Lines = value; }
        }

        public bool IsFileLoaded
        {
            get { return AppSession.IsFileLoaded; }
            set { AppSession.IsFileLoaded = value; }
        }

        private void LoadLinesFromFile(string appDataFolder, string filename)
        {
            if (IsFileLoaded)
            {
                return;
            }

            var lines = new Dictionary<int, string[]>();

            var filepath = Path.Combine(_env.ContentRootPath, appDataFolder, filename);

            // Read the file and display it line by line.
            StreamReader file = new StreamReader(filepath);
            string line = file.ReadLine(); // just burning the header row
            while ((line = file.ReadLine()) != null)
            {
                // line = Era, Date, Notes, Band ... Band5, Arena, City, St, Tour, With ... With5
                string[] words = line.Split('\t');
                foreach (var w in words)
                {
                    w.Trim();
                }

                lines.Add(lines.Count, words);
            }

            Lines = lines;
            IsFileLoaded = true;
            file.Close();
        }

        #endregion
    }
}