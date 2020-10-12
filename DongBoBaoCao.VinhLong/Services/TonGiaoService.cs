using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class TonGiaoService: ITonGiaoService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetTonGiao;
        string urlCreateTonGiao;

        public TonGiaoService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetTonGiao = _config.GetSection("VinhLong:GetTonGiao:url").Value;
            urlCreateTonGiao = _config.GetSection("VinhLong:CreateTonGiao:url").Value;
        }

        public List<TonGiao> Get()
        {
            var rs = _httpService.PostVinhLong(urlGetTonGiao, null, null);

            List<TonGiao> result = JsonConvert.DeserializeObject<List<TonGiao>>(rs);

            return result;
        }

        public int Create()
        {
            var rs = _httpService.PostVinhLong(urlGetTonGiao, null, null);

            List<TonGiao> result = JsonConvert.DeserializeObject<List<TonGiao>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<TonGiao> temp = new List<TonGiao>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateTonGiao, null, temp);
                    temp = new List<TonGiao>();
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
