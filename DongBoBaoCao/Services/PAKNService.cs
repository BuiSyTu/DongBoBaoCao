using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using System.Threading.Tasks;
using DongBoBaoCao.Interfaces;
using System;

namespace DongBoBaoCao.Core.Services
{
    public class PAKNService : IPAKNService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;
        private readonly IHttpService _httpService;

        private readonly string _baseAddress;
        private readonly string _bearToken;



        public PAKNService(IConfiguration config, ICommonService commonService, IHttpService httpService)
        {
            _config = config;
            _commonService = commonService;
            _httpService = httpService;

            _baseAddress = _config.GetSection("PAKN:baseAddress").Value;
            _bearToken = _config.GetSection("PAKN:bearToken").Value;
        }

        public void AddChiTieuBaoCao()
        {
            throw new NotImplementedException();
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
                        var DH03 = new Indicator
                        {
                            Code = "DH03",
                            value = 600 + random.Next(0, 300)
                        };
                        indicators.Add(DH03);

                        var DH0301 = new Indicator
                        {
                            Code = "DH0301",
                            value = DH03.value
                        };
                        indicators.Add(DH0301);

                        var DH030101 = new Indicator
                        {
                            Code = "DH030101",
                            value = DH0301.value
                        };
                        indicators.Add(DH030101);

                        var DH03010101 = new Indicator
                        {
                            Code = "DH03010101",
                            value = DH030101.value * (75 + random.Next(-7, 7)) / 100
                        };
                        indicators.Add(DH03010101);

                        var DH03010102 = new Indicator
                        {
                            Code = "DH03010102",
                            value = DH030101.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010102);

                        var DH03010103 = new Indicator
                        {
                            Code = "DH03010103",
                            value = DH030101.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010103);

                        var DH03010104 = new Indicator
                        {
                            Code = "DH03010104",
                            value = DH030101.value - DH03010101.value - DH03010102.value - DH03010103.value
                        };
                        indicators.Add(DH03010104);

                        var DH030102 = new Indicator
                        {
                            Code = "DH030102",
                            value = DH0301.value
                        };
                        indicators.Add(DH030102);

                        var DH03010201 = new Indicator
                        {
                            Code = "DH03010201",
                            value = DH030102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010201);

                        var DH03010202 = new Indicator
                        {
                            Code = "DH03010202",
                            value = DH030102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010202);

                        var DH03010203 = new Indicator
                        {
                            Code = "DH03010203",
                            value = DH030102.value * (15 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010202);

                        var DH03010204 = new Indicator
                        {
                            Code = "DH03010204",
                            value = DH030102.value * (7 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010204);

                        var DH03010206 = new Indicator
                        {
                            Code = "DH03010206",
                            value = DH030102.value * (7 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010206);

                        var DH03010207 = new Indicator
                        {
                            Code = "DH03010207",
                            value = DH030102.value * (7 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010207);

                        var DH03010208 = new Indicator
                        {
                            Code = "DH03010208",
                            value = DH030102.value * (25 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010208);

                        var DH03010209 = new Indicator
                        {
                            Code = "DH03010209",
                            value = DH030102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010209);

                        var DH03010210 = new Indicator
                        {
                            Code = "DH03010209",
                            value = DH030102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH03010210);

                        var DH03010205 = new Indicator
                        {
                            Code = "DH03010205",
                            value = DH030102.value - DH03010201.value - DH03010202.value - DH03010203.value - DH03010204.value - DH03010206.value - DH03010207.value - DH03010208.value - DH03010209.value - DH03010210.value
                        };
                        indicators.Add(DH03010205);

                        var DH030103 = new Indicator
                        {
                            Code = "DH030103",
                            value = DH0301.value
                        };
                        indicators.Add(DH030103);

                        var DH03010301 = new Indicator
                        {
                            Code = "DH03010301",
                            value = DH030103.value * (20 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH03010301);

                        var DH0301030101 = new Indicator
                        {
                            Code = "DH0301030101",
                            value = DH03010301.value * (90 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH0301030101);

                        var DH0301030102 = new Indicator
                        {
                            Code = "DH03010303",
                            value = DH03010301.value - DH0301030101.value
                        };
                        indicators.Add(DH0301030102);

                        var DH03010302 = new Indicator
                        {
                            Code = "DH03010302",
                            value = DH030103.value - DH03010301.value
                        };
                        indicators.Add(DH03010302);

                        var DH0301030201 = new Indicator
                        {
                            Code = "DH0301030201",
                            value = DH03010302.value * (60 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH0301030201);

                        var DH0301030202 = new Indicator
                        {
                            Code = "DH0301030202",
                            value = DH03010302.value * (30 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH0301030201);

                        var DH0301030203 = new Indicator
                        {
                            Code = "DH0301030203",
                            value = DH03010302.value - DH0301030201.value - DH0301030202.value
                        };
                        indicators.Add(DH0301030203);

                        var DH030104 = new Indicator
                        {
                            Code = "DH030104",
                            value = DH0301.value
                        };
                        indicators.Add(DH030104);

                        var DH03010401 = new Indicator
                        {
                            Code = "DH03010401",
                            value = DH030104.value * (60 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH03010401);

                        var DH03010402 = new Indicator
                        {
                            Code = "DH03010402",
                            value = DH030104.value * (25 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH03010402);

                        var DH03010403 = new Indicator
                        {
                            Code = "DH03010403",
                            value = DH030104.value - DH03010401.value - DH030104.value
                        };
                        indicators.Add(DH03010403);

                        var DH0303 = new Indicator
                        {
                            Code = "DH0303",
                            value = DH03.value
                        };
                        indicators.Add(DH0303);

                        var DH030301 = new Indicator
                        {
                            Code = "DH030301",
                            value = DH0303.value * (50 + random.Next(-1, 1)) / 100
                        };
                        indicators.Add(DH030301);

                        var DH030302 = new Indicator
                        {
                            Code = "DH030302",
                            value = DH0303.value * (48 + random.Next(-1, 1)) / 100
                        };
                        indicators.Add(DH030302);

                        var DH030303 = new Indicator
                        {
                            Code = "DH030303",
                            value = DH0303.value - DH030301.value - DH030302.value
                        };
                        indicators.Add(DH030303);
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
