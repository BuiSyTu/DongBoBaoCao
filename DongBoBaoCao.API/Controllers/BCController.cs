using DongBoBaoCao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BCController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IBCService _bCService;

        private readonly string _baseAddress;
        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;
        private readonly string _bearToken;

        public BCController(IConfiguration config, IBCService bCService)
        {
            _config = config;
            _bCService = bCService;

            _baseAddress = _config.GetSection("BC:baseAddress").Value;
            _bearToken = _config.GetSection("BC:bearToken").Value;
            _danhSachDuLieu = _config.GetSection("DanhSachDuLieu").Value;
            _danhSachDuLieuTrongNgay = _config.GetSection("DanhSachDuLieuTrongNgay").Value;
        }

        [HttpGet("Information")]
        public IActionResult Information()
        {
            return Ok(new
            {
                _baseAddress,
                _bearToken,
                _danhSachDuLieuTrongNgay,
                _danhSachDuLieu
            });
        }

        [HttpGet("DanhSachDuLieu")]
        public IActionResult GetDanhSachDuLieu(string fromDate = "01/01/2020", string toDate = "23/09/2020", int page = 1, int limit = 10)
        {
            var result = _bCService.GetDanhSachDuLieu(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public IActionResult GetDanhSachDuLieuTrongNgay(int page = 1, int limit = 10)
        {
            var result = _bCService.GetDanhSachDuLieuTrongNgay(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public IActionResult CreateAllDanhSachDuLieu()
        {
            int total = _bCService.CreateDanhSachDuLieu();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public IActionResult CreateAllDanhSachDuLieuTrongNgay()
        {
            int total = _bCService.CreateDanhSachDuLieuTrongNgay();
            return Ok(new { total });
        }
    }
}
