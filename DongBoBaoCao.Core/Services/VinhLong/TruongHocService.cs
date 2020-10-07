using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class TruongHocService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetTruongHoc;
        string urlCreateTruongHoc;

        public TruongHocService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetTruongHoc = _config.GetSection("VinhLong:GetTruongHoc:url").Value;
            urlCreateTruongHoc = _config.GetSection("VinhLong:CreateTruongHoc:url").Value;
        }

        public List<TruongHoc> Get()
        {
            var input = new Input
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetTruongHoc, null, input);

            List<TruongHoc> result = JsonConvert.DeserializeObject<List<TruongHoc>>(rs);

            return result;
        }

        public int Create()
        {
            var input = new Input
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetTruongHoc, null, input);

            List<TruongHoc> result = JsonConvert.DeserializeObject<List<TruongHoc>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<TruongHoc> temp = new List<TruongHoc>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateTruongHoc, null, temp);
                    temp = new List<TruongHoc>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
