using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Data;
using OrderManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<IActionResult> VaqonsAndOrders()
        {
            // if either vaqons or orders contain something, don't seed
            var vaqonsInDb = await _context
                .Vaqons.ToListAsync();
            var ordersInDb = await _context
                .Orders.ToListAsync();
            if (vaqonsInDb.Count() != 0 || ordersInDb.Count() != 0)
                return new JsonResult( new { Error = "Database already seeded" });
            // list of vaqon objects
            var vaqons = CreateVaqons();
            // insert each to database and catch the stupid tolist exception
            foreach(var vaqon in vaqons)
            {
                try
                {
                    if (vaqon.Type != null)
                    {
                        await _context
                            .Vaqons
                            .FromSqlInterpolated($"InsertVaqonMBK {vaqon.Type}").ToListAsync();
                    }
                    else
                    {
                        await _context
                            .Vaqons
                            .FromSqlInterpolated($"InsertVaqonMB {vaqon.FilePath}, {vaqon.DocType}").ToListAsync();
                    }
                }
                catch (InvalidOperationException ex)
                    when (ex.Message == "The required column 'Id' was not present in the results of a 'FromSql' operation.") { }
            }
            // list of order objects
            // get vaqon ids from database
            var vaqonIds = await _context
                .Vaqons
                .FromSqlRaw("GetUnusedVaqonIds")
                .Select(v => v.Id).ToListAsync();
            var orders = CreateOrders(vaqonIds);
            // insert each to database and catch the stupid tolist exception
            foreach(var order in orders)
            {
                // insert ordeer
                var xidmet = new SqlParameter("@Xidmet", order.Xidmet);
                var vahid = new SqlParameter("@Vahod", order.Vahid);
                var miqdar = new SqlParameter("@Miqdar", order.Miqdar);
                var qiymet = new SqlParameter("@Qiymet", order.Qiymet);
                var creationDate = new SqlParameter("@CreationDate", order.CreationDate);
                var vaqonId = new SqlParameter("@VaqonId", order.VaqonId);
                var orderId = new SqlParameter("@OrderId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                try
                {
                    await _context
                        .Orders
                        .FromSqlRaw("InsertOrder" +
                            " @Xidmet={0}, @Vahid={1}, @Miqdar={2}, @Qiymet={3}, @CreationDate={4}, @VaqonId={5}, @OrderId={6} output",
                            xidmet, vahid, miqdar, qiymet, creationDate, vaqonId, orderId)
                        .ToListAsync();
                }
                catch (InvalidOperationException ex)
                  when (ex.Message == "The required column 'Id' was not present in the results of a 'FromSql' operation.") { }
                // update order id of vaqon id
                try
                {
                    await _context
                        .Vaqons
                        .FromSqlInterpolated($"UpdateOrderIdOfVaqonId {(int)orderId.Value}")
                        .ToListAsync();
                }
                catch (InvalidOperationException ex)
                    when (ex.Message == "The required column 'Id' was not present in the results of a 'FromSql' operation.")
                { }
            }

            // return created orders and vaqons
            vaqonsInDb = await _context
                .Vaqons.ToListAsync();
            ordersInDb = await _context
                .Orders.ToListAsync();
            return new JsonResult(new {
                nVaqon = vaqonsInDb.Count(),
                nOrder = ordersInDb.Count(),
                Vaqons = vaqonsInDb,
                Orders = ordersInDb
            });
        }

        private static List<Vaqon> CreateVaqons()
        {
            var vaqons = new List<Vaqon>();
            // 5 milli broker
            vaqons.Add(new Vaqon {
                DocType = DocTypeEnum.QAIME,
                FilePath = "/Source/qaime1.pdf"
            });
            vaqons.Add(new Vaqon
            {
                DocType = DocTypeEnum.QAIME,
                FilePath = "/Source/qaime2.pdf"
            });
            vaqons.Add(new Vaqon
            {
                DocType = DocTypeEnum.QAIME,
                FilePath = "/Source/qaime3.pdf"
            });
            vaqons.Add(new Vaqon
            {
                DocType = DocTypeEnum.QAIME,
                FilePath = "/Source/qaime4.pdf"
            });
            vaqons.Add(new Vaqon
            {
                DocType = DocTypeEnum.QAIME,
                FilePath = "/Source/qaime5.pdf"
            });
            // 5 milli brokerden kecmis
            vaqons.Add(new Vaqon { Type = TypeEnum.ITXAL });
            vaqons.Add(new Vaqon { Type = TypeEnum.QISA_ITXAL });
            vaqons.Add(new Vaqon { Type = TypeEnum.ITXAL });
            vaqons.Add(new Vaqon { Type = TypeEnum.QISA_ITXAL });
            vaqons.Add(new Vaqon { Type = TypeEnum.ITXAL });

            return vaqons;
        }

        private static List<Order> CreateOrders(List<int> vaqonIds)
        {
            // don't think DI would work with static methods
            var random = new Random();
            var orders = new List<Order>();
            // 8 orders, leave 2 vaqons empty
            for(var i = 0; i < 8; i++)
            {
                // random between 0 to 3, since currency enum has 3 values
                var vahid = random.Next(4);
                // random between 0 to 2, since xidmet enum has 2 values
                var xidmet = random.Next(3);
                orders.Add(new Order { 
                    CreationDate = new DateTime(2012 + i, 2, 12 + i),
                    Miqdar = i + 3,
                    Qiymet = (23.5M + (decimal)i) * 3.2M,
                    Vahid = (CurrencyEnum)vahid,
                    Xidmet = (XidmetEnum)xidmet,
                    VaqonId = vaqonIds[i]
                });
            }
            return orders;
        }

        /// only dev
        //[HttpGet]
        public async Task<IActionResult> Users()
        {
            // if there are users in db, don't seed
            var usersInDb = await _context
                .Users.ToListAsync();
            if (usersInDb.Count() != 0)
                return new JsonResult(new { Error = "Database already seeded" });
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
