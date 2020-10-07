using Microsoft.Extensions.Configuration;
using DongBoBaoCao.Core.Interfaces;
using System.Collections.Generic;
using DongBoBaoCao.Core.ViewModels;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace DongBoBaoCao.Core.Services
{
    public class QLVBService : IQLVBService
    {
        private readonly IConfiguration _config;
        private readonly ICommonService _commonService;
        private readonly IDateTimeService _dateTimeService;
        private readonly IHttpService _httpService;

        private readonly string _baseAddress;
        private readonly string _bearToken;

        public List<Indicator> Init()
        {
            Random random = new Random();
            var indicators = new List<Indicator>();

            var DH01 = new Indicator
            {
                Code = "DH01",
                value = 2000 + random.Next(-1500, 1500),              
            };
            indicators.Add(DH01);

            var DH0101 = new Indicator
            {
                Code = "DH0101",
                value = DH01.value
            };
            indicators.Add(DH0101);

            var DH010101 = new Indicator
            {
                Code = "DH010101",
                value = DH0101.value
            };
            indicators.Add(DH010101);

            var DH01010101 = new Indicator
            {
                Code = "DH01010101",
                value = DH010101.value * (85 + random.Next(-5, 5)) / 100
            };
            indicators.Add(DH01010101);

            var DH01010102 = new Indicator
            {
                Code = "DH01010102",
                value = DH010101.value - DH01010101.value
            };
            indicators.Add(DH01010102);

            var DH010102 = new Indicator
            {
                Code = "DH010102",
                value = DH0101.value
            };
            indicators.Add(DH010102);

            var DH01010201 = new Indicator
            {
                Code = "DH01010201",
                value = DH010102.value * (5 + random.Next(-2, 2)) / 100
            };
            indicators.Add(DH01010201);

            var DH01010202 = new Indicator
            {
                Code = "DH01010202",
                value = DH010102.value * (15 + random.Next(-3, 3)) / 100
            };
            indicators.Add(DH01010202);

            var DH01010203 = new Indicator
            {
                Code = "DH01010203",
                value = DH010102.value * (5 + random.Next(-2, 2)) / 100
            };
            indicators.Add(DH01010203);

            var DH01010205 = new Indicator
            {
                Code = "DH01010205",
                value = DH010102.value * (10 + random.Next(-2, 2)) / 100
            };
            indicators.Add(DH01010205);

            var DH01010204 = new Indicator
            {
                Code = "DH01010204",
                value = DH010102.value - DH01010201.value - DH01010202.value - DH01010203.value - DH01010205.value
            };
            indicators.Add(DH01010204);

            var DH010103 = new Indicator
            {
                Code = "DH010103",
                value = DH0101.value
            };
            indicators.Add(DH010103);

            var DH01010301 = new Indicator
            {
                Code = "DH01010301",
                value = DH010103.value * (20 + random.Next(-3, 3)) / 100
            };
            indicators.Add(DH01010301);

            var DH01010303 = new Indicator
            {
                Code = "DH01010303",
                value = DH010103.value * (20 + random.Next(-3, 3)) / 100
            };
            indicators.Add(DH01010303);

            var DH01010302 = new Indicator
            {
                Code = "DH01010302",
                value = DH010103.value - DH01010301.value - DH01010303.value
            };
            indicators.Add(DH01010302);

            var DH010104 = new Indicator
            {
                Code = "DH01010302",
                value = DH0101.value
            };
            indicators.Add(DH010104);

            var DH01010401 = new Indicator
            {
                Code = "DH01010401",
                value = DH010104.value * (2 + random.Next(-2, 2)) / 100
            };
            indicators.Add(DH01010401);

            var DH01010402 = new Indicator
            {
                Code = "DH01010401",
                value = DH010104.value - DH01010401.value
            };
            indicators.Add(DH01010402);

            var DH0102 = new Indicator
            {
                Code = "DH0102",
                value = DH01.value
            };
            indicators.Add(DH0102);

            var DH010201 = new Indicator
            {
                Code = "DH010201",
                value = DH0102.value * (95 + random.Next(-5, 5)) / 100
            };
            indicators.Add(DH010201);

            var DH010202 = new Indicator
            {
                Code = "DH010202",
                value = DH0102.value * (80 + random.Next(-5, 5)) / 100
            };
            indicators.Add(DH010202);

            var DH01020201 = new Indicator
            {
                Code = "DH01020201",
                value = DH010202.value * (90 + random.Next(-5, 5)) / 100
            };
            indicators.Add(DH01020201);

            var DH01020202 = new Indicator
            {
                Code = "DH01020202",
                value = DH010202.value * (20 + random.Next(-3, 3)) / 100
            };
            indicators.Add(DH01020202);

            var DH010203 = new Indicator
            {
                Code = "DH010203",
                value = DH0102.value * (98 + random.Next(-2, 2)) / 100
            };
            indicators.Add(DH010203);

            var DH010204 = new Indicator
            {
                Code = "DH010204",
                value = DH0102.value * (60 + random.Next(-10, 10)) / 100
            };
            indicators.Add(DH010204);

            var DH010205 = new Indicator
            {
                Code = "DH010205",
                value = DH0102.value * (70 + random.Next(-10, 10)) / 100
            };
            indicators.Add(DH010205);

            return indicators;
        }

        public QLVBService(IConfiguration config, ICommonService commonService, IDateTimeService dateTimeService, IHttpService httpService)
        {
            _config = config;
            _commonService = commonService;
            _dateTimeService = dateTimeService;
            _httpService = httpService;

            _baseAddress = _config.GetSection("QLVB:baseAddress").Value;
            _bearToken = _config.GetSection("QLVB:bearToken").Value;
        }

        public void AddChiTieuBaoCao()
        {
            var listOuDataItem = new List<OUDataItem>();
            var dataYears = new List<int> { 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };


            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var officeCodes = new List<string> { "000-00-12-H40", "000-00-19-H40", "000-00-20-H40" };
            var indicatorCodes = new List<string> { "DH01010201", "DH01010202", "DH01010203", "DH01010204", "DH01010205" };
            var lstfields = new List<string> { "TrangThaiChung", "TrangThaiChung", "TrangThaiChung", "TrangThaiChung", "TrangThaiChung" };
            var lstvalues = new List<string> { "01", "02##04", "02##03", "03##04", "03##03" };

            var chartInputs = new List<ChartInput>();
            
            for (var i = 0; i < officeCodes.Count; i++)
            {
                var officeCode = officeCodes[i];
                for (var j = 0; j < indicatorCodes.Count; j++)
                {
                    var indicatorCode = indicatorCodes[j];
                    var fields = lstfields[j];
                    var values = lstvalues[j];
                    for (var k = 0; k < dataYears.Count; k++)
                    {
                        var year = dataYears[k];

                        for (var l = 0; l < months.Count; l++)
                        {
                            var month = months[l];
                            var periodId = periodIds[l];

                            var rs = _httpService.Post("https://baocao.hanhchinhcong.net/_vti_bin/td.bcdh/bcdhservice.svc/Loc1", null, new CharInputByMonth
                            {
                                fields = fields,
                                maDonVi = officeCode,
                                maPhanMem = "QLVB",
                                month = month,
                                year = year,
                                values = values
                            });

                            ChartOutput output = JsonConvert.DeserializeObject<ChartOutput>(rs);

                            var value = output.data[0].value;
                            var textValue = value.ToString();

                            var rs1 = _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, new OUDataItem
                            {
                                dataTypeId = 3, // Thực hiện
                                dataYear = year,
                                indicatorCode = indicatorCode,
                                officeCode = officeCode,
                                periodId = periodId,
                                value = value,
                                textValue = textValue
                            });
                        }
                    }
                }
            }

            //_commonService.AddOrUpdateIndicator();
        }

        public void AddChiTieuBaoCao1()
        {
            var dataYears = new List<int> { 2016, 2017, 2018, 2019, 2020 };
            var months = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var periodIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var officeCodes = new List<string> { "000-00-19-H40", "000-00-20-H40", "000-00-21-H40", "000-00-22-H40", "000-00-23-H40", "000-00-25-H40", "000-00-26-H40", "000-00-27-H40", "000-00-28-H40" };
            var officeCode1s = new List<string> {"000-00-02-H40","000-00-03-H40","000-00-04-H40","000-00-05-H40","000-00-06-H40","000-00-07-H40","000-00-08-H40","000-00-09-H40","000-00-10-H40","000-00-11-H40","000-00-13-H40","000-00-14-H40","000-00-15-H40","000-00-16-H40","000-00-17-H40","000-00-18-H40" };

            for (var i = 0; i < dataYears.Count; i++)
            {
                var datayear = dataYears[i];
                for (var j = 0; j < months.Count; j++)
                {
                    var month = months[j];
                    var periodId = periodIds[j];
                    for (var k = 0; k < officeCodes.Count; k++)
                    {
                        var officeCode = officeCodes[k];

                        Random random = new Random();
                        var indicators = new List<Indicator>();

                        var DH01 = new Indicator
                        {
                            Code = "DH01",
                            value = 2000 + random.Next(-1500, 1500),
                        };
                        indicators.Add(DH01);

                        var DH0101 = new Indicator
                        {
                            Code = "DH0101",
                            value = DH01.value
                        };
                        indicators.Add(DH0101);

                        var DH010101 = new Indicator
                        {
                            Code = "DH010101",
                            value = DH0101.value
                        };
                        indicators.Add(DH010101);

                        var DH01010101 = new Indicator
                        {
                            Code = "DH01010101",
                            value = DH010101.value * (85 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH01010101);

                        var DH01010102 = new Indicator
                        {
                            Code = "DH01010102",
                            value = DH010101.value - DH01010101.value
                        };
                        indicators.Add(DH01010102);

                        var DH010102 = new Indicator
                        {
                            Code = "DH010102",
                            value = DH0101.value
                        };
                        indicators.Add(DH010102);

                        var DH01010201 = new Indicator
                        {
                            Code = "DH01010201",
                            value = DH010102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010201);

                        var DH01010202 = new Indicator
                        {
                            Code = "DH01010202",
                            value = DH010102.value * (15 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01010202);

                        var DH01010203 = new Indicator
                        {
                            Code = "DH01010203",
                            value = DH010102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010203);

                        var DH01010205 = new Indicator
                        {
                            Code = "DH01010205",
                            value = DH010102.value * (10 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010205);

                        var DH01010204 = new Indicator
                        {
                            Code = "DH01010204",
                            value = DH010102.value - DH01010201.value - DH01010202.value - DH01010203.value - DH01010205.value
                        };
                        indicators.Add(DH01010204);

                        var DH010103 = new Indicator
                        {
                            Code = "DH010103",
                            value = DH0101.value
                        };
                        indicators.Add(DH010103);

                        var DH01010301 = new Indicator
                        {
                            Code = "DH01010301",
                            value = DH010103.value * (20 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01010301);

                        var DH01010303 = new Indicator
                        {
                            Code = "DH01010303",
                            value = DH010103.value * (20 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01010303);

                        var DH01010302 = new Indicator
                        {
                            Code = "DH01010302",
                            value = DH010103.value - DH01010301.value - DH01010303.value
                        };
                        indicators.Add(DH01010302);

                        var DH010104 = new Indicator
                        {
                            Code = "DH01010302",
                            value = DH0101.value
                        };
                        indicators.Add(DH010104);

                        var DH01010401 = new Indicator
                        {
                            Code = "DH01010401",
                            value = DH010104.value * (2 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010401);

                        var DH01010402 = new Indicator
                        {
                            Code = "DH01010401",
                            value = DH010104.value - DH01010401.value
                        };
                        indicators.Add(DH01010402);

                        var DH0102 = new Indicator
                        {
                            Code = "DH0102",
                            value = DH01.value
                        };
                        indicators.Add(DH0102);

                        var DH010201 = new Indicator
                        {
                            Code = "DH010201",
                            value = DH0102.value * (95 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH010201);

                        var DH010202 = new Indicator
                        {
                            Code = "DH010202",
                            value = DH0102.value * (80 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH010202);

                        var DH01020201 = new Indicator
                        {
                            Code = "DH01020201",
                            value = DH010202.value * (90 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH01020201);

                        var DH01020202 = new Indicator
                        {
                            Code = "DH01020202",
                            value = DH010202.value * (20 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01020202);

                        var DH010203 = new Indicator
                        {
                            Code = "DH010203",
                            value = DH0102.value * (98 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH010203);

                        var DH010204 = new Indicator
                        {
                            Code = "DH010204",
                            value = DH0102.value * (60 + random.Next(-10, 10)) / 100
                        };
                        indicators.Add(DH010204);

                        var DH010205 = new Indicator
                        {
                            Code = "DH010205",
                            value = DH0102.value * (70 + random.Next(-10, 10)) / 100
                        };
                        indicators.Add(DH010205);

                        for (var l = 0; l < indicators.Count; l++)
                        {
                            var indicator = indicators[l];

                            var rs1 = _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, new OUDataItem
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

            for (var i = 0; i < dataYears.Count; i++)
            {
                var datayear = dataYears[i];
                for (var j = 0; j < months.Count; j++)
                {
                    var month = months[j];
                    var periodId = periodIds[j];
                    for (var k = 0; k < officeCode1s.Count; k++)
                    {
                        var officeCode = officeCode1s[k];

                        Random random = new Random();
                        var indicators = new List<Indicator>();

                        var DH01 = new Indicator
                        {
                            Code = "DH01",
                            value = 1000 + random.Next(-400, 400),
                        };
                        indicators.Add(DH01);

                        var DH0101 = new Indicator
                        {
                            Code = "DH0101",
                            value = DH01.value
                        };
                        indicators.Add(DH0101);

                        var DH010101 = new Indicator
                        {
                            Code = "DH010101",
                            value = DH0101.value
                        };
                        indicators.Add(DH010101);

                        var DH01010101 = new Indicator
                        {
                            Code = "DH01010101",
                            value = DH010101.value * (85 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH01010101);

                        var DH01010102 = new Indicator
                        {
                            Code = "DH01010102",
                            value = DH010101.value - DH01010101.value
                        };
                        indicators.Add(DH01010102);

                        var DH010102 = new Indicator
                        {
                            Code = "DH010102",
                            value = DH0101.value
                        };
                        indicators.Add(DH010102);

                        var DH01010201 = new Indicator
                        {
                            Code = "DH01010201",
                            value = DH010102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010201);

                        var DH01010202 = new Indicator
                        {
                            Code = "DH01010202",
                            value = DH010102.value * (15 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01010202);

                        var DH01010203 = new Indicator
                        {
                            Code = "DH01010203",
                            value = DH010102.value * (5 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010203);

                        var DH01010205 = new Indicator
                        {
                            Code = "DH01010205",
                            value = DH010102.value * (10 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010205);

                        var DH01010204 = new Indicator
                        {
                            Code = "DH01010204",
                            value = DH010102.value - DH01010201.value - DH01010202.value - DH01010203.value - DH01010205.value
                        };
                        indicators.Add(DH01010204);

                        var DH010103 = new Indicator
                        {
                            Code = "DH010103",
                            value = DH0101.value
                        };
                        indicators.Add(DH010103);

                        var DH01010301 = new Indicator
                        {
                            Code = "DH01010301",
                            value = DH010103.value * (20 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01010301);

                        var DH01010303 = new Indicator
                        {
                            Code = "DH01010303",
                            value = DH010103.value * (20 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01010303);

                        var DH01010302 = new Indicator
                        {
                            Code = "DH01010302",
                            value = DH010103.value - DH01010301.value - DH01010303.value
                        };
                        indicators.Add(DH01010302);

                        var DH010104 = new Indicator
                        {
                            Code = "DH01010302",
                            value = DH0101.value
                        };
                        indicators.Add(DH010104);

                        var DH01010401 = new Indicator
                        {
                            Code = "DH01010401",
                            value = DH010104.value * (2 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH01010401);

                        var DH01010402 = new Indicator
                        {
                            Code = "DH01010401",
                            value = DH010104.value - DH01010401.value
                        };
                        indicators.Add(DH01010402);

                        var DH0102 = new Indicator
                        {
                            Code = "DH0102",
                            value = DH01.value
                        };
                        indicators.Add(DH0102);

                        var DH010201 = new Indicator
                        {
                            Code = "DH010201",
                            value = DH0102.value * (95 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH010201);

                        var DH010202 = new Indicator
                        {
                            Code = "DH010202",
                            value = DH0102.value * (80 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH010202);

                        var DH01020201 = new Indicator
                        {
                            Code = "DH01020201",
                            value = DH010202.value * (90 + random.Next(-5, 5)) / 100
                        };
                        indicators.Add(DH01020201);

                        var DH01020202 = new Indicator
                        {
                            Code = "DH01020202",
                            value = DH010202.value * (20 + random.Next(-3, 3)) / 100
                        };
                        indicators.Add(DH01020202);

                        var DH010203 = new Indicator
                        {
                            Code = "DH010203",
                            value = DH0102.value * (98 + random.Next(-2, 2)) / 100
                        };
                        indicators.Add(DH010203);

                        var DH010204 = new Indicator
                        {
                            Code = "DH010204",
                            value = DH0102.value * (60 + random.Next(-10, 10)) / 100
                        };
                        indicators.Add(DH010204);

                        var DH010205 = new Indicator
                        {
                            Code = "DH010205",
                            value = DH0102.value * (70 + random.Next(-10, 10)) / 100
                        };
                        indicators.Add(DH010205);

                        for (var l = 0; l < indicators.Count; l++)
                        {
                            var indicator = indicators[l];

                            var rs1 = _httpService.Post("https://baocao.namdinh.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, new OUDataItem
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

            var indicatorCodes = new List<string> { };
        }

        public int CreateDanhSachDuLieu()
        {
            int total = _commonService.CreateDanhSachDuLieu(_baseAddress, _bearToken);
            return total;
        }

        public async Task<int> CreateDanhSachDuLieuAsync()
        {
            int total = await _commonService.CreateDanhSachDuLieuAsync(_baseAddress, _bearToken);
            return total;
        }

        public int CreateDanhSachDuLieuTrongNgay()
        {
            int total = _commonService.CreateDanhSachDuLieuTrongNgay(_baseAddress, _bearToken);
            return total;
        }

        public async Task<int> CreateDanhSachDuLieuTrongNgayAsync()
        {
            int total = await _commonService.CreateDanhSachDuLieuTrongNgayAsync(_baseAddress, _bearToken);
            return total;
        }

        public ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            var result = _commonService.GetDanhSachDuLieu(baseAddress, danhSachDuLieu, bearToken, fromDate, toDate, page, limit);
            return result;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit)
        {
            var result = await _commonService.GetDanhSachDuLieuAsync(baseAddress, danhSachDuLieu, bearToken, fromDate, toDate, page, limit);
            return result;
        }

        public ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var result = _commonService.GetDanhSachDuLieuTrongNgay(baseAddress, danhSachDuLieuTrongNgay, bearToken, page, limit);
            return result;
        }

        public async Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit)
        {
            var result = await _commonService.GetDanhSachDuLieuTrongNgayAsync(baseAddress, danhSachDuLieuTrongNgay, bearToken, page, limit);
            return result;
        }

        public ChartOutput GetDuLieuLoc(ChartInput input)
        {
            var result = _commonService.GetDuLieuLoc(input);
            return result;
        }
    }
}
