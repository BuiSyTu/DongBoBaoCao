using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class DonViGiaoDucService: IDonViGiaoDucService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private readonly string urlGetDonViGiaoDuc;
        private readonly string urlCreateDonViGiaoDuc;
        private readonly string urlTruncateDonViGiaoDuc;

        public DonViGiaoDucService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDonViGiaoDuc = _config.GetSection("VinhLong:GetDonViGiaoDuc:url").Value;
            urlCreateDonViGiaoDuc = _config.GetSection("VinhLong:CreateDonViGiaoDuc:url").Value;
            urlTruncateDonViGiaoDuc = _config.GetSection("VinhLong:TruncateDonViGiaoDuc:url").Value;
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

        public bool? Delete()
        {
            var rs = _httpService.Delete(urlTruncateDonViGiaoDuc, null, null);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            bool result = JsonConvert.DeserializeObject<bool>(rs);
            return result;
        }

        public int DeleteAndCreateNew()
        {
            throw new System.NotImplementedException();
        }
    }
}
