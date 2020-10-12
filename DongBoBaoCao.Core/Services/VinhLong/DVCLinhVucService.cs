using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class DVCLinhVucService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetDVCLinhVuc;
        string urlCreateDVCLinhVuc;

        public DVCLinhVucService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDVCLinhVuc = _config.GetSection("VinhLong:GetDVCLinhVuc:url").Value;
            urlCreateDVCLinhVuc = _config.GetSection("VinhLong:CreateDVCLinhVuc:url").Value;
        }

        public List<DVCLinhVuc> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCLinhVuc, null, input);

            ApiDVCLinhVuc result = JsonConvert.DeserializeObject<ApiDVCLinhVuc>(rs);

            var items = result.items;

            return items;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCLinhVuc, null, input);

            ApiDVCLinhVuc result = JsonConvert.DeserializeObject<ApiDVCLinhVuc>(rs);
            var items = result.items;

            var length = items.Count;

            int count = 0;
            int countAll = 0;
            List<DVCLinhVuc> temp = new List<DVCLinhVuc>();

            foreach (var item in items)
            {
                item.ID = countAll.ToString();
                item.Ma = item.MaLinhVuc;
                item.Ten = item.TenLinhVuc;
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDVCLinhVuc, null, temp);
                    temp = new List<DVCLinhVuc>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
