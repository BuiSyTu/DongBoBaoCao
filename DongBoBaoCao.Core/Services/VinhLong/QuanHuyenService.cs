using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class QuanHuyenService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetQuanHuyen;
        string urlCreateQuanHuyen;

        public QuanHuyenService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetQuanHuyen = _config.GetSection("VinhLong:GetQuanHuyen:url").Value;
            urlCreateQuanHuyen = _config.GetSection("VinhLong:CreateQuanHuyen:url").Value;
        }

        public List<QuanHuyen> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetQuanHuyen, null, input);

            List<QuanHuyen> result = JsonConvert.DeserializeObject<List<QuanHuyen>>(rs);

            return result;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetQuanHuyen, null, input);

            List<QuanHuyen> result = JsonConvert.DeserializeObject<List<QuanHuyen>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<QuanHuyen> temp = new List<QuanHuyen>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateQuanHuyen, null, temp);
                    temp = new List<QuanHuyen>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
