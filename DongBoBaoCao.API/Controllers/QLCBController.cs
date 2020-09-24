using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QLCBController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IQLCBService _qLCBService;

        private readonly string _baseAddress;
        private readonly string _bearToken;
        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;

        public QLCBController(IConfiguration config, IQLCBService qLCBService)
        {
            _config = config;
            _qLCBService = qLCBService;

            _baseAddress = _config.GetSection("QLCB:baseAddress").Value;
            _bearToken = _config.GetSection("QLCB:bearToken").Value;
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
            var result = _qLCBService.GetDanhSachDuLieu(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public IActionResult GetDanhSachDuLieuTrongNgay(int page = 1, int limit = 10)
        {
            var result = _qLCBService.GetDanhSachDuLieuTrongNgay(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public IActionResult CreateAllDanhSachDuLieu()
        {
            int total = _qLCBService.CreateDanhSachDuLieu();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public IActionResult CreateAllDanhSachDuLieuTrongNgay()
        {
            int total = _qLCBService.CreateDanhSachDuLieuTrongNgay();
            return Ok(new { total });
        }
    }
}
