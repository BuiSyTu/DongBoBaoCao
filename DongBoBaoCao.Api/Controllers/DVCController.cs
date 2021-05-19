using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DongBoBaoCao.Api.Controllers
{
    public class DVCController : Controller
    {
        private IHttpService _httpService;

        public DVCController(IHttpService httpService)
        {
            _httpService = httpService;
        }

        [HttpGet("chiTieu")]
        public IActionResult AddChiTieu(string officeCode = null, int? month = 1, int? dataYear = 2020)
        {
            int result = 0;

            var indicatorCodes = new List<string> {
                "DH02010101",
                "DH02010102",
                "DH02010103",
                "DH02010104",
                "DH02010105",
                "DH02010201",
                "DH02010202",
                "DH02010203",
                "DH02010204",
                "DH02010301",
                "DH02010302",
                "DH02010303",
                "DH02010304",
                "DH02010305",
                "DH02010306",
                "DH02010401",
                "DH02010402",
                "DH02010501",
                "DH02010502",
                "DH020201",
                "DH020202",
                "DH020203"
            };

            var periodId = month;
            foreach (var indicatorCode in indicatorCodes)
            {
                var indicatorInput = new IndicatorInput
                {
                    IndicatorCode = indicatorCode,
                    Month = month.Value,
                    OfficeCode = officeCode,
                    Year = dataYear.Value
                };

                string filterResult = _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bcdh/bcdhservice.svc/DVC/Loc", null, indicatorInput);

                if (string.IsNullOrEmpty(filterResult)) continue;

                var indicatorOutput = JsonConvert.DeserializeObject<IndicatorOutput>(filterResult);
                int value = indicatorOutput.Value;

                if (value == 0) continue;

                result++;

                var oUDataItem = new OUDataItem
                {
                    dataTypeId = 3, // Thực hiện
                    dataYear = dataYear.Value,
                    indicatorCode = indicatorCode,
                    officeCode = officeCode,
                    periodId = periodId.Value,
                    value = value,
                    textValue = value.ToString()
                };

                _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, oUDataItem);

            }

            return Ok(result);
        }
    }
}
