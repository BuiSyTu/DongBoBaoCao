using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Interfaces;
using System;
using DongBoBaoCao.ViewModels;
using Newtonsoft.Json;

namespace DongBoBaoCao.Services
{
    public class CDDHService : ICDDHService
    {
        private readonly IConfiguration _config;
        private readonly IHttpService _httpService;
        private readonly ILoginService _loginService;
        private readonly IDateTimeService _dateTimeService;

        private readonly string _tokenGet;
        private readonly string _loginToken;

        private readonly string _urlGet;
        private readonly string _urlFilter;
        private readonly string _urlAdd;
        private readonly string _urlUpdate;

        private readonly string _fromDate;
        private readonly int _fromMonth;
        private readonly int _fromYear;
        private readonly string _toDate;
        private readonly int _toMonth;
        private readonly int _toYear;
        private readonly int _limit;


        public CDDHService(IConfiguration config, IHttpService httpService, ILoginService loginService, IDateTimeService dateTimeService)
        {
            _config = config;
            _httpService = httpService;
            _loginService = loginService;
            _dateTimeService = dateTimeService;

            _urlGet = _config.GetSection("CDDH:get:address").Value;
            _loginToken = _loginService.GetToken();
            _tokenGet = _config.GetSection("CDDH:get:bearToken").Value;
            _urlFilter = _config.GetSection("CDDH:filter:address").Value;
            _urlAdd = _config.GetSection("CDDH:add:address").Value;
            _urlUpdate = _config.GetSection("CapNhatChiTieuDonVi:address").Value;

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
            var dataYears = new List<int> { 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var officeCodes = new List<string> {
                "000-00-19-H40", "000-00-20-H40", "000-00-21-H40", "000-00-22-H40", "000-00-23-H40", "000-00-25-H40", "000-00-26-H40", "000-00-27-H40", "000-00-28-H40", "000-00-02-H40", "000-00-03-H40", "000-00-04-H40", "000-00-05-H40", "000-00-06-H40", "000-00-07-H40", "000-00-08-H40", "000-00-09-H40", "000-00-10-H40", "000-00-11-H40", "000-00-13-H40", "000-00-14-H40", "000-00-15-H40", "000-00-16-H40", "000-00-17-H40", "000-00-18-H40"
            };
            var indicatorCodes = new List<string> { 
                "DH04010101",
                "DH04010102",
                "DH04010201",
                "DH04010202",
                "DH04010203",
                "DH04010204",
                "DH04010205",
                "DH04010206",
                "DH04010301",
                "DH04010302",
                "DH04010303",
                "DH04010304",
                "DH040201",
                "DH040202",
                "DH040203",
                "DH040204",
                "DH040301",
                "DH040302",
                "DH040303"
            };
            foreach (var datayear in dataYears) { 
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
                            int value = indicatorOutput.Value;

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

                            _httpService.Post(_urlUpdate, null, oUDataItem);

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
                        var cDDHs = GetDanhSachDuLieu(month, year, page);
                        if (cDDHs is null || cDDHs.Count <= 0) break;

                        _httpService.Post(_urlAdd, null, cDDHs);
                        if (cDDHs.Count < _limit) break;
                        page++;
                    }
                }
            }
        }

        public ICollection<CDDHViewModel> GetDanhSachDuLieu(int? page)
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

            var result = JsonConvert.DeserializeObject<APIResult<CDDHViewModel>>(rs);
            return result.data;
        }

        public ICollection<CDDHViewModel> GetDanhSachDuLieu(int month, int year, int? page)
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

            var result = JsonConvert.DeserializeObject<APIResult<CDDHViewModel>>(rs);
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

                            _httpService.Post(_urlUpdate, null, new OUDataItem
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
