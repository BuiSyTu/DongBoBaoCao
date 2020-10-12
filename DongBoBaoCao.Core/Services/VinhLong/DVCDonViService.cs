using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class DVCDonViService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetDVCDonVi;
        string urlCreateDVCDonVi;

        public DVCDonViService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDVCDonVi = _config.GetSection("VinhLong:GetDVCDonVi:url").Value;
            urlCreateDVCDonVi = _config.GetSection("VinhLong:CreateDVCDonVi:url").Value;
        }

        public List<DVCDonVi> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCDonVi, null, input);

            ApiDVCDonVi result = JsonConvert.DeserializeObject<ApiDVCDonVi>(rs);

            var items = result.items;

            return items;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCDonVi, null, input);

            ApiDVCDonVi result = JsonConvert.DeserializeObject<ApiDVCDonVi>(rs);
            var items = result.items;

            var length = items.Count;

            int count = 0;
            int countAll = 0;
            List<DVCDonVi> temp = new List<DVCDonVi>();

            foreach (var item in items)
            {
                item.ID = countAll.ToString();
                item.Ma = item.MaDonVi;
                item.Ten = item.TenDonVi;
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDVCDonVi, null, temp);
                    temp = new List<DVCDonVi>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
