using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                // TODO: check vaqon doesn't belong to another order
                await InsertOrder(order);
                return StatusCode(201);
            }
            catch (SqlException ex) 
            {
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                return StatusCode(400);
            }
        }

        private async Task<ActionResult> InsertOrder(OrderEditDTO order)
        {
            // check vaqon doesn't belong to any order
            var vaqons = await _context
                .Vaqons
                .FromSqlRaw("GetUnusedVaqonIds")
                .Select(v => v.Id)
                .ToListAsync();
            if (!vaqons.Contains(order.VaqonId))
                throw new Exception();
            // insert the order to the database
            var xidmet = new SqlParameter("@Xidmet", StringToXidmet(order.Xidmet));
            var vahid = new SqlParameter("@Vahod", StringToVahid(order.Vahid));
            var miqdar = new SqlParameter("@Miqdar", order.Miqdar);
            var qiymet = new SqlParameter("@Qiymet", order.Qiymet);
            var creationDate = new SqlParameter("@CreationDate", order.Tarix);
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
              when (ex.Message == "The required column 'Id' was not present in the results of a 'FromSql' operation.")
            {
                // fromsqlraw or fromsqlinterpolated doesn't run if you don't call .ToList or .ToListAsync afterward
                // it doesn't work even if you call _context.SaveChanges or .SaveChangesAsync at some other line
                // idk why, i don't care why, but calling .ToListAsync() at end throws this exception
                // since it tries to conver the result of query to List<Vaqon> object
                // but InsertVaqonMB stored procedure INSERTS the data and does not return anything
            }
            // update the order id of the vaqon
            try
            {
                await _context
                    .Vaqons
                    .FromSqlInterpolated($"UpdateOrderIdOfVaqonId {(int)orderId.Value}")
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
                when (ex.Message == "The required column 'Id' was not present in the results of a 'FromSql' operation.") { }
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
