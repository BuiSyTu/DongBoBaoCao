using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class DVCDonViService: IDVCDonViService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private readonly string urlGetDVCDonVi;
        private readonly string urlCreateDVCDonVi;
        private readonly string urlTruncateDVCDonVi;

        public DVCDonViService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDVCDonVi = _config.GetSection("VinhLong:GetDVCDonVi:url").Value;
            urlCreateDVCDonVi = _config.GetSection("VinhLong:CreateDVCDonVi:url").Value;
            urlTruncateDVCDonVi = _config.GetSection("VinhLong:TruncateDVCDonVi:url").Value;
        }

        public List<DVCDonVi> Get()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCDonVi, null, input);

            ApiDVCDonVi result = JsonConvert.DeserializeObject<ApiDVCDonVi>(rs);

            var items = result.items;

            return items;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.GetVinhLong(urlGetDVCDonVi, null, input);

            ApiDVCDonVi result = JsonConvert.DeserializeObject<ApiDVCDonVi>(rs);
            var items = result.items;

            var length = items.Count;

            int count = 0;
            int countAll = 0;
            List<DVCDonVi> temp = new List<DVCDonVi>();

            foreach (var item in items)
            {
                item.ID = countAll.ToString();
                item.Ma = item.MaDonVi;
                item.Ten = item.TenDonVi;
                temp.Add(item);
                count++;
                countAll++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDVCDonVi, null, temp);
                    temp = new List<DVCDonVi>();
                    count = 0;
                }
            }

            return countAll;
        }

        public bool? Delete()
        {
            var rs = _httpService.Delete(urlTruncateDVCDonVi, null, null);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            bool result = JsonConvert.DeserializeObject<bool>(rs);
            return result;
        }

        public int DeleteAndCreateNew()
        {
            var checkDelete = Delete();
            int result = 0;

            if (checkDelete.Value)
            {
                result = Create();
            }

            return result;
        }
    }
}
