using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL19.Data;
using BL19.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BL19.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext _ctx;
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogController(BlogDbContext context, UserManager<ApplicationUser> userManager)
        {
            _ctx = context;
            _userManager = userManager;
        }

        #region blogs
        public Task<IActionResult> Index()
        {
            return ShowBlog(null);
        }

        public Task<IActionResult> Sports()
        {
            return ShowBlog("Sports");
        }

        public Task<IActionResult> Politics()
        {
            return ShowBlog("Politics");
        }

        public Task<IActionResult> WebDev()
        {
            return ShowBlog("WebDev");
        }

        public Task<IActionResult> Science()
        {
            return ShowBlog("Science");
        }

        public Task<IActionResult> Parenthood()
        {
            return ShowBlog("Parenthood");
        }

        private async Task<IActionResult> ShowBlog(string cat)
        {
            var cats = await _ctx.BlogCategories.ToListAsync();
            var ecs = await _ctx.BlogEntryCategories.ToListAsync();
            BlogViewModel vm = new BlogViewModel();
            Category category = cats.FirstOrDefault(c => c.Name == cat);

            if (category != null)
            {
                ViewData["Title"] = cat;
                vm.Entries = await _ctx.BlogEntryCategories
                    .Where(w => w.Category.Name.Equals(cat))
                    .Select(s => s.Entry)
                    .OrderByDescending(o => o.Id)
                    .OrderByDescending(o => o.CreatedOn)
                    .ToListAsync();
                vm.HeaderImage = category.HeaderImage;
                //if (cat == "Sports") vm.HeaderImage = "IconicNflPics.jpg";
                //if (cat == "Sports") vm.HeaderImage = "IconicMlbPics.jpg";
                vm.RightSide = "_BlogSidebar" + cat;
            }
            else
            {
                ViewData["Title"] = "Blog";
                vm.Entries = await _ctx.BlogEntries
                    .OrderByDescending(o => o.CreatedOn)
                    .ThenByDescending(o => o.Id)
                    .ToListAsync();
                vm.HeaderImage = ""; // TODO: make "BlogHeaderGeneral.jpg", put it on website, and then set it here
                vm.RightSide = "_BlogSidebarGeneral";
            }

            return View("Blog", vm);
        }
        #endregion


        #region admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            ViewData["Title"] = "Admin";

            var entries = await _ctx.BlogEntries
                .OrderByDescending(o => o.Id)
                .OrderByDescending(o => o.CreatedOn)
                .ToListAsync();

            var cats = await _ctx.BlogCategories.ToListAsync();
            var ecs = await _ctx.BlogEntryCategories.ToListAsync();

            return View(entries);
        }

        // GET: Blog/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var vm = new EntryViewModel(new Entry(), _ctx.BlogCategories.ToList());
            return View(vm);
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var entry = vm.Entry;
                entry.CreatedOn = DateTime.UtcNow;
                entry.ModifiedOn = entry.CreatedOn;
                _ctx.BlogEntries.Add(entry);
                await _ctx.SaveChangesAsync();

                var categoriesToAdd = vm.Categories.Where(c => c.Id != 0);
                foreach (var category in categoriesToAdd)
                {
                    _ctx.BlogEntryCategories.Add(new EntryCategory(entry, category));
                }
                await _ctx.SaveChangesAsync();

                return RedirectToAction("Admin");
            }
            return View(vm);
        }

        // GET: Blog/Details/5
        [Authorize(Roles = "Admin")]
        public Task<IActionResult> Details(int? id)
        {
            return ShowEntry(id, "Details");
        }

        // GET: Blog/Edit/5
        [Authorize(Roles = "Admin")]
        public Task<IActionResult> Edit(int? id)
        {
            return ShowEntry(id, "Edit");
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EntryViewModel vm)
        {
            var entry = vm.Entry;
            if (id != entry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    entry.ModifiedOn = DateTime.UtcNow;
                    _ctx.Update(entry);
                    await _ctx.SaveChangesAsync();

                    var updatedCategoryIds = vm.Categories.Where(c => c.Id != 0).Select(s => s.Id);
                    var entryCats = _ctx.BlogEntryCategories.Where(w => w.EntryId == entry.Id);

                    foreach (var ec in entryCats)
                    {
                        if (!updatedCategoryIds.Contains(ec.CategoryId))
                        {
                            _ctx.BlogEntryCategories.Remove(ec);
                        }
                    }
                    await _ctx.SaveChangesAsync();

                    foreach (var categoryId in updatedCategoryIds)
                    {
                        if (!entryCats.Any(a => a.CategoryId == categoryId))
                        {
                            _ctx.BlogEntryCategories.Add(new EntryCategory(entry.Id, categoryId));
                        }
                    }
                    await _ctx.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntryExists(entry.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Admin");
            }
            return View(vm);
        }

        // GET: Blog/Delete/5
        [Authorize(Roles = "Admin")]
        public Task<IActionResult> Delete(int? id)
        {
            return ShowEntry(id, "Delete");
        }

        // POST: Blog/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entry = await _ctx.BlogEntries.SingleOrDefaultAsync(m => m.Id == id);
            _ctx.BlogEntries.Remove(entry);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Admin");
        }

        private async Task<IActionResult> ShowEntry(int? id, string view)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entry = await _ctx.BlogEntries.SingleOrDefaultAsync(m => m.Id == id);
            if (entry == null)
            {
                return NotFound();
            }

            var vm = new EntryViewModel(entry, _ctx.BlogCategories.ToList())
            {
                ActiveCategoryIds = _ctx.BlogEntryCategories.Where(w => w.EntryId == entry.Id).Select(s => s.CategoryId).ToList()
            };
            return View(view, vm);
        }

        private bool EntryExists(int id)
        {
            return _ctx.BlogEntries.Any(e => e.Id == id);
        }

        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminTables()
        {
            var vm = new ManageBlogViewModel()
            {
                Categories = await _ctx.BlogCategories.ToListAsync(),
                Tags = await _ctx.BlogTags.ToListAsync()
            };
            return View(vm);
        }
        #endregion admin
    }
}