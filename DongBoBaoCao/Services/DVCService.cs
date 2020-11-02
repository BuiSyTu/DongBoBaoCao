using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using DongBoBaoCao.Interfaces;
using System;
using DongBoBaoCao.ViewModels;
using Newtonsoft.Json;

namespace DongBoBaoCao.Core.Services
{
    public class DVCService : IDVCService
    {
        private readonly IConfiguration _config;
        private readonly IHttpService _httpService;
        private readonly ILoginService _loginService;
        private readonly IDateTimeService _dateTimeService;

        private readonly string _loginToken;

        private readonly string _urlGet;
        private readonly string _tokenGet;

        private readonly string _urlFilter;
        private readonly string _urlAdd;

        private readonly string _fromDate;
        private readonly string _toDate;
        private readonly int _limit;


        public DVCService(IConfiguration config, IHttpService httpService, ILoginService loginService, IDateTimeService dateTimeService)
        {
            _config = config;
            _httpService = httpService;
            _loginService = loginService;
            _dateTimeService = dateTimeService;

            _loginToken = _loginService.GetToken();

            _urlFilter = _config.GetSection("DVC:filter:address").Value;
            _urlGet = _config.GetSection("DVC:get:address").Value;
            _tokenGet = _config.GetSection("DVC:get:bearToken").Value;
            _urlAdd = _config.GetSection("DVC:add:address").Value;

            _fromDate = _config.GetSection("fromDate").Value;
            _toDate = _config.GetSection("toDate").Value;
            _limit = Convert.ToInt32(_config.GetSection("limit").Value);
        }

        public void CreateDanhSachDuLieu()
        {
            int page = 1;

            while (true)
            {
                ICollection<DVCViewModel> dVCs = GetDanhSachDuLieu(page);
                if (dVCs is null || dVCs.Count <= 0) break;

                _httpService.Post(_urlAdd, null, dVCs);
                if (dVCs.Count < _limit) break;
                page++;
            }
        }

        public ICollection<DVCViewModel> GetDanhSachDuLieu(int? page)
        {
            string token = _loginToken ?? _loginService.GetToken();

            var input = new DanhSachDuLieuInput
            {
                token = token,
                fromDate = _fromDate,
                toDate = _toDate,
                page = page ?? 1,
                limit = _limit
            };

            string rs = _httpService.Post(_urlGet, _tokenGet, input);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            DVCResult result = JsonConvert.DeserializeObject<DVCResult>(rs);
            return result.data;
        }

        public void AddChiTieuBaoCao()
        {
            var dataYears = new List<int> { 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var officeCodes = new List<string> { "000-00-19-H40", "000-00-20-H40", "000-00-21-H40", "000-00-22-H40", "000-00-23-H40", "000-00-25-H40", "000-00-26-H40", "000-00-27-H40", "000-00-28-H40", "000-00-02-H40", "000-00-03-H40", "000-00-04-H40", "000-00-05-H40", "000-00-06-H40", "000-00-07-H40", "000-00-08-H40", "000-00-09-H40", "000-00-10-H40", "000-00-11-H40", "000-00-13-H40", "000-00-14-H40", "000-00-15-H40", "000-00-16-H40", "000-00-17-H40", "000-00-18-H40" };
            var indicatorCodes = new List<string> { "DH02010101", "DH02010102", "DH02010103", "DH02010104", "DH02010105", "DH02010201", "DH02010202", "DH02010203", "DH02010204", "DH02010301", "DH02010302", "DH02010303", "DH02010304", "DH02010305", "DH02010306", "DH02010401", "DH02010402" };
            foreach (var datayear in dataYears)
            {
                for (var j = 0; j < months.Count; j++)
                {
                    var month = months[j];
                    var periodId = periodIds[j];
                    foreach (var officeCode in officeCodes)
                    {
                        foreach (var indicatorCode in indicatorCodes)
                        {
                            var indicatorInput = new IndicatorInput
                            {
                                IndicatorCode = indicatorCode,
                                Month = month,
                                OfficeCode = officeCode,
                                SoftwareCode = "PAKN",
                                Year = datayear
                            };

                            string filterResult = _httpService.Post(_urlFilter, null, indicatorInput);

                            if (string.IsNullOrEmpty(filterResult)) continue;

                            var indicatorOutput = JsonConvert.DeserializeObject<IndicatorOutput>(filterResult);
                            var value = indicatorOutput.Value;

                            var oUDataItem = new OUDataItem
                            {
                                dataTypeId = 3, // Thực hiện
                                dataYear = datayear,
                                indicatorCode = indicatorCode,
                                officeCode = officeCode,
                                periodId = periodId,
                                value = value,
                                textValue = value.ToString()
                            };

                            _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, oUDataItem);

                        }
                    }
                }
            }
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
