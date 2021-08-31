using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            try
            {
                await InsertOrder(order);
                return StatusCode(201);
            }
            catch (SqlException)
            {
                return StatusCode(400);
            }
            catch (Exception)
            {
                return StatusCode(400);
            }
        }

        private async Task<ActionResult> InsertOrder(OrderEditDTO order)
        {
            XidmetEnum? xidmet = StringToXidmet(order.Xidmet);
            CurrencyEnum? vahid = StringToVahid(order.Vahid);
            try
            {
                await _context
                    .Orders
                    .FromSqlInterpolated($"InsertOrder {xidmet}, {vahid}, {order.Miqdar}, {order.Qiymet}, {order.Tarix}, {order.VaqonId}")
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
              when (ex.Message == "The required column 'Id' was not present in the results of a 'FromSql' operation.")
            {
                // fromsqlraw or fromsqlinterpolated doesn't run if you don't call .ToList or .ToListAsync afterward
                // it doesn't work even if you call _context.SaveChanges or .SaveChangesAsync at some other line
                // idk why, i don't care why, but calling .ToListAsync() at end throws this exception
                // since it tries to conver the result of query to List<Vaqon> object
                // but InsertVaqonMB stored procedure INSERTS the data and does not return anything
            }
            return Ok();
        }

        private CurrencyEnum? StringToVahid(string vahid)
        {
            switch(vahid.ToUpper())
            {
                case "AZN":
                    return CurrencyEnum.AZN;
                case "EUR":
                    return CurrencyEnum.EUR;
                case "RUB":
                    return CurrencyEnum.RUB;
                case "USD":
                    return CurrencyEnum.USD;
                default:
                    return null;
            }
        }

        private XidmetEnum? StringToXidmet(string xidmet)
        {
            switch (xidmet.ToLower())
            {
                case "gomrukleme":
                    return XidmetEnum.GOMRUKLEME;
                case "mal_qebulu":
                    return XidmetEnum.MAL_QEBULU;
                case "saxlanc":
                    return XidmetEnum.SAXLANC;
                default:
                    return null;
            }
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
