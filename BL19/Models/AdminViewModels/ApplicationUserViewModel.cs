﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BL19.Models
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }
    }
}
