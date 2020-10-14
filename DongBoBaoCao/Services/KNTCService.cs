using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using System.Threading.Tasks;
using DongBoBaoCao.Interfaces;

namespace DongBoBaoCao.Core.Services
{
    public class KNTCService : IKNTCService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;

        private readonly string _baseAddress;
        private readonly string _bearToken;


        public KNTCService(IConfiguration config, ICommonService commonService)
        {
            _config = config;
            _commonService = commonService;

            _baseAddress = _config.GetSection("KNTC:baseAddress").Value;
            _bearToken = _config.GetSection("KNTC:bearToken").Value;
        }

        public int CreateDanhSachDuLieu()
        {
            int total = _commonService.CreateDanhSachDuLieu(_baseAddress, _bearToken);
            return total;
        }

        public async Task<int> CreateDanhSachDuLieuAsync()
        {
            int total = await _commonService.CreateDanhSachDuLieuAsync(_baseAddress, _bearToken);
            return total;
        }

        public int CreateDanhSachDuLieuTrongNgay()
        {
            int total = _commonService.CreateDanhSachDuLieuTrongNgay(_baseAddress, _bearToken);
            return total;
        }

        public async Task<int> CreateDanhSachDuLieuTrongNgayAsync()
        {
            int total = await _commonService.CreateDanhSachDuLieuTrongNgayAsync(_baseAddress, _bearToken);
            return total;
        }

        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            var result = _commonService.GetDanhSachDuLieu(baseAddress, danhSachDuLieu, bearToken, fromDate, toDate, page, limit);
            return result;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            var result = await _commonService.GetDanhSachDuLieuAsync(baseAddress, danhSachDuLieu, bearToken, fromDate, toDate, page, limit);
            return result;
        }

        public ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var result = _commonService.GetDanhSachDuLieuTrongNgay(baseAddress, danhSachDuLieuTrongNgay, bearToken, page, limit);
            return result;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var result = await _commonService.GetDanhSachDuLieuTrongNgayAsync(baseAddress, danhSachDuLieuTrongNgay, bearToken, page, limit);
            return result;
        }
    }
}
