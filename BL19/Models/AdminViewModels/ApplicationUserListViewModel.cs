using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class ApplicationUserListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Roles { get; set; }
    }
}
