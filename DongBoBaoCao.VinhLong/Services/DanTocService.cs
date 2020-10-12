using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class DanTocService: IDanTocService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private readonly string urlGetDanToc;
        private readonly string urlCreateDanToc;
        private readonly string urlTruncateDanToc;

        public DanTocService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetDanToc = _config.GetSection("VinhLong:GetDanToc:url").Value;
            urlCreateDanToc = _config.GetSection("VinhLong:CreateDanToc:url").Value;
            urlTruncateDanToc = _config.GetSection("VinhLong:TruncateDanToc:url").Value;
        }

        public List<DanToc> Get()
        {
            var rs = _httpService.PostVinhLong(urlGetDanToc, null, null);

            List<DanToc> result = JsonConvert.DeserializeObject<List<DanToc>>(rs);

            return result;
        }

        public int Create()
        {
            var rs = _httpService.PostVinhLong(urlGetDanToc, null, null);

            List<DanToc> result = JsonConvert.DeserializeObject<List<DanToc>>(rs);
            var length = result.Count;

            int id = 1;
            int count = 0;
            int countAll = 0;
            List<DanToc> temp = new List<DanToc>();

            foreach (var item in result)
            {
                item.ID = id.ToString();
                temp.Add(item);
                count++;
                countAll++;
                id++;

                if (count == 50 || countAll == length)
                {
                    _httpService.Post(urlCreateDanToc, null, temp);
                    temp = new List<DanToc>();
                    count = 0;
                }
            }

            return countAll;
        }

        public bool? Delete()
        {
            var rs = _httpService.Delete(urlTruncateDanToc, null, null);

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
