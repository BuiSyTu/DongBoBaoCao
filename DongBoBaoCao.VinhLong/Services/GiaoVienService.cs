using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class GiaoVienService: IGiaoVienService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private string urlGetGiaoVien;
        private string urlCreateGiaoVien;
        private string urlCreateTruongHoc;


        public GiaoVienService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetGiaoVien = _config.GetSection("VinhLong:GetGiaoVien:url").Value;
            urlCreateGiaoVien = _config.GetSection("VinhLong:CreateGiaoVien:url").Value;
            urlCreateTruongHoc = _config.GetSection("VinhLong:CreateTruongHoc:url").Value;
        }

        public List<GiaoVien> Get()
        {
            var lstNamHoc = new List<int>() { 2019, 2020 };

            var rsTruongHoc = _httpService.Get(urlCreateTruongHoc, null, null);
            List<TruongHoc> lstTruongHoc = JsonConvert.DeserializeObject<List<TruongHoc>>(rsTruongHoc);

            List<InputGiaoVien> lstInput = new List<InputGiaoVien>();

            foreach (var truongHoc in lstTruongHoc)
            {
                foreach (var namHoc in lstNamHoc)
                {
                    var input = new InputGiaoVien
                    {
                        offset = 0,
                        limit = 99999,
                        maTruongHoc = truongHoc.MaTruongHoc,
                        namHoc = namHoc
                    };

                    lstInput.Add(input);
                }
            }

            var lstGiaoVien = new List<GiaoVien>();

            foreach (var input in lstInput)
            {
                var rs = _httpService.PostVinhLong(urlGetGiaoVien, null, null);

                List<GiaoVien> result = JsonConvert.DeserializeObject<List<GiaoVien>>(rs);
                lstGiaoVien.AddRange(result);
            }

            return lstGiaoVien;
        }

        public int Create()
        {
            var lstNamHoc = new List<int>() { 2019, 2020 };

            var rsTruongHoc = _httpService.Get(urlCreateTruongHoc, null, null);
            List<TruongHoc> lstTruongHoc = JsonConvert.DeserializeObject<List<TruongHoc>>(rsTruongHoc);

            List<InputGiaoVien> lstInput = new List<InputGiaoVien>();

            foreach (var truongHoc in lstTruongHoc)
            {
                foreach (var namHoc in lstNamHoc)
                {
                    var input = new InputGiaoVien
                    {
                        offset = 0,
                        limit = 9999,
                        maTruongHoc = truongHoc.MaTruongHoc,
                        namHoc = namHoc
                    };

                    lstInput.Add(input);
                }
            }

            int id = 1;   
            var temp = new List<GiaoVien>();

            foreach (var input in lstInput)
            {
                var rs = _httpService.PostVinhLong(urlGetGiaoVien, null, input);

                ApiGiaoVien apiReturn = JsonConvert.DeserializeObject<ApiGiaoVien>(rs);
                var result = apiReturn?.danhSachGiaoVien;
                if (result is null || result.Count == 0) continue;

                var length = result.Count;
                int count = 0;
                int countAll = 0;

                foreach (var item in result)
                {
                    item.ID = id.ToString();
                    item.NamHoc = input.namHoc.ToString();
                    temp.Add(item);
                    count++;
                    countAll++;
                    id++;

                    if (count == 50 || id == length + 1)
                    {
                        _httpService.Post(urlCreateGiaoVien, null, temp);
                        temp = new List<GiaoVien>();
                        count = 0;
                    }
                }           
            }

            return id;
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
