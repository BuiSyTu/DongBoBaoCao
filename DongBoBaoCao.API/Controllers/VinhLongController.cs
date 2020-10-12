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
        private readonly TinhThanhService _tinhThanhService;

        public VinhLongController(TruongHocService truongHocService, TinhThanhService tinhThanhService)
        {
            _truongHocService = truongHocService;
            _tinhThanhService = tinhThanhService;
        }

        [HttpGet("TruongHoc")]
        public IActionResult GetTruongHoc()
        {
            var result = _truongHocService.Get();
            return Ok(result);
        }

        [HttpGet("TinhThanh")]
        public IActionResult GetTinhThanh()
        {
            var result = _tinhThanhService.Get();
            return Ok(result);
        }
    }
}
