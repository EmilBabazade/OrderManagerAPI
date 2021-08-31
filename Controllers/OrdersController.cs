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
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize(Roles = "Standart")]
        public async Task<List<OrderDisplayDTO>> GetOrders()
        {
            // call get orders sp
            var orders = await _context
                .Orders
                .FromSqlRaw("GetOrders")
                .Select(order => OrderToDisplayDTO(order))
                .ToListAsync();
            // return what you got
            return orders;
        }

        [HttpPost, Authorize(Roles = "Administrator")]
        public async Task<ActionResult> CreateOrder(OrderEditDTO order) {
            throw new NotImplementedException();
        }

        private static OrderDisplayDTO OrderToDisplayDTO(Order order)
        {
            // convert xidmet to string
            var xidmet = "";
            switch (order.Xidmet) {
                case XidmetEnum.GOMRUKLEME:
                    xidmet = "gomrukleme";
                    break;
                case XidmetEnum.MAL_QEBULU:
                    xidmet = "mal_qebulu";
                    break;
                case XidmetEnum.SAXLANC:
                    xidmet = "saxlanc";
                    break;
            }
            // convert vahid to string
            var vahid = "";
            switch (order.Vahid)
            {
                case CurrencyEnum.AZN:
                    vahid = "AZN";
                    break;
                case CurrencyEnum.EUR:
                    vahid = "EUR";
                    break;
                case CurrencyEnum.RUB:
                    vahid = "RUB";
                    break;
                case CurrencyEnum.USD:
                    vahid = "USD";
                    break;
            }
            return new OrderDisplayDTO
            {
                Sifaris = order.Id,
                Xidmet = xidmet,
                Vahid = vahid,
                Miqdar = order.Miqdar,
                Qiymet = order.Qiymet,
                Tarix = order.CreationDate,
                Vaqon = order.VaqonId,
                TotalQiymet = order.Miqdar * order.Qiymet
            };
        }
    }
}
