using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.VinhLong.Services
{
    public class HCCDonViService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private readonly string urlGetHCCDonVi;
        private readonly string urlCreateHCCDonVi;

        public HCCDonViService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetHCCDonVi = _config.GetSection("VinhLong:GetHCCDonVi:url").Value;
            urlCreateHCCDonVi = _config.GetSection("VinhLong:CreateHCCDonVi:url").Value;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetHCCDonVi, null, input);

            ApiHCCDonVi result = JsonConvert.DeserializeObject<ApiHCCDonVi>(rs);
            var items = result.items;

            var length = items.Count;

            int id = 1;
            int count = 0;
            int countAll = 0;
            List<HCCDonVi> temp = new List<HCCDonVi>();

            foreach (var item in items)
            {
                item.Id = id.ToString();
                item.Ma = item.MaDonVi;
                item.Ten = item.TenDonVi;
                temp.Add(item);
                count++;
                countAll++;
                id++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateHCCDonVi, null, temp);
                    temp = new List<HCCDonVi>();
                    count = 0;
                }
            }

            return countAll;
        }
    }
}
