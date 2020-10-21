using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using DongBoBaoCao.Interfaces;
using System;

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
            var dataYears = new List<int> { 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var officeCodes = new List<string> { "000-00-19-H40", "000-00-20-H40", "000-00-21-H40", "000-00-22-H40", "000-00-23-H40", "000-00-25-H40", "000-00-26-H40", "000-00-27-H40", "000-00-28-H40", "000-00-02-H40", "000-00-03-H40", "000-00-04-H40", "000-00-05-H40", "000-00-06-H40", "000-00-07-H40", "000-00-08-H40", "000-00-09-H40", "000-00-10-H40", "000-00-11-H40", "000-00-13-H40", "000-00-14-H40", "000-00-15-H40", "000-00-16-H40", "000-00-17-H40", "000-00-18-H40" };

            for (var i = 0; i < dataYears.Count; i++)
            {
                var datayear = dataYears[i];
                for (var j = 0; j < months.Count; j++)
                {
                    var month = months[j];
                    var periodId = periodIds[j];
                    for (var k = 0; k < officeCodes.Count; k++)
                    {
                        Random random = new Random();
                        var indicators = new List<Indicator>();

                        #region indicator
                        var DH02 = new Indicator
                        {
                            Code = "DH02",
                            value = 600 + random.Next(0, 300)
                        };
                        indicators.Add(DH02);

                        var DH0201 = new Indicator
                        {
                            Code = "DH0201",
                            value = DH02.value
                        };
                        indicators.Add(DH0201);

                        var DH020101 = new Indicator
                        {
                            Code = "DH020101",
                            value = DH02.value
                        };
                        indicators.Add(DH020101);

                        var DH02010101 = new Indicator
                        {
                            Code = "DH02010101",
                            value = DH020101.value * (18 + random.Next(-7, 7)) / 100
                        };
                        indicators.Add(DH02010101);

                        var DH02010102 = new Indicator
                        {
                            Code = "DH02010102",
                            value = DH020101.value * (18 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH02010102);

                        var DH02010103 = new Indicator
                        {
                            Code = "DH02010103",
                            value = DH020101.value * (18 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH02010103);

                        var DH02010104 = new Indicator
                        {
                            Code = "DH02010104",
                            value = DH020101.value * (18 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH02010104);
                        var DH02010105 = new Indicator
                        {
                            Code = "DH02010105",
                            value = DH020101.value - DH02010101.value - DH02010102.value - DH02010103.value - DH02010104.value
                        };
                        indicators.Add(DH02010105);

                        var DH020102 = new Indicator
                        {
                            Code = "DH020102",
                            value = DH020101.value * (30 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH020102);

                        var DH02010201 = new Indicator
                        {
                            Code = "DH02010201",
                            value = DH020102.value * (30 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010201);

                        var DH02010202 = new Indicator
                        {
                            Code = "DH02010202",
                            value = DH020102.value * (10 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010202);

                        var DH02010203 = new Indicator
                        {
                            Code = "DH02010203",
                            value = DH020102.value * (30 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010203);

                        var DH02010204 = new Indicator
                        {
                            Code = "DH02010204",
                            value = DH020102.value - DH02010201.value - DH02010202.value - DH02010203.value
                        };
                        indicators.Add(DH02010204);

                        var DH020103 = new Indicator
                        {
                            Code = "DH020103",
                            value = DH0201.value * (50 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH020103);

                        var DH02010301 = new Indicator
                        {
                            Code = "DH02010301",
                            value = DH020103.value * (5 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010301);

                        var DH02010302 = new Indicator
                        {
                            Code = "DH02010302",
                            value = DH020103.value * (30 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010302);

                        var DH02010303 = new Indicator
                        {
                            Code = "DH02010303",
                            value = DH020103.value * (5 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010303);

                        var DH02010304 = new Indicator
                        {
                            Code = "DH02010304",
                            value = DH020103.value * (5 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010304);

                        var DH02010305 = new Indicator
                        {
                            Code = "DH02010305",
                            value = DH020103.value * (30 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010305);

                        var DH02010306 = new Indicator
                        {
                            Code = "DH02010306",
                            value = DH020103.value - DH02010301.value - DH02010302.value - DH02010303.value - DH02010304.value - DH02010305.value
                        };
                        indicators.Add(DH02010306);

                        var DH020104 = new Indicator
                        {
                            Code = "Dh020104",
                            value = DH0201.value * (5 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH020104);

                        var DH02010401 = new Indicator
                        {
                            Code = "DH02010401",
                            value = DH020104.value * (40 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010401);

                        var DH02010402 = new Indicator
                        {
                            Code = "DH02010402",
                            value = DH020104.value - DH02010401.value
                        };
                        indicators.Add(DH02010402);

                        var DH020105 = new Indicator
                        {
                            Code = "DH020105",
                            value = DH0201.value * (5 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH020105);

                        var DH02010501 = new Indicator
                        {
                            Code = "DH02010501",
                            value = DH020105.value * (40 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH02010501);

                        var DH02010502 = new Indicator
                        {
                            Code = "DH02010502",
                            value = DH020105.value - DH02010501.value
                        };
                        indicators.Add(DH02010502);

                        #endregion

                        var officeCode = officeCodes[k];
                        for (var l = 0; l < indicators.Count; l++)
                        {
                            var indicator = indicators[l];

                            var rs = _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, new OUDataItem
                            {
                                dataTypeId = 3, // Thực hiện
                                dataYear = datayear,
                                indicatorCode = indicator.Code,
                                officeCode = officeCode,
                                periodId = periodId,
                                value = indicator.value,
                                textValue = indicator.value.ToString()
                            });
                        }
                    }
                }
            }
        }
    }
}
