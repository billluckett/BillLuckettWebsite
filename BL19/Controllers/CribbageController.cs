﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BL19.Controllers
{
    public class CribbageController : Controller
    {
        public IActionResult Index()
        {
            return View("Cribbage");
        }
    }
}