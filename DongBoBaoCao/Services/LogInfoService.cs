using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.Services
{
    public class LogInfoService
    {
        private readonly IConfiguration _config;
        private readonly IHttpService _httpService;
        private readonly string _urlFilter;

        public LogInfoService(IConfiguration config, IHttpService httpService)
        {
            _config = config;
            _httpService = httpService;

            _urlFilter = _config.GetSection("LogInfo:filter:address").Value;
        }

        public void AddChiTieuBaoCao()
        {
            var dataYears = new List<int> { 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var officeCodes = new List<string> {
                "000-00-00-H40"
            };
            var indicatorCodes = new List<string> {
                "DH070201",
                "DH070202",
                "DH070401",
                "DH070402",
                "DH070403",
                "DH070404",
                "DH070405",
                "DH070406",
                "DH070407"
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

                            if (value == 0) continue;

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
    }
}
