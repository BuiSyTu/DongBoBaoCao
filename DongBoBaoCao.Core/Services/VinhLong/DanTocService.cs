using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class DanTocService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetDanToc;
        string urlCreateDanToc;

        public DanTocService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDanToc = _config.GetSection("VinhLong:GetDanToc:url").Value;
            urlCreateDanToc = _config.GetSection("VinhLong:CreateDanToc:url").Value;
        }

        public List<DanToc> Get()
        {
            var rs = _httpService.PostVinhLong(urlGetDanToc, null, null);

            List<DanToc> result = JsonConvert.DeserializeObject<List<DanToc>>(rs);

            return result;
        }

        public int Create()
        {
            var rs = _httpService.PostVinhLong(urlGetDanToc, null, null);

            List<DanToc> result = JsonConvert.DeserializeObject<List<DanToc>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<DanToc> temp = new List<DanToc>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDanToc, null, temp);
                    temp = new List<DanToc>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
