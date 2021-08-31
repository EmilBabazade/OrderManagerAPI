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
    public class VaqonsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VaqonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Standart")]
        public async Task<IEnumerable<VaqonDTO>> GetVaqons(bool getMilliBrokerdenKecmis = false)
        {
            IEnumerable<VaqonDTO> vaqons;
            if (getMilliBrokerdenKecmis)
            {
                vaqons = await _context
                    .Vaqons
                    .FromSqlRaw("GetVaqonsMBK")
                    .Select(v => VaqonToMilliBrokerKecmisDTO(v))
                    .ToListAsync();
            } else
            {
                vaqons = await _context
                    .Vaqons
                    .FromSqlRaw("GetVaqonsMB")
                    .Select(v => VaqonToMilliBrokerDTO(v))
                    .ToListAsync();
            }
            return vaqons;
        }

#nullable enable
        public class CreateVaqonParams {
            public VaqonBrokerXidmetindenKecmiDTO? vaqonMBK { get; set; }
            public VaqonMIlliBrokerDTO? vaqonMB { get; set; }
        };

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> CreateVaqon(CreateVaqonParams vaqon)
        {
            var vaqonMBK = vaqon.vaqonMBK;
            var vaqonMB = vaqon.vaqonMB;
            try
            {
                if (vaqonMBK != null)
                {
                    await InsertVaqonMBK(vaqonMBK);
                    return StatusCode(201);
                }
                
                if (vaqonMB != null) {
                    await InsertVaqonMB(vaqonMB);
                    return StatusCode(201);
                }
                return StatusCode(400);
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

        private async Task<ActionResult> InsertVaqonMBK(VaqonBrokerXidmetindenKecmiDTO vaqon)
        {
            var type = StringToType(vaqon.Nov);
            try
            {
                await _context
                .Vaqons
                .FromSqlInterpolated($"InsertVaqonMBK {type}").ToListAsync();
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

        private TypeEnum StringToType(string nov)
        {
            if (nov.ToLower() == "itxal")
                return TypeEnum.ITXAL;
            if (nov.ToLower() == "qisa_itxal")
                return TypeEnum.QISA_ITXAL;
            throw new Exception("Invalid Type");
        }

        private async Task<ActionResult> InsertVaqonMB(VaqonMIlliBrokerDTO vaqon)
        {
            var docType = StringToFileType(vaqon.DokumentNovu);
            try
            {
                await _context
                .Vaqons
                .FromSqlInterpolated($"InsertVaqonMB {vaqon.Dokument}, {docType}").ToListAsync();
            } catch (InvalidOperationException ex) 
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

        private DocTypeEnum StringToFileType(string docType)
        {
            if (docType.ToLower() == "qaime")
                return DocTypeEnum.QAIME;
            throw new Exception("Invalid Document Type");
        }


#nullable enable
        private static VaqonBrokerXidmetindenKecmiDTO VaqonToMilliBrokerKecmisDTO(Vaqon v)
        {
            string? type = null;
            if (v.Type == TypeEnum.ITXAL)
                type = "itxal";
            if (v.Type == TypeEnum.QISA_ITXAL)
                type = "qisa_itxal";
            return new VaqonBrokerXidmetindenKecmiDTO
            {
                Id = v.Id,
                Nov = type
            };
        }

#nullable enable
        private static VaqonMIlliBrokerDTO VaqonToMilliBrokerDTO(Vaqon v)
        {
            string? docType = v.DocType == DocTypeEnum.QAIME
                ? "qaime" : null;
            return new VaqonMIlliBrokerDTO
            {
                Id = v.Id,
                DokumentNovu = docType,
                Dokument = v.FilePath
            };
        }
    }
}
