using DongBoBaoCao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        public IActionResult GetDanhSachDuLieu(string fromDate = "01/01/2020", string toDate = "23/09/2020", int page = 1, int limit = 10)
        {
            var result = _pAKNService.GetDanhSachDuLieu(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public IActionResult GetDanhSachDuLieuTrongNgay(int page = 1, int limit = 10)
        {
            var result = _pAKNService.GetDanhSachDuLieuTrongNgay(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public IActionResult CreateAllDanhSachDuLieu()
        {
            int total = _pAKNService.CreateDanhSachDuLieu();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public IActionResult CreateAllDanhSachDuLieuTrongNgay()
        {
            int total = _pAKNService.CreateDanhSachDuLieuTrongNgay();
            return Ok(new { total });
        }
    }
}
