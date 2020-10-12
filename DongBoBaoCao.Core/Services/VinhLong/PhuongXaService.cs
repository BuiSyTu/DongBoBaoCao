using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class PhuongXaService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetPhuongXa;
        string urlCreatePhuongXa;

        public PhuongXaService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetPhuongXa = _config.GetSection("VinhLong:GetPhuongXa:url").Value;
            urlCreatePhuongXa = _config.GetSection("VinhLong:CreatePhuongXa:url").Value;
        }

        public List<PhuongXa> Get()
        {
            var input = new InputQuanHuyen
            {
                maQuanHuyen = ""
            };

            var rs = _httpService.PostVinhLong(urlGetPhuongXa, null, input);

            List<PhuongXa> result = JsonConvert.DeserializeObject<List<PhuongXa>>(rs);

            return result;
        }

        public int Create()
        {
            var input = new InputQuanHuyen
            {
                maQuanHuyen = ""
            };

            var rs = _httpService.PostVinhLong(urlGetPhuongXa, null, input);

            List<PhuongXa> result = JsonConvert.DeserializeObject<List<PhuongXa>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<PhuongXa> temp = new List<PhuongXa>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreatePhuongXa, null, temp);
                    temp = new List<PhuongXa>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
