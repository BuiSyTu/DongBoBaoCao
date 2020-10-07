using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DongBoBaoCao.Core.Services.VinhLong;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VinhLongController : ControllerBase
    {
        private readonly TruongHocService _truongHocService;

        public VinhLongController(TruongHocService truongHocService)
        {
            _truongHocService = truongHocService;
        }

        [HttpGet("TruongHoc")]
        public IActionResult GetTruongHoc()
        {
            var result = _truongHocService.Get();
            return Ok(result);
        }
    }
}
