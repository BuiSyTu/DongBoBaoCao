using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.Api.Controllers
{
    public class PAKNController : Controller
    {
        private IHttpService _httpService;
        public PAKNController(IHttpService httpService)
        {
            _httpService = httpService;
        }

        [HttpPost("chiTieu")]
        public IActionResult AddChiTieu(string officeCode = null)
        {
            int result = 0;
            var dataYears = new List<int> { 2020 };
            var months = new List<int> { 9, 10, 11 };
            var periodIds = new List<int> { 9, 10, 11 };

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
                "DH03010206",
                "DH03010207",
                "DH03010208",
                "DH03010209",
                "DH03010210",
                "DH03010301",
                "DH03010302",
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
                    foreach (var indicatorCode in indicatorCodes)
                    {
                        var indicatorInput = new IndicatorInput
                        {
                            IndicatorCode = indicatorCode,
                            Month = month,
                            OfficeCode = officeCode,
                            Year = datayear
                        };

                        string filterResult = _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bcdh/bcdhservice.svc/PAKN/Loc", null, indicatorInput);

                        if (string.IsNullOrEmpty(filterResult)) continue;

                        var indicatorOutput = JsonConvert.DeserializeObject<IndicatorOutput>(filterResult);
                        var value = indicatorOutput.Value;
                        if (value > 0) result++;

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
            return Ok(result);
        }
    }
}
