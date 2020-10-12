using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class TruongHocService: ITruongHocService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private string urlGetTruongHoc;
        private string urlCreateTruongHoc;
        private string urlCreateQuanHuyen;
        private readonly string urlTruncateTruongHoc;

        public TruongHocService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetTruongHoc = _config.GetSection("VinhLong:GetTruongHoc:url").Value;
            urlCreateTruongHoc = _config.GetSection("VinhLong:CreateTruongHoc:url").Value;
            urlCreateQuanHuyen = _config.GetSection("VinhLong:CreateQuanHuyen:url").Value;
            urlTruncateTruongHoc = _config.GetSection("VinhLong:TruncateTruongHoc:url").Value;
        }

        public List<TruongHoc> Get()
        {
            var input = new InputTruongHoc
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetTruongHoc, null, input);

            List<TruongHoc> result = JsonConvert.DeserializeObject<List<TruongHoc>>(rs);

            return result;
        }

        public int Create()
        {
            var input = new InputTinhThanh
            {
                maTinhThanh = "86"
            };

            var rs = _httpService.PostVinhLong(urlGetTruongHoc, null, input);

            List<TruongHoc> result = JsonConvert.DeserializeObject<List<TruongHoc>>(rs);
            var length = result.Count;

            int id = 1;
            int count = 0;
            int countAll = 0;
            List<TruongHoc> temp = new List<TruongHoc>();

            foreach (var item in result)
            {
                item.ID = id.ToString();
                temp.Add(item);
                count++;
                countAll++;
                id++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateTruongHoc, null, temp);
                    temp = new List<TruongHoc>();
                    count = 0;
                }
            }

            return countAll;
        }

        public bool? Delete()
        {
            var rs = _httpService.Delete(urlTruncateTruongHoc, null, null);

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
