using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.Core.ViewModels.VinhLong;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Services.VinhLong
{
    public class HocSinhService
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _config;
        private string urlGetHocSinh;
        private string urlCreateHocSinh;
        private string urlCreateTruongHoc;


        public HocSinhService(IHttpService httpService, IConfiguration config)
        {
            _httpService = httpService;
            _config = config;

            urlGetHocSinh = _config.GetSection("VinhLong:GetHocSinh:url").Value;
            urlCreateHocSinh = _config.GetSection("VinhLong:CreateHocSinh:url").Value;
            urlCreateTruongHoc = _config.GetSection("VinhLong:CreateTruongHoc:url").Value;
        }

        public List<HocSinh> Get()
        {
            var lstNamHoc = new List<int>() { 2019, 2020 };

            var rsTruongHoc = _httpService.Get(urlCreateTruongHoc, null, null);
            List<TruongHoc> lstTruongHoc = JsonConvert.DeserializeObject<List<TruongHoc>>(rsTruongHoc);

            List<InputHocSinh> lstInput = new List<InputHocSinh>();

            foreach (var truongHoc in lstTruongHoc)
            {
                foreach (var namHoc in lstNamHoc)
                {
                    var input = new InputHocSinh
                    {
                        offset = 0,
                        limit = 9999,
                        maTruongHoc = truongHoc.MaTruongHoc,
                        namHoc = namHoc
                    };

                    lstInput.Add(input);
                }
            }

            var lstHocSinh = new List<HocSinh>();

            foreach (var input in lstInput)
            {
                var rs = _httpService.PostVinhLong(urlGetHocSinh, null, null);

                List<HocSinh> result = JsonConvert.DeserializeObject<List<HocSinh>>(rs);
                lstHocSinh.AddRange(result);
            }

            return lstHocSinh;
        }

        public int Create()
        {
            var lstNamHoc = new List<int>() { 2019, 2020 };

            var rsTruongHoc = _httpService.Get(urlCreateTruongHoc, null, null);
            List<TruongHoc> lstTruongHoc = JsonConvert.DeserializeObject<List<TruongHoc>>(rsTruongHoc);
            List<InputHocSinh> lstInput = new List<InputHocSinh>();

            foreach (var truongHoc in lstTruongHoc)
            {
                foreach (var namHoc in lstNamHoc)
                {
                    var input = new InputHocSinh
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
            var temp = new List<HocSinh>();

            foreach (var input in lstInput)
            {
                var rs = _httpService.PostVinhLong(urlGetHocSinh, null, input);

                ApiHocSinh apiReturn = JsonConvert.DeserializeObject<ApiHocSinh>(rs);
                var result = apiReturn?.listDanhSachHocSinh;
                if (result is null || result.Count == 0) continue;

                var length = result.Count;
                int count = 0;
                int countAll = 0;

                foreach (var item in result)
                {
                    item.ID = id.ToString();
                    item.NamHoc = input.namHoc.ToString();
                    item.TruongHoc = item.MaTruong;
                    temp.Add(item);
                    count++;
                    countAll++;
                    id++;

                    if (count == 50 || id == length + 1)
                    {
                        _httpService.Post(urlCreateHocSinh, null, temp);
                        temp = new List<HocSinh>();
                        count = 0;
                    }
                }
            }

            return id;
        }
    }
}
