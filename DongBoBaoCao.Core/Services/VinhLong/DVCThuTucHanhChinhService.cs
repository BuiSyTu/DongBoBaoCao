using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class DVCThuTucHanhChinhService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetDVCThuTucHanhChinh;
        string urlCreateDVCThuTucHanhChinh;

        public DVCThuTucHanhChinhService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDVCThuTucHanhChinh = _config.GetSection("VinhLong:GetDVCThuTucHanhChinh:url").Value;
            urlCreateDVCThuTucHanhChinh = _config.GetSection("VinhLong:CreateDVCThuTucHanhChinh:url").Value;
        }

        public List<DVCThuTucHanhChinh> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCThuTucHanhChinh, null, input);

            ApiDVCThuTucHanhChinh result = JsonConvert.DeserializeObject<ApiDVCThuTucHanhChinh>(rs);

            var items = result.items;

            return items;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCThuTucHanhChinh, null, input);

            ApiDVCThuTucHanhChinh result = JsonConvert.DeserializeObject<ApiDVCThuTucHanhChinh>(rs);
            var items = result.items;

            var length = items.Count;

            int count = 0;
            int countAll = 0;
            List<DVCThuTucHanhChinh> temp = new List<DVCThuTucHanhChinh>();

            foreach (var item in items)
            {
                item.ID = countAll.ToString();
                item.Ma = item.MaThuTuc;
                item.Ten = item.TenThuTuc;
                item.LinhVuc = item.TenLinhVuc;
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDVCThuTucHanhChinh, null, temp);
                    temp = new List<DVCThuTucHanhChinh>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
