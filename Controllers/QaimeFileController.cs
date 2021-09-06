using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QaimeFileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public QaimeFileController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost, Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateFile([FromForm(Name = "file")] IFormFile file) // always null for some reason
        {
            if (file == null)
                return StatusCode(400);
            var source = Path.Combine(_env.WebRootPath, "Source");
            if (file.Length > 0)
            {
                var filePath = Path.Combine(source, file.FileName);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }
            else
                return StatusCode(400);
            return Ok();
        }
    }
}
