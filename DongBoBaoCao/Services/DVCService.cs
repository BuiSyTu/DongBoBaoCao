using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using System.Threading.Tasks;
using DongBoBaoCao.Interfaces;

namespace DongBoBaoCao.Core.Services
{
    public class DVCService : IDVCService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;
        private readonly IHttpService _httpService;

        private readonly string _baseAddress;
        private readonly string _bearToken;


        public DVCService(IConfiguration config, ICommonService commonService, IHttpService httpService)
        {
            _config = config;
            _commonService = commonService;
            _httpService = httpService;

            _baseAddress = _config.GetSection("DVC:baseAddress").Value;
            _bearToken = _config.GetSection("DVC:bearToken").Value;
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

        public void AddChiTieuBaoCao()
        {
            var indicators = new List<Indicator>();

            var DH02 = new Indicator
            {
                Code = "DH02"
            };

            var DH0201 = new Indicator
            {
                Code = "DH0201"
            };

            var DH020101 = new Indicator
            {
                Code = "DH020101"
            };

            var DH020102 = new Indicator
            {
                Code = "DH020102",
                fields = "TrangThaiPhanMem",
                fieldValues = "02"
            };

            var DH020103 = new Indicator
            {
                Code = "DH020102",
                fields = "TrangThaiPhanMem",
                fieldValues = "04"
            };
        }

        public void RandomChiTieuBaoCao()
        {

        }
    }
}
