using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagerAPI.Data;
using OrderManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaqonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VaqonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize]
        public async Task<List<Vaqon>> GetVaqons() =>
            await _context.Vaqons.FromSqlRaw("GetVaqons").ToListAsync();

        //[HttpPost]
        //public async Task<ActionResult> CreateVaqon(VaqonMIlliBrokerDTO vaqon)
        //{
        //    await _context.Vaqons.FromSqlInterpolated($"");
        //}
    }
}
