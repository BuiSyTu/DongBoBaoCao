using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class DVCHoSoService
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
    }
}
