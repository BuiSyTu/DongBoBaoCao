using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class DVCHoSoService: IDVCHoSoService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetDVCHoSo;
        string urlCreateDVCHoSo;

        public DVCHoSoService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDVCHoSo = _config.GetSection("VinhLong:GetDVCHoSo:url").Value;
            urlCreateDVCHoSo = _config.GetSection("VinhLong:CreateDVCHoSo:url").Value;
        }

        public List<DVCHoSo> Get()
        {
            var rs = _httpService.GetVinhLong(urlGetDVCHoSo, null, null);

            ApiDVCHoSo result = JsonConvert.DeserializeObject<ApiDVCHoSo>(rs);

            var items = result.items;

            return items;
        }

        public int Create()
        {
            int pageNumber = 0;
            int pageSize = 1000;

            while (true)
            {
                var rs = _httpService.GetVinhLong($"{urlGetDVCHoSo}?pageNumber={pageNumber}&pageSize={pageSize}", null, null);
                ApiDVCHoSo result = JsonConvert.DeserializeObject<ApiDVCHoSo>(rs);

                var items = result?.items;
                if (items is null || items.Count == 0) break;
                pageNumber++;

                var length = items.Count;

                int count = 0;
                int countAll = 0;
                List<DVCHoSo> temp = new List<DVCHoSo>();

                foreach (var item in items)
                {
                    item.ID = item.HoSoID;
                    temp.Add(item);
                    count++;
                    countAll++;

                    if (count == 50 || countAll == length)
                    {
                        _httpService.Post(urlCreateDVCHoSo, null, temp);
                        temp = new List<DVCHoSo>();
                        count = 0;
                    }
                }

                
            }

            return 0;
        }

        public bool? Delete()
        {
            throw new System.NotImplementedException();
        }

        public int DeleteAndCreateNew()
        {
            throw new System.NotImplementedException();
        }

        public void ThemChiTieuBaoCao()
        {
           
            var result = _httpService.Get("https://baocao.hanhchinhcong.net/_vti_bin/td.bcdh.vinhlong/bcdhvinhlongservice.svc/GetTongSoHoSoTheoNam?nam=2021", null, null);
            List<ChiTieu> lstHoSo = JsonConvert.DeserializeObject<List<ChiTieu>>(result);
            foreach (var item in lstHoSo)
            {
                Console.WriteLine(item.MaChiTieu + ":" + item.GiaTri);
            }
            
            foreach (var hoSo in lstHoSo)
            {   

                var rs = _httpService.Post("https://baocao.vinhlong.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDonVi", null, new OUDataItem
                {
                    dataTypeId = 3, // Thực hiện
                    dataYear = 2021,
                    indicatorCode = hoSo.MaChiTieu,
                    officeCode = "000-00-00-H61",
                    periodId = 1,
                    value = hoSo.GiaTri,
                    textValue = hoSo.GiaTri.ToString()
                });
                Console.WriteLine(rs);
            };

        }
    }
}
