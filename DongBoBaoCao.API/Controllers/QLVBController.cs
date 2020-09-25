using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.Services;
using DongBoBaoCao.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QLVBController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IQLVBService _qLVBService;

        private readonly string _baseAddress;
        private readonly string _bearToken;
        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;

        public QLVBController(IConfiguration config, IQLVBService qLVBService)
        {
            _config = config;
            _qLVBService = qLVBService;

            _baseAddress = _config.GetSection("QLCH:baseAddress").Value;
            _bearToken = _config.GetSection("QLCH:bearToken").Value;
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
            var result = await _qLVBService.GetDanhSachDuLieuAsync(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public async Task<IActionResult> GetDanhSachDuLieuTrongNgayAsync(int page = 1, int limit = 10)
        {
            var result = await _qLVBService.GetDanhSachDuLieuTrongNgayAsync(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuAsync()
        {
            int total = await _qLVBService.CreateDanhSachDuLieuAsync();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuTrongNgayAsync()
        {
            int total = await _qLVBService.CreateDanhSachDuLieuTrongNgayAsync();
            return Ok(new { total });
        }
    }
}
