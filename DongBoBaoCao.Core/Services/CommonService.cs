using DongBoBaoCao.Core.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DongBoBaoCao.Core.Interfaces;

namespace DongBoBaoCao.Core.Services
{
    public class CommonService: ICommonService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;
        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;
        private readonly string _baseAddressDw;
        private readonly string _extensionDw;
        private readonly int _limit;
        private readonly string _token;
        private readonly string _fromDate;
        private readonly string _toDate;

        public CommonService(IConfiguration config, IHttpService httpService, ILoginService loginService)
        {
            _config = config;
            _loginService = loginService;
            _httpService = httpService;
            _danhSachDuLieu = _config.GetSection("DanhSachDuLieu").Value;
            _danhSachDuLieuTrongNgay = _config.GetSection("DanhSachDuLieuTrongNgay").Value;
            _baseAddressDw = _config.GetSection("DW:baseAddress").Value;
            _extensionDw = _config.GetSection("DW:extension").Value;
            _limit = Convert.ToInt32(_config.GetSection("limit").Value);
            _token = _loginService.GetToken();
            _fromDate = _config.GetSection("fromDate").Value;
            _toDate = _config.GetSection("toDate").Value;
        }

        public int CreateDanhSachDuLieu(string baseAddress, string bearToken)
        {
            int page = 1;
            int total = 0;

            while (true)
            {
                ICollection<VanBan> listVanBan = GetDanhSachDuLieu(baseAddress, bearToken, page);
                _httpService.Post(_baseAddressDw, _extensionDw, null, listVanBan);
                int count = listVanBan.Count;
                total += count;
                if (listVanBan.Count <= 0) break;
                page++;
            }

            return total;
        }

        public int CreateDanhSachDuLieuTrongNgay(string baseAddress, string bearToken)
        {
            int page = 1;
            int total = 0;

            while (true)
            {
                var listVanBan = GetDanhSachDuLieuTrongNgay(baseAddress, bearToken, page);
                _httpService.Post(_baseAddressDw, _extensionDw, null, listVanBan);
                int count = listVanBan.Count;
                total += count;
                if (count <= 0) break;
                page++;
            }

            return total;
        }

        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string bearToken, int page)
        {
            page++;
            var input = new DanhSachDuLieuInput
            {
                token = _token,
                fromDate = _fromDate,
                toDate = _toDate,
                page = page,
                limit = _limit
            };
            var rs = _httpService.Post(baseAddress, _danhSachDuLieu, bearToken, input);
            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            var token = _loginService.GetToken();

            var danhSachDuLieuInput = new DanhSachDuLieuInput
            {
                fromDate = fromDate,
                toDate = toDate,
                page = page,
                limit = limit,
                token = token
            };

            var rs = _httpService.Post(baseAddress, danhSachDuLieu, bearToken, danhSachDuLieuInput);
            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string bearToken, int page)
        {
            var input = new DanhSachDuLieuTrongNgayInput
            {
                token = _token,
                page = page,
                limit = _limit
            };

            var rs = _httpService.Post(baseAddress, _danhSachDuLieuTrongNgay, bearToken, input);
            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var token = _loginService.GetToken();

            var danhSachDuLieuTrongNgayInput = new DanhSachDuLieuTrongNgayInput
            {
                page = page,
                limit = limit,
                token = token
            };

            var rs = _httpService.Post(baseAddress, danhSachDuLieuTrongNgay, bearToken, danhSachDuLieuTrongNgayInput);
            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }
    }
}
