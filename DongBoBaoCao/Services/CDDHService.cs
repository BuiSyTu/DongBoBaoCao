using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using DongBoBaoCao.Interfaces;
using System;

namespace DongBoBaoCao.Core.Services
{
    public class CDDHService : ICDDHService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;

        private readonly string _baseAddress;
        private readonly string _bearToken;


        public CDDHService(IConfiguration config, ICommonService commonService)
        {
            _config = config;
            _commonService = commonService;

            _baseAddress = _config.GetSection("CDDH:baseAddress").Value;
            _bearToken = _config.GetSection("CDDH:bearToken").Value;
        }

        public int CreateDanhSachDuLieu()
        {
            int total = _commonService.CreateDanhSachDuLieu(_baseAddress, _bearToken);
            return total;
        }


        public int CreateDanhSachDuLieuTrongNgay()
        {
            int total = _commonService.CreateDanhSachDuLieuTrongNgay(_baseAddress, _bearToken);
            return total;
        }


        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            var result = _commonService.GetDanhSachDuLieu(baseAddress, danhSachDuLieu, bearToken, fromDate, toDate, page, limit);
            return result;
        }

        public ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var result = _commonService.GetDanhSachDuLieuTrongNgay(baseAddress, danhSachDuLieuTrongNgay, bearToken, page, limit);
            return result;
        }

        public void RandomChiTieuBaoCao()
        {
            var indicators = new List<Indicator>();
            Random random = new Random();

            var DH04 = new Indicator
            {
                Code = "DH04",
                value = 800 + random.Next(-50, 50)
            };

            var DH0401 = new Indicator
            {
                Code = "DH0401",
                value = DH04.value
            };

            var DH040101 = new Indicator
            {
                Code = "DH040101",
                value = DH0401.value
            };

            var DH04010101 = new Indicator
            {
                Code = "DH04010101",
                value = DH040101.value * (50 + random.Next(-10, 10)) / 100
            };

            var DH04010102 = new Indicator
            {
                Code = "DH04010102",
                value = DH040101.value - DH04010101.value
            };

            var DH040102 = new Indicator
            {
                Code = "DH040102",
                value = DH0401.value
            };

            var DH04010201 = new Indicator
            {
                Code = "DH04010201",
                value = DH040102.value * (20 + random.Next(-2, 2)) / 100
            };

            var DH04010202 = new Indicator
            {
                Code = "DH04010202",
                value = DH040102.value * (15 + random.Next(-2, 2)) / 100
            };

            var DH04010203 = new Indicator
            {
                Code = "DH04010203",
                value = DH040102.value * (15 + random.Next(-2, 2)) / 100
            };

            var DH04010204 = new Indicator
            {
                Code = "DH04010204",
                value = DH040102.value * (15 + random.Next(-2, 2)) / 100
            };

            var DH04010205 = new Indicator
            {
                Code = "DH04010205",
                value = DH040102.value * (15 + random.Next(-2, 2)) / 100
            };

            var DH04010206 = new Indicator
            {
                Code = "DH04010206",
                value = DH040102.value - DH04010201.value - DH04010202.value - DH04010203.value - DH04010204.value - DH04010205.value
            };

        }
    }
}
