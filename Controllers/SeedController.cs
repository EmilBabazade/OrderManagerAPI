using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Data;
using OrderManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public SeedController(
                ApplicationDbContext context,
                RoleManager<IdentityRole> roleManager,
                UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        const string STANDART_ROLE = "Standart";
        const string ADMIN_ROLE = "Administrator";

        /// only dev
        //[HttpGet]
        public async Task<IActionResult> Users()
        {
            // create default roles id they dont exist
            if (await _roleManager.FindByNameAsync(STANDART_ROLE) == null)
                await _roleManager.CreateAsync(new IdentityRole(STANDART_ROLE));

            if (await _roleManager.FindByNameAsync(ADMIN_ROLE) == null)
                await _roleManager.CreateAsync(new IdentityRole(ADMIN_ROLE));

            // list of CREATED users
            var addedUsers = new List<User>();

            // create admin user if it doesnt exist
            var adminEmail = "admin@email.com";
            if (await _userManager.FindByNameAsync(adminEmail) == null)
                await CreateUser(adminEmail, addedUsers, true);

            // create standart user if it doesnt exist
            var standartEmail = "standart@email.com";
            if (await _userManager.FindByNameAsync(standartEmail) == null)
                await CreateUser(standartEmail, addedUsers);

            // persist the changes if at least one user has been created 
            if (addedUsers.Count > 0)
                await _context.SaveChangesAsync();

            return new JsonResult(new { 
                Count = addedUsers.Count,
                Users = addedUsers
            });
        }

        private async Task<ActionResult> CreateUser(string email, List<User> addedUsers, bool isAdmin = false)
        {
            var name = isAdmin ? "admin" : "user";
            // new account
            var user = new User
            {
                FirstName = name,
                LastName = $"{name}ovich",
                Email = $"{name}@{name}.com",
                UserName = $"{name}@{name}.com"
            };
            // insert to db
            await _userManager.CreateAsync(user, "Qwerty@2");
            // assing roles
            await _userManager.AddToRoleAsync(user, STANDART_ROLE);
            if (isAdmin)
                await _userManager.AddToRoleAsync(user, ADMIN_ROLE);
            // add to created users list
            addedUsers.Add(user);

            return Ok();
        }
    }
}
