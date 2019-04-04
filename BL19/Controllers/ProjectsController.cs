using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BL19.Models;

namespace BL19.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            var projects = new List<Project>() {
                new Project() { Title = "p" },
                new Project() { Title = "q" },
                new Project() { Title = "a" },
                new Project() { Title = "b" },
            };

            var projectsArray = projects.OrderBy(p => p.Title).ToArray();
            return View("Projects", projectsArray);
        }
    }
}