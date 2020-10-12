using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class DonViGiaoDucService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        string urlGetDonViGiaoDuc;
        string urlCreateDonViGiaoDuc;

        public DonViGiaoDucService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDonViGiaoDuc = _config.GetSection("VinhLong:GetDonViGiaoDuc:url").Value;
            urlCreateDonViGiaoDuc = _config.GetSection("VinhLong:CreateDonViGiaoDuc:url").Value;
        }

        public List<DonViGiaoDuc> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetDonViGiaoDuc, null, input);

            List<DonViGiaoDuc> result = JsonConvert.DeserializeObject<List<DonViGiaoDuc>>(rs);

            return result;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetDonViGiaoDuc, null, input);
            List<DonViGiaoDuc> result = JsonConvert.DeserializeObject<List<DonViGiaoDuc>>(rs);
            var length = result.Count;

            int count = 0;
            int countAll = 0;
            List<DonViGiaoDuc> temp = new List<DonViGiaoDuc>();

            foreach (var item in result)
            {
                item.ID = countAll.ToString();
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDonViGiaoDuc, null, temp);
                    temp = new List<DonViGiaoDuc>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
