using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class LopHocService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private string urlGetLopHoc;
        private string urlCreateLopHoc;
        private string urlCreateTruongHoc;


        public LopHocService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetLopHoc = _config.GetSection("VinhLong:GetLopHoc:url").Value;
            urlCreateLopHoc = _config.GetSection("VinhLong:CreateLopHoc:url").Value;
            urlCreateTruongHoc = _config.GetSection("VinhLong:CreateTruongHoc:url").Value;
        }

        public List<LopHoc> Get()
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

            var lstLopHoc = new List<LopHoc>();

            foreach (var input in lstInput)
            {
                var rs = _httpService.PostVinhLong(urlGetLopHoc, null, null);

                List<LopHoc> result = JsonConvert.DeserializeObject<List<LopHoc>>(rs);
                lstLopHoc.AddRange(result);
            }

            return lstLopHoc;
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
            var temp = new List<LopHoc>();

            foreach (var input in lstInput)
            {
                var rs = _httpService.PostVinhLong(urlGetLopHoc, null, input);

                List<LopHoc> result = JsonConvert.DeserializeObject<List<LopHoc>>(rs);

                if (result.Count == 0 || result is null) continue;

                var length = result.Count;
                int count = 0;
                int countAll = 0;

                foreach (var item in result)
                {
                    item.ID = id.ToString();
                    item.NamHoc = input.namHoc.ToString();
                    item.Ma = item.MaLopHoc;
                    item.Ten = item.TenLopHoc;
                    item.MaTruongHoc = input.maTruongHoc;
                    temp.Add(item);
                    count++;
                    countAll++;
                    id++;

                    if (count == 50 || countAll == length)
                    {
                        _httpService.Post(urlCreateLopHoc, null, temp);
                        temp = new List<LopHoc>();
                        count = 0;
                    }
                }
            }

            return id;
        }
    }
}
