using DongBoBaoCao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PAKNController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IPAKNService _pAKNService;

        private readonly string _baseAddress;
        private readonly string _bearToken;
        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;

        public PAKNController(IConfiguration config, IPAKNService pAKNService)
        {
            _config = config;
            _baseAddress = _config.GetSection("PAKN:baseAddress").Value;
            _bearToken = _config.GetSection("PAKN:bearToken").Value;
            _danhSachDuLieu = _config.GetSection("DanhSachDuLieu").Value;
            _danhSachDuLieuTrongNgay = _config.GetSection("DanhSachDuLieuTrongNgay").Value;

            _pAKNService = pAKNService;
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
            var result = await _pAKNService.GetDanhSachDuLieuAsync(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public async Task<IActionResult> GetDanhSachDuLieuTrongNgayAsync(int page = 1, int limit = 10)
        {
            var result = await _pAKNService.GetDanhSachDuLieuTrongNgayAsync(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuAsync()
        {
            int total = await _pAKNService.CreateDanhSachDuLieuAsync();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuTrongNgayAsync()
        {
            int total = await _pAKNService.CreateDanhSachDuLieuTrongNgayAsync();
            return Ok(new { total });
        }
    }
}
