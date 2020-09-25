using DongBoBaoCao.API.ViewModels;
using DongBoBaoCao.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DongBoBaoCao.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KNTCController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IKNTCService _kNTCService;

        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;
        private readonly string _baseAddress;
        private readonly string _bearToken;


        public KNTCController(IConfiguration config, IKNTCService kNTCService)
        {
            _config = config;
            _kNTCService = kNTCService;

            _baseAddress = _config.GetSection("KNTC:baseAddress").Value;
            _bearToken = _config.GetSection("KNTC:bearToken").Value;
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
            var result = await _kNTCService.GetDanhSachDuLieuAsync(_baseAddress, _danhSachDuLieu, _bearToken, fromDate, toDate, page, limit);
            return Ok(result);
        }

        [HttpGet("DanhSachDuLieuTrongNgay")]
        public async Task<IActionResult> GetDanhSachDuLieuTrongNgayAsync(int page = 1, int limit = 10)
        {
            var result = await _kNTCService.GetDanhSachDuLieuTrongNgayAsync(_baseAddress, _danhSachDuLieuTrongNgay, _bearToken, page, limit);
            return Ok(result);
        }

        [HttpPost("DanhSachDuLieu/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuAsync()
        {
            int total = await _kNTCService.CreateDanhSachDuLieuAsync();
            return Ok(new { total });
        }

        [HttpPost("DanhSachDuLieuTrongNgay/All")]
        public async Task<IActionResult> CreateAllDanhSachDuLieuTrongNgayAsync()
        {
            int total = await _kNTCService.CreateDanhSachDuLieuTrongNgayAsync();
            return Ok(new { total });
        }

        [HttpGet("DanhSachDuLieu/HCM/{page}")]
        public IActionResult GetDanhSachDuLieuHCM(int page)
        {
            Uri postUrl = new Uri("http://kntc.tphcm.gov.vn/_layouts/tandan/kntc/KNTCWCFService.svc/DanhSachDuLieu");
            var userJson = "{\"fromDate\": \"01/01/2020\",\"toDate\": \"23/09/2020\",\"page\":" + page + ",\"limit\": 1000}";
            HttpContent c = new StringContent(userJson, Encoding.UTF8, "application/json");
            string duLieu = PostURI(postUrl, c).Result;
            List<VanBanKNTC> objs = JsonConvert.DeserializeObject<List<VanBanKNTC>>(duLieu);
            return Ok(new { total = objs.Count, data = objs });
        }

        private async Task<string> PostURI(Uri urlAPI, HttpContent inputAPI)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(urlAPI, inputAPI);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            return response;
        }
    }
}
