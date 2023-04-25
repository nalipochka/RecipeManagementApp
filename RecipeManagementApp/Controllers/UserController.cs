using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeManagementApp.Context;
using RecipeManagementApp.Models.UserViewModels;
using System.Data;

namespace RecipeManagementApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            IEnumerable<IndexUserViewModel> usersVm = mapper.Map<IEnumerable<IndexUserViewModel>>(users);
            return View(usersVm);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel vM = mapper.Map<EditUserViewModel>(user);
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel vM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(vM.Id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Email = vM.Email;
                user.FirstName = vM.FirstName;
                user.LastName = vM.LastName;
                user.UserName = vM.Login;
                user.DateOfBirth = vM.DateOfBirth;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vM);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            DeleteUserViewModel vM = mapper.Map<DeleteUserViewModel>(user);
            return View(vM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (userManager.Users == null)
            {
                return NotFound();
            }
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(user);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel vM)
        {
            if (ModelState.IsValid)
            {
                User user = mapper.Map<User>(vM);
                var result = await userManager.CreateAsync(user, vM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(vM);
        }
        public async Task<IActionResult> ChangePassword(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordUserViewModel vM = mapper.Map<ChangePasswordUserViewModel>(user);
            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserViewModel vM)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(vM.Id);
                if (user == null)
                {
                    return NotFound();
                }
                var passwordValidator = HttpContext.RequestServices.GetRequiredService<IPasswordValidator<User>>();
                var passwordHasher = HttpContext.RequestServices.GetRequiredService<IPasswordHasher<User>>();
                var identityResult = await passwordValidator.ValidateAsync(userManager, user, vM.NewPassword);
                if (identityResult.Succeeded)
                {
                    string hashedPassword = passwordHasher.HashPassword(user, vM.NewPassword);
                    user.PasswordHash = hashedPassword;
                    await userManager.UpdateAsync(user);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }
            return View(vM);
        }

        public async Task<IActionResult> ChangeRole(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            List<IdentityRole> allRole = await roleManager.Roles.ToListAsync();
            //ChangeRoleViewModel vM = mapper.Map<ChangeRoleViewModel>(user);
            //vM.AllRoles = allRole;
            //vM.UserRoles = userRoles;
            ChangeRoleViewModel vM = new ChangeRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                AllRoles = allRole,
                UserRoles = userRoles
            };
             return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeRole(string? id, List<string> roles)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            IList<string> userRoles = await userManager.GetRolesAsync(user);
            IEnumerable<string> addedRoles = roles.Except(userRoles);
            IEnumerable<string> deletedRoles = userRoles.Except(roles);
            await userManager.AddToRolesAsync(user, addedRoles);
            await userManager.RemoveFromRolesAsync(user, deletedRoles);
            return RedirectToAction("Index", "User");
        }
    }
}
