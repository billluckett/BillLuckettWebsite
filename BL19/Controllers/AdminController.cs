using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BL19.Data;
using BL19.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BL19.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _ctx;
        private readonly string _externalCookieScheme;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public AdminController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          RoleManager<ApplicationRole> roleManager,
          ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _ctx = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Users");
        }

        #region users
        [HttpGet]
        public IActionResult Users()
        {
            List<ApplicationUserListViewModel> vm = new List<ApplicationUserListViewModel>();
            ApplicationUser[] users = _userManager.Users.ToArray();

            for (var i = 0; i < users.Length; i++)
            {
                ApplicationUser u = users[i];
                var roles = string.Join(", ", _userManager.GetRolesAsync(u).Result);
                var user = new ApplicationUserListViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Username = u.UserName,
                    Email = u.Email,
                    Roles = string.Join(", ", roles)
                };

                vm.Add(user);
            }

            vm = vm.OrderBy(u => u.Username).ToList();
            return View("Users", vm);
        }

        [HttpGet]
        public IActionResult AddApplicationUser()
        {
            ViewData["Title"] = "Users";

            ApplicationUserViewModel model = new ApplicationUserViewModel()
            {
                ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).OrderBy(r => r.Text).ToList()
            };
            return PartialView("_AddApplicationUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddApplicationUser(ApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                    if (applicationRole != null)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Users");
                        }
                    }
                }
            }
            return PartialView("_AddApplicationUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> EditApplicationUser(string id)
        {
            EditApplicationUserViewModel model = new EditApplicationUserViewModel()
            {
                ApplicationRoles = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id
                }).OrderBy(r => r.Text).ToList()
            };
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    model.Name = user.Name;
                    model.Email = user.Email;
                    model.ApplicationRoleId = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Id;
                }
            }
            return PartialView("_EditApplicationUser", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditApplicationUser(string id, EditApplicationUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Email = model.Email;
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        IdentityResult addPasswordResult = null;
                        if (!string.IsNullOrEmpty(model.NewPassword))
                        {
                            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                            addPasswordResult = await _userManager.ResetPasswordAsync(user, code, model.NewPassword);
                        }

                        string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                        string existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;

                        if (existingRoleId != model.ApplicationRoleId)
                        {
                            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                            if (roleResult.Succeeded)
                            {
                                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.ApplicationRoleId);
                                if (applicationRole != null)
                                {
                                    IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                }
                            }
                        }

                        return RedirectToAction("Users");
                    }
                }
            }
            return PartialView("_EditApplicationUser", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationUser(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    name = applicationUser.Name;
                }
            }
            return PartialView("_DeleteApplicationUser", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApplicationUser(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
                if (applicationUser != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(applicationUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Users");
                    }
                }
            }
            return View();
        }
        #endregion users

        #region roles
        [HttpGet]
        public IActionResult Roles()
        {
            List<ApplicationRoleListViewModel> vm = _roleManager.Roles.Select(r => new ApplicationRoleListViewModel
            {
                Id = r.Id,
                RoleName = r.Name,
                Description = r.Description,
                //NumberOfUsers = r.Users.Count
                NumberOfUsers = _ctx.UserRoles.Where(ur => ur.RoleId == r.Id).Count()
            }).OrderBy(r => r.RoleName).ToList();

            return View("Roles", vm);
        }

        [HttpGet]
        public async Task<IActionResult> AddEditApplicationRole(string id)
        {
            ViewData["Title"] = "Roles";

            ApplicationRoleViewModel model = new ApplicationRoleViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    model.Id = applicationRole.Id;
                    model.RoleName = applicationRole.Name;
                    model.Description = applicationRole.Description;
                }
            }
            return PartialView("_AddEditApplicationRole", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditApplicationRole(string id, ApplicationRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isExist = !String.IsNullOrEmpty(id);
                ApplicationRole applicationRole = isExist ? await _roleManager.FindByIdAsync(id) :
                new ApplicationRole
                {
                    CreatedDate = DateTime.UtcNow
                };
                applicationRole.Name = model.RoleName;
                applicationRole.Description = model.Description;
                applicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                IdentityResult roleResult = isExist ? await _roleManager.UpdateAsync(applicationRole)
                                                    : await _roleManager.CreateAsync(applicationRole);
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Roles");
                }
            }
            return View("_AddEditApplicationRole", model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteApplicationRole(string id)
        {
            string name = string.Empty;
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    name = applicationRole.Name;
                }
            }
            return PartialView("_DeleteApplicationRole", name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteApplicationRole(string id, IFormCollection form)
        {
            if (!String.IsNullOrEmpty(id))
            {
                ApplicationRole applicationRole = await _roleManager.FindByIdAsync(id);
                if (applicationRole != null)
                {
                    IdentityResult roleResult = _roleManager.DeleteAsync(applicationRole).Result;
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction("Roles");
                    }
                }
            }
            return View();
        }
        #endregion roles
    }
}