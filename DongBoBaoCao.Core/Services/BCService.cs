using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;

namespace DongBoBaoCao.Core.Services
{
    public class BCService : IBCService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;

        private readonly string _baseAddress;
        private readonly string _bearToken;


        public BCService(IConfiguration config, ICommonService commonService)
        {
            _config = config;
            _commonService = commonService;

            _baseAddress = _config.GetSection("BC:baseAddress").Value;
            _bearToken = _config.GetSection("BC:bearToken").Value;
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
    }
}
