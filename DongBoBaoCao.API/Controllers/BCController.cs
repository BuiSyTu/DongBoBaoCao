using DongBoBaoCao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetDanhSachDuLieuAsync(string fromDate = "01/01/2020", string toDate = "23/09/2020", int page = 1, int limit = 10)
        {
            var result = await _bCService.GetDanhSachDuLieuAsync(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public async Task<IActionResult> GetDanhSachDuLieuTrongNgayAsync(int page = 1, int limit = 10)
        {
            var result = await  _bCService.GetDanhSachDuLieuTrongNgayAsync(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuAsync()
        {
            int total = await _bCService.CreateDanhSachDuLieuAsync();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuTrongNgayAsync()
        {
            int total = await _bCService.CreateDanhSachDuLieuTrongNgayAsync();
            return Ok(new { total });
        }
    }
}
