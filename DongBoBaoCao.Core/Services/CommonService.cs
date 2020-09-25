using DongBoBaoCao.Core.ViewModels;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using DongBoBaoCao.Core.Interfaces;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;
        private readonly string _danhSachDuLieu;
        private readonly string _danhSachDuLieuTrongNgay;
        private readonly string _baseAddressDw;
        private readonly string _extensionDw;
        private readonly int _limit;
        private readonly string _fromDate;
        private readonly string _toDate;
        private readonly string _token;

        public CommonService(IConfiguration config, IHttpService httpService, ILoginService loginService)
        {
            _config = config;
            _loginService = loginService;
            _httpService = httpService;
            _token = null;

            _danhSachDuLieu = _config.GetSection("DanhSachDuLieu").Value;
            _danhSachDuLieuTrongNgay = _config.GetSection("DanhSachDuLieuTrongNgay").Value;
            _baseAddressDw = _config.GetSection("DW:baseAddress").Value;
            _extensionDw = _config.GetSection("DW:extension").Value;
            _limit = Convert.ToInt32(_config.GetSection("limit").Value);
            _fromDate = _config.GetSection("fromDate").Value;
            _toDate = _config.GetSection("toDate").Value;
            _token = _loginService.GetToken();
        }

        public int CreateDanhSachDuLieu(string baseAddress, string bearToken)
        {
            int page = 1;
            int total = 0;

            while (true)
            {
                ICollection<VanBan> listVanBan = GetDanhSachDuLieu(baseAddress, bearToken, page);
                if (listVanBan is null || listVanBan.Count <= 0) break;

                _httpService.Post(_baseAddressDw, _extensionDw, null, listVanBan);
                total += listVanBan.Count;
                page++;
            }

            return total;
        }

        public async Task<int> CreateDanhSachDuLieuAsync(string baseAddress, string bearToken)
        {
            int page = 1;
            int total = 0;

            while (true)
            {
                ICollection<VanBan> listVanBan = await GetDanhSachDuLieuAsync(baseAddress, bearToken, page);
                if (listVanBan is null || listVanBan.Count <= 0) break;

                await _httpService.PostAsync(_baseAddressDw, _extensionDw, null, listVanBan);
                total += listVanBan.Count;
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
                if (listVanBan is null || listVanBan.Count <= 0) break;

                _httpService.Post(_baseAddressDw, _extensionDw, null, listVanBan);
                total += listVanBan.Count;
                page++;
            }

            return total;
        }

        public async Task<int> CreateDanhSachDuLieuTrongNgayAsync(string baseAddress, string bearToken)
        {
            int page = 1;
            int total = 0;

            while (true)
            {
                var listVanBan = await GetDanhSachDuLieuTrongNgayAsync(baseAddress, bearToken, page);
                if (listVanBan is null || listVanBan.Count <= 0) break;

                await _httpService.PostAsync(_baseAddressDw, _extensionDw, null, listVanBan);
                total += listVanBan.Count;
                page++;
            }

            return total;
        }

        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string bearToken, int page)
        {
            string token = _token ?? _loginService.GetToken();

            var input = new DanhSachDuLieuInput
            {
                token = token,
                fromDate = _fromDate,
                toDate = _toDate,
                page = page,
                limit = _limit
            };
            var rs = _httpService.Post(baseAddress, _danhSachDuLieu, bearToken, input);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string bearToken, int page)
        {
            string token = _token ?? _loginService.GetToken();

            var input = new DanhSachDuLieuInput
            {
                token = token,
                fromDate = _fromDate,
                toDate = _toDate,
                page = page,
                limit = _limit
            };
            var rs = await _httpService.PostAsync(baseAddress, _danhSachDuLieu, bearToken, input);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            string token = _token ?? _loginService.GetToken();

            var danhSachDuLieuInput = new DanhSachDuLieuInput
            {
                fromDate = fromDate,
                toDate = toDate,
                page = page,
                limit = limit,
                token = token
            };

            var rs = _httpService.Post(baseAddress, danhSachDuLieu, bearToken, danhSachDuLieuInput);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            string token = _token ?? _loginService.GetToken();

            var danhSachDuLieuInput = new DanhSachDuLieuInput
            {
                fromDate = fromDate,
                toDate = toDate,
                page = page,
                limit = limit,
                token = token
            };

            var rs = await _httpService.PostAsync(baseAddress, danhSachDuLieu, bearToken, danhSachDuLieuInput);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

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

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string bearToken, int page)
        {
            var input = new DanhSachDuLieuTrongNgayInput
            {
                token = _token,
                page = page,
                limit = _limit
            };

            var rs = await _httpService.PostAsync(baseAddress, _danhSachDuLieuTrongNgay, bearToken, input);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var danhSachDuLieuTrongNgayInput = new DanhSachDuLieuTrongNgayInput
            {
                page = page,
                limit = limit,
                token = _token
            };

            var rs = _httpService.Post(baseAddress, danhSachDuLieuTrongNgay, bearToken, danhSachDuLieuTrongNgayInput);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var danhSachDuLieuTrongNgayInput = new DanhSachDuLieuTrongNgayInput
            {
                page = page,
                limit = limit,
                token = _token
            };

            var rs = await _httpService.PostAsync(baseAddress, danhSachDuLieuTrongNgay, bearToken, danhSachDuLieuTrongNgayInput);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            APIResult result = JsonConvert.DeserializeObject<APIResult>(rs);
            return result.data;
        }
    }
}
