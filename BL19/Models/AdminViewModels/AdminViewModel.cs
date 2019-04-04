using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL19.Models
{
    public class AdminViewModel
    {
        public List<ApplicationUserListViewModel> Users { get; set; }
        public List<ApplicationRoleListViewModel> Roles { get; set; }
    }
}
