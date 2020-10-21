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
        private readonly IHttpService _httpService;

        private readonly string _baseAddress;
        private readonly string _bearToken;


        public CDDHService(IConfiguration config, ICommonService commonService, IHttpService httpService)
        {
            _config = config;
            _commonService = commonService;
            _httpService = httpService;

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



                        #region indicators
                        var DH04 = new Indicator
                        {
                            Code = "DH04",
                            value = 800 + random.Next(-50, 50)
                        };
                        indicators.Add(DH04);

                        var DH0401 = new Indicator
                        {
                            Code = "DH0401",
                            value = DH04.value
                        };
                        indicators.Add(DH0401);

                        var DH040101 = new Indicator
                        {
                            Code = "DH040101",
                            value = DH0401.value
                        };
                        indicators.Add(DH040101);

                        var DH04010101 = new Indicator
                        {
                            Code = "DH04010101",
                            value = DH040101.value * (50 + random.Next(-10, 10)) / 100
                        };
                        indicators.Add(DH04010101);

                        var DH04010102 = new Indicator
                        {
                            Code = "DH04010102",
                            value = DH040101.value - DH04010101.value
                        };
                        indicators.Add(DH04010102);

                        var DH040102 = new Indicator
                        {
                            Code = "DH040102",
                            value = DH0401.value
                        };
                        indicators.Add(DH040102);

                        var DH04010201 = new Indicator
                        {
                            Code = "DH04010201",
                            value = DH040102.value * (20 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH04010201);

                        var DH04010202 = new Indicator
                        {
                            Code = "DH04010202",
                            value = DH040102.value * (15 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH04010202);

                        var DH04010203 = new Indicator
                        {
                            Code = "DH04010203",
                            value = DH040102.value * (15 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH04010203);

                        var DH04010204 = new Indicator
                        {
                            Code = "DH04010204",
                            value = DH040102.value * (15 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH04010204);

                        var DH04010205 = new Indicator
                        {
                            Code = "DH04010205",
                            value = DH040102.value * (15 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH04010205);

                        var DH04010206 = new Indicator
                        {
                            Code = "DH04010206",
                            value = DH040102.value - DH04010201.value - DH04010202.value - DH04010203.value - DH04010204.value - DH04010205.value
                        };
                        indicators.Add(DH04010206);

                        var DH040103 = new Indicator
                        {
                            Code = "DH040103",
                            value = DH0401.value
                        };
                        indicators.Add(DH040103);

                        var DH04010301 = new Indicator
                        {
                            Code = "DH04010301",
                            value = DH040103.value * (60 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH04010301);

                        var DH04010302 = new Indicator
                        {
                            Code = "DH04010302",
                            value = DH040103.value * (10 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH04010302);

                        var DH04010303 = new Indicator
                        {
                            Code = "DH04010303",
                            value = DH040103.value * (15 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH04010303);

                        var DH04010304 = new Indicator
                        {
                            Code = "DH04010304",
                            value = DH040103.value - DH04010301.value - DH04010302.value - DH04010303.value
                        };
                        indicators.Add(DH04010304);

                        var DH0402 = new Indicator
                        {
                            Code = "DH0402",
                            value = DH04.value
                        };
                        indicators.Add(DH0402);

                        var DH040201 = new Indicator
                        {
                            Code = "DH040201",
                            value = DH0402.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH040201);

                        var DH040203 = new Indicator
                        {
                            Code = "DH040203",
                            value = DH0402.value * (20 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH040203);

                        var DH040204 = new Indicator
                        {
                            Code = "DH040204",
                            value = DH0402.value * (10 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH040204);

                        var DH040202 = new Indicator
                        {
                            Code = "DH040202",
                            value = DH0402.value - DH040201.value - DH040203.value - DH040204.value
                        };
                        indicators.Add(DH040202);
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
