using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Services
{
    public class QLVBService : IQLVBService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;
        private readonly IDateTimeService _dateTimeService;

        private readonly string _baseAddress;
        private readonly string _bearToken;

        public QLVBService(IConfiguration config, ICommonService commonService, IDateTimeService dateTimeService)
        {
            _config = config;
            _commonService = commonService;
            _dateTimeService = dateTimeService;

            _baseAddress = _config.GetSection("QLVB:baseAddress").Value;
            _bearToken = _config.GetSection("QLVB:bearToken").Value;
        }

        public void AddChiTieuBaoCao()
        {
            var listOuDataItem = new List<OUDataItem>();
            var dataYears = new List<int> { 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var tuNgays = new List<string>();
            var denNgays = new List<string>();

            for (var i = 0; i < dataYears.Count; i++)
            {
                for (var j = 0; j < months.Count; j++)
                {
                    tuNgays.Add(_dateTimeService.GetStartDateOfMonth(months[j], dataYears[i]));
                    denNgays.Add(_dateTimeService.GetLastDateOfMonth(months[j], dataYears[i]));
                }
            }


            var periodIds = new List<int> { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21};
            var officeCodes = new List<string> { "000-00-12-H40", "000-00-19-H40", "000-00-20-H40" };
            var indicatorCodes = new List<string> { "DH01010201", "DH01010202", "DH01010203", "DH01010204", "DH01010205" };
            var fields = new List<string> { "TrangThaiChung", "TrangThaiChung", "TrangThaiChung", "TrangThaiChung", "TrangThaiChung" };
            var values = new List<string> { "01", "02##04", "02##03", "03##04", "03##03" };

            var chartInputs = new List<ChartInput>();
            for (var i = 0; i < fields.Count; i++)
            {
                for (var j = 0; j < officeCodes.Count; j ++)
                {
                    for (var k = 0; k < tuNgays.Count; k++)
                    {
                        chartInputs.Add(new ChartInput
                        {
                            denNgay = denNgays[k],
                            fields = fields[i],
                            maDonVi = officeCodes[j],
                            maPhanMem = "QLVB",
                            tuNgay = tuNgays[k],
                            values = values[i]
                        });
                    }
                }
            }

            //_commonService.AddOrUpdateIndicator();
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

        public ChartOutput GetDuLieuLoc(ChartInput input)
        {
            var result = _commonService.GetDuLieuLoc(input);
            return result;
        }
    }
}
