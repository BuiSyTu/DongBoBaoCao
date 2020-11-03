using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Interfaces;
using System;
using DongBoBaoCao.ViewModels;
using Newtonsoft.Json;

namespace DongBoBaoCao.Core.Services
{
    public class PAKNService : IPAKNService
    {
        private readonly IConfiguration _config;
        private readonly IHttpService _httpService;
        private readonly ILoginService _loginService;

        private readonly string _loginToken;

        private readonly string _urlGet;
        private readonly string _tokenGet;

        private readonly string _urlAdd;

        private readonly string _urlFilter;

        private readonly string _fromDate;
        private readonly string _toDate;
        private readonly int _limit;

        public PAKNService(IConfiguration config, IHttpService httpService, ILoginService loginService)
        {
            _config = config;
            _httpService = httpService;
            _loginService = loginService;

            _loginToken = _loginService.GetToken();

            _urlFilter = _config.GetSection("PAKN:filter:address").Value;

            _urlGet = _config.GetSection("PAKN:get:address").Value;
            _tokenGet = _config.GetSection("PAKN:get:bearToken").Value;

            _urlAdd = _config.GetSection("PAKN:add:address").Value;

            _fromDate = _config.GetSection("fromDate").Value;
            _toDate = _config.GetSection("toDate").Value;
            _limit = Convert.ToInt32(_config.GetSection("limit").Value);
        }

        public void AddChiTieuBaoCao()
        {
            var dataYears = new List<int> { 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var officeCodes = new List<string> {  "000-00-00-H40",
                                        "000-00-18-H40",
                                        "000-00-05-H40",
                                        "000-00-12-H40",
                                        "000-00-10-H40",
                                        "000-00-08-H40",
                                        "000-00-13-H40",
                                        "000-00-02-H40",
                                        "000-00-09-H40",
                                        "000-00-04-H40",
                                        "000-00-15-H40",
                                        "000-00-11-H40",
                                        "000-00-07-H40",
                                        "000-00-14-H40",
                                        "000-00-19-H40",
                                        "000-00-20-H40",
                                        "000-00-21-H40",
                                        "000-00-22-H40",
                                        "000-00-23-H40",
                                        "000-00-24-H40",
                                        "000-00-25-H40",
                                        "000-00-26-H40",
                                        "000-00-06-H40",
                                        "000-00-03-H40",
                                        "000-00-16-H40",
                                        "000-00-17-H40",
                                        "000-00-01-H40",
                                        "000-00-27-H40",
                                        "000-00-28-H40",
                                        "000-21-24-H40",
                                        "000-22-24-H40",
                                        "000-23-24-H40",
                                        "000-45-24-H40",
                                        "000-24-24-H40",
                                        "000-25-24-H40",
                                        "000-44-24-H40",
                                        "000-26-24-H40",
                                        "000-43-24-H40",
                                        "000-27-24-H40",
                                        "000-41-24-H40",
                                        "000-28-24-H40",
                                        "000-42-24-H40",
                                        "000-29-24-H40",
                                        "000-40-24-H40",
                                        "000-30-24-H40",
                                        "000-31-24-H40",
                                        "000-39-24-H40",
                                        "000-32-24-H40",
                                        "000-33-24-H40",
                                        "000-34-24-H40",
                                        "000-35-24-H40",
                                        "000-36-24-H40",
                                        "000-37-24-H40",
                                        "000-38-24-H40",
                                        "000-18-19-H40",
                                        "000-16-19-H40",
                                        "000-17-19-H40",
                                        "000-19-19-H40",
                                        "000-20-19-H40",
                                        "000-21-19-H40",
                                        "000-22-19-H40",
                                        "000-23-19-H40",
                                        "000-24-19-H40",
                                        "000-25-19-H40",
                                        "000-26-19-H40",
                                        "000-27-19-H40",
                                        "000-28-19-H40",
                                        "000-29-19-H40",
                                        "000-30-19-H40",
                                        "000-16-20-H40",
                                        "000-17-20-H40",
                                        "000-31-19-H40",
                                        "000-32-19-H40",
                                        "000-33-19-H40",
                                        "000-34-19-H40",
                                        "000-35-19-H40",
                                        "000-36-19-H40",
                                        "000-37-19-H40",
                                        "000-17-28-H40",
                                        "000-18-20-H40",
                                        "000-18-28-H40",
                                        "000-19-20-H40",
                                        "000-19-28-H40",
                                        "000-20-20-H40",
                                        "000-20-28-H40",
                                        "000-21-20-H40",
                                        "000-21-28-H40",
                                        "000-22-20-H40",
                                        "000-22-28-H40",
                                        "000-23-20-H40",
                                        "000-23-28-H40",
                                        "000-24-20-H40",
                                        "000-24-28-H40",
                                        "000-25-28-H40",
                                        "000-26-28-H40",
                                        "000-25-20-H40",
                                        "000-27-28-H40",
                                        "000-26-20-H40",
                                        "000-27-20-H40",
                                        "000-28-20-H40",
                                        "000-28-28-H40",
                                        "000-29-28-H40",
                                        "000-29-20-H40",
                                        "000-30-20-H40",
                                        "000-30-28-H40",
                                        "000-31-20-H40",
                                        "000-32-20-H40",
                                        "000-31-28-H40",
                                        "000-33-20-H40",
                                        "000-34-20-H40",
                                        "000-32-28-H40",
                                        "000-35-20-H40",
                                        "000-33-28-H40",
                                        "000-36-20-H40",
                                        "000-37-20-H40",
                                        "000-34-28-H40",
                                        "000-38-20-H40",
                                        "000-35-28-H40",
                                        "000-36-28-H40",
                                        "000-39-20-H40",
                                        "000-40-20-H40",
                                        "000-37-28-H40",
                                        "000-41-20-H40",
                                        "000-38-28-H40",
                                        "000-42-20-H40",
                                        "000-39-28-H40",
                                        "000-43-20-H40",
                                        "000-40-28-H40",
                                        "000-41-28-H40",
                                        "000-44-20-H40",
                                        "000-42-28-H40",
                                        "000-45-20-H40",
                                        "000-46-20-H40",
                                        "000-43-28-H40",
                                        "000-47-20-H40",
                                        "000-44-28-H40",
                                        "000-45-28-H40",
                                        "000-48-20-H40",
                                        "000-49-20-H40",
                                        "000-50-20-H40",
                                        "000-46-28-H40",
                                        "000-16-21-H40",
                                        "000-47-28-H40",
                                        "000-48-28-H40",
                                        "000-17-21-H40",
                                        "000-18-21-H40",
                                        "000-16-27-H40",
                                        "000-19-21-H40",
                                        "000-17-27-H40",
                                        "000-18-27-H40",
                                        "000-19-27-H40",
                                        "000-20-27-H40",
                                        "000-20-21-H40",
                                        "000-21-27-H40",
                                        "000 21-21-H40",
                                        "000-22-27-H40",
                                        "000-23-27-H40",
                                        "000-22-21-H40",
                                        "000-24-27-H40",
                                        "000-23-21-H40",
                                        "000-24-21-H40",
                                        "000-25-27-H40",
                                        "000-25-21-H40",
                                        "000-26-27-H40",
                                        "000-27-27-H40",
                                        "000-28-27-H40",
                                        "000-29-27-H40",
                                        "000-26-21-H40",
                                        "000-16-22-H40",
                                        "000-17-22-H40",
                                        "000-18-22-H40",
                                        "000-19-22-H40",
                                        "000-20-22-H40",
                                        "000-21-22-H40",
                                        "000-22-22-H40",
                                        "000-23-22-H40",
                                        "000-24-22-H40",
                                        "000-25-22-H40",
                                        "000-26-22-H40",
                                        "000-27-22-H40",
                                        "000-28-22-H40",
                                        "000-29-22-H40",
                                        "000-30-22-H40",
                                        "000-31-22-H40",
                                        "000-32-22-H40",
                                        "000-33-22-H40",
                                        "000-34-22-H40",
                                        "000-35-22-H40",
                                        "000-30-27-H40",
                                        "000-31-27-H40",
                                        "000-32-27-H40",
                                        "000-33-27-H40",
                                        "000-34-27-H40",
                                        "000-35-27-H40",
                                        "000-16-26-H40",
                                        "000-17-26-H40",
                                        "000-18-26-H40",
                                        "000-16-23-H40",
                                        "000-18-23-H40",
                                        "000-19-23-H40",
                                        "000-19-26-H40",
                                        "000-20-23-H40",
                                        "000-20-26-H40",
                                        "000-21-23-H40",
                                        "000-22-23-H40",
                                        "000-23-23-H40",
                                        "000-21-26-H40",
                                        "000-24-23-H40",
                                        "000-22-26-H40",
                                        "000-23-26-H40",
                                        "000-17-23-H40",
                                        "000-24-26-H40",
                                        "000-25-23-H40",
                                        "000-26-23-H40",
                                        "000-27-23-H40",
                                        "000-25-26-H40",
                                        "000-28-23-H40",
                                        "000-29-23-H40",
                                        "000-30-23-H40",
                                        "000-31-23-H40",
                                        "000-32-23-H40",
                                        "000-33-23-H40",
                                        "000-34-23-H40",
                                        "000-35-23-H40",
                                        "000-36-23-H40",
                                        "000-37-23-H40",
                                        "000-38-23-H40",
                                        "000-39-23-H40",
                                        "000-40-23-H40",
                                        "000-17-25-H40",
                                        "000-18-25-H40",
                                        "000-20-25-H40",
                                        "000-21-25-H40",
                                        "000-24-25-H40",
                                        "000-25-25-H40",
                                        "000-26-25-H40",
                                        "000-27-25-H40",
                                        "000-28-25-H40",
                                        "000-29-25-H40",
                                        "000-30-25-H40",
                                        "000-31-25-H40",
                                        "000-32-25-H40",
                                        "000-33-25-H40",
                                        "000-34-25-H40",
                                        "000-35-25-H40",
                                        "000-36-25-H40",
                                        "000-37-25-H40",
                                        "000-22-25-H40",
                                        "000-23-25-H40",
                                        "000-26-26-H40",
                                        "000-27-26-H40",
                                        "000-28-26-H40",
                                        "000-29-26-H40",
                                        "000-30-26-H40",
                                        "000-31-26-H40",
                                        "000-32-26-H40",
                                        "000-33-26-H40",
                                        "000-19-25-H40",
                                        "95939a39-8d61-4459-8439-46c86bb6b8c8",
                                        "cb6942cc-632a-4a91-a3c4-6eb8b567192e",
                                        "2396066e-f2d6-4cd2-8adc-645259dbb6ee",
                                        "af8ff4fb-c5c7-4eb0-953e-a9823bb392ec",
                                        "64f7bf00-3264-4746-bcea-076828089848",
                                        "18ea754f-ae93-44f2-84df-7d8e14495ef1",
                                        "b01040bb-e660-4238-bbc5-ae0b9b93dca5",
                                        "021dcdb0-1d45-449c-b169-f54d9d10558b",
                                        "cc91bc85-fcc0-4d20-a2e3-3a3aa6f386f3",
                                        "17be116b-90b9-4b66-967e-779d070eb0da",
                                        "dc075056-41c3-4022-8745-9f9d1761269b",
                                        "eae4b15e-776a-4337-af83-82e19a5d2ae1",
                                        "7d122b67-e32a-4c61-90cc-cbb483d012e1",
                                        "b118de77-31f4-4ad1-9dae-b9f40e09cb7d",
                                        "48e56e9a-55c8-4a3f-b784-cef96516ad5a",
                                        "cfa3b600-a033-441a-9cb8-7019b872d68b",
                                        "f4e19584-2265-4ed5-9a3a-644da2f58c66",
                                        "4a6134bb-5c90-4b79-8e4d-9b3d78a80b72",
                                        "4E7A5828-909B-A57E-A3FF-5B0C767F2B3C",
                                        "4ef240b0-9e71-4782-b6f2-3768c53f60b8" };
            var indicatorCodes = new List<string> { "DH03010101", "DH03010102", "DH03010103", "DH03010104", "DH03010201", "DH03010202", "DH03010203", "DH03010204", "DH03010205", "DH03010206", "DH03010207", "DH03010208", "DH03010209", "DH03010210", "DH03010301", "DH03010302", "DH03010401", "DH03010402", "DH03010403", "DH030301", "DH030302", "DH030303" };
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

        public void CreateDanhSachDuLieu()
        {
            int page = 1;

            while (true)
            {
                ICollection<PAKNViewModel> dVCs = GetDanhSachDuLieu(page);
                if (dVCs is null || dVCs.Count <= 0) break;

                _httpService.Post(_urlAdd, null, dVCs);
                if (dVCs.Count < _limit) break;
                page++;
            }
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
