using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using System;
using DongBoBaoCao.ViewModels;
using Newtonsoft.Json;

namespace DongBoBaoCao.Services
{
    public class PAKNService
    {
        private readonly IConfiguration _config;
        private readonly IHttpService _httpService;
        private readonly ILoginService _loginService;
        private readonly IDateTimeService _dateTimeService;

        private readonly string _loginToken;

        private readonly string _urlGet;
        private readonly string _tokenGet;

        private readonly string _urlAdd;

        private readonly string _urlFilter;

        private readonly string _fromDate;
        private readonly int _fromMonth;
        private readonly int _fromYear;
        private readonly string _toDate;
        private readonly int _toMonth;
        private readonly int _toYear;
        private readonly int _limit;

        public PAKNService(IConfiguration config, IHttpService httpService, ILoginService loginService, IDateTimeService dateTimeService)
        {
            _config = config;
            _httpService = httpService;
            _loginService = loginService;
            _dateTimeService = dateTimeService;

            _loginToken = _loginService.GetToken();

            _urlFilter = _config.GetSection("PAKN:filter:address").Value;

            _urlGet = _config.GetSection("PAKN:get:address").Value;
            _tokenGet = _config.GetSection("PAKN:get:bearToken").Value;

            _urlAdd = _config.GetSection("PAKN:add:address").Value;

            _fromDate = _config.GetSection("fromDate:date").Value;
            _fromMonth = Convert.ToInt32(_config.GetSection("fromDate:month").Value);
            _fromYear = Convert.ToInt32(_config.GetSection("fromDate:year").Value);
            _toDate = _config.GetSection("toDate:date").Value;
            _toMonth = Convert.ToInt32(_config.GetSection("toDate:month").Value);
            _toYear = Convert.ToInt32(_config.GetSection("toDate:year").Value);
            _limit = Convert.ToInt32(_config.GetSection("limit").Value);
        }

        public void AddChiTieuBaoCao()
        {
            var dataYears = new List<int> {  2020 };
            var months = new List<int> { 10, 11 };
            var periodIds = new List<int> { 10, 11 };

            var officeCodes = new List<string> {
                 "000-00-00-H40",
                "000-00-19-H40", "000-00-20-H40", "000-00-21-H40", "000-00-22-H40", "000-00-23-H40", "000-00-25-H40", "000-00-26-H40", "000-00-27-H40", "000-00-28-H40", "000-00-02-H40", "000-00-03-H40", "000-00-04-H40", "000-00-05-H40", "000-00-06-H40", "000-00-07-H40", "000-00-08-H40", "000-00-09-H40", "000-00-10-H40", "000-00-11-H40", "000-00-13-H40", "000-00-14-H40", "000-00-15-H40", "000-00-16-H40", "000-00-17-H40", "000-00-18-H40", "000-30-24-H40", "000-24-24-H40", "000-34-24-H40"
            };
            var indicatorCodes = new List<string> {
                "DH03010101",
                "DH03010102",
                "DH03010103",
                "DH03010104",
                "DH03010201",
                "DH03010202",
                "DH03010203",
                "DH03010204",
                "DH03010205",
                "DH0301030101",
                "DH0301030102",
                "DH0301030201",
                "DH0301030202",
                "DH0301030203",
                "DH03010401",
                "DH03010402",
                "DH03010403",
                "DH030301",
                "DH030302",
                "DH030303"
            };
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

        public void CreateDanhSachDuLieu()
        {
            for (var year = _fromYear; year <= _toYear; year++)
            {
                for (var month = _fromMonth; month <= _toMonth; month++)
                {
                    int page = 1;

                    while (true)
                    {
                        ICollection<PAKNViewModel> pAKNs = GetDanhSachDuLieu(month, year, page);
                        if (pAKNs is null || pAKNs.Count <= 0) break;

                        _httpService.Post(_urlAdd, null, pAKNs);
                        if (pAKNs.Count < _limit) break;
                        page++;
                    }
                }
            }
        }

        public ICollection<PAKNViewModel> GetDanhSachDuLieu(int month, int year, int? page)
        {
            string token = _loginToken ?? _loginService.GetToken();

            var input = new DanhSachDuLieuInput
            {
                token = token,
                fromDate = _dateTimeService.GetStartDateOfMonth(month, year, "dd/MM/yyyy"),
                toDate = _dateTimeService.GetLastDateOfMonth(month, year, "dd/MM/yyyy"),
                page = page ?? 1,
                limit = _limit
            };

            string rs = _httpService.Post(_urlGet, _tokenGet, input);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<APIResult<PAKNViewModel>>(rs);
            return result.data;
        }


        public ICollection<PAKNViewModel> GetDanhSachDuLieu(int? page)
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

            var result = JsonConvert.DeserializeObject<APIResult<PAKNViewModel>>(rs);
            return result.data;
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
