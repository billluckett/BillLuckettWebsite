using BL19.Models.ConcertModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL19.SessionManager
{
    public class AppSession
    {
        public static Dictionary<int, string[]> Lines { get; set; }

        public static bool IsFileLoaded = false;
    }
}