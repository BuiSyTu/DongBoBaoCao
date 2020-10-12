using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class TinhThanhService: ITinhThanhService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetTinhThanh;
        string urlCreateTinhThanh;

        public TinhThanhService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetTinhThanh = _config.GetSection("VinhLong:GetTinhThanh:url").Value;
            urlCreateTinhThanh = _config.GetSection("VinhLong:CreateTinhThanh:url").Value;
        }

        public List<TinhThanh> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetTinhThanh, null, input);

            List<TinhThanh> result = JsonConvert.DeserializeObject<List<TinhThanh>>(rs);

            return result;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetTinhThanh, null, input);

            List<TinhThanh> result = JsonConvert.DeserializeObject<List<TinhThanh>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<TinhThanh> temp = new List<TinhThanh>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateTinhThanh, null, temp);
                    temp = new List<TinhThanh>();
                    count = 0;
                }
            }

            return countAll;
        }

        public bool? Delete()
        {
            throw new System.NotImplementedException();
        }

        public int DeleteAndCreateNew()
        {
            throw new System.NotImplementedException();
        }
    }
}
