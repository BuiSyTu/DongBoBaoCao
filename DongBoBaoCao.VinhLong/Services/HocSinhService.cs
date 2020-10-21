using DongBoBaoCao.Core.Interfaces;
using DongBoBaoCao.VinhLong.Interfaces;
using DongBoBaoCao.VinhLong.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Services
{
    public class HocSinhService: IHocSinhService
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

        public bool? Delete()
        {
            throw new System.NotImplementedException();
        }

        public int DeleteAndCreateNew()
        {
            throw new System.NotImplementedException();
        }

        public void AddChiTieuBaoCao()

        {
            var lstMaQuanHuyen = new List<string> { "855", "857", "858", "859", "860", "861", "862", "863" };
            foreach (var quanHuyen in lstMaQuanHuyen)
            {
                var rsDonVi = _httpService.Get("https://baocao.hanhchinhcong.net/_vti_bin/td.bcdh.vinhlong/bcdhvinhlongservice.svc/PhuongXa", null, null);
                List<DonViVM> lstDonVi = JsonConvert.DeserializeObject<List<DonViVM>>(rsDonVi);
                foreach (var donVi in lstDonVi)
                {

                    var url = "https://baocao.hanhchinhcong.net/_vti_bin/td.bcdh.vinhlong/bcdhvinhlongservice.svc/GetTongSoTatCaHocSinh?maTinhThanh=86&maQuanHuyen=" + quanHuyen + "&namHoc=2020" + "&maPhuongXa=" + donVi.MaPhuongXa;
                    var lstHocSinh = _httpService.Get(url, null, null);
                    if (lstHocSinh == "")
                    {
                        continue;
                    }
                    else
                    {
                        TruongHocVM lstHS = JsonConvert.DeserializeObject<TruongHocVM>(lstHocSinh);

                        
                            var value = lstHS.GiaTri;
                            var textValue = value.ToString();
                            var offCode = lstHS.MaDonVi;
                            var rs1 = _httpService.Post("https://bc.vinhlong.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDiaBan", null, new AreaDataItem
                            {
                                dataTypeId = 3, // Thực hiện
                                dataYear = 2020,
                                indicatorCode = lstHS.MaChiTieu,
                                areaCode = offCode,
                                periodId = 1,
                                value = int.Parse(value),
                                textValue = textValue
                            });

                        
                    }
                }

            }


        }

        public void AddChiTieuBaoCao1()
        {

            var url = "https://baocao.hanhchinhcong.net/_vti_bin/td.bcdh.vinhlong/bcdhvinhlongservice.svc/GetTongSoHocSinh?maTinhThanh=86&namHoc=2020";
            var lstHocSinh = _httpService.Get(url, null, null);

            List<TruongHocVM> lstHS = JsonConvert.DeserializeObject<List<TruongHocVM>>(lstHocSinh);

            foreach (var hs in lstHS)
            {
                var value = hs.GiaTri;
                var textValue = value.ToString();
                var offCode = hs.MaDonVi;
                var rs1 = _httpService.Post("https://baoccao.vinhlong.gov.vn/_vti_bin/td.bc.dw/dwservice.svc/CapNhatChiTieuDiaBan", null, new AreaDataItem
                {
                    dataTypeId = 3, // Thực hiện
                    dataYear = 2020,
                    indicatorCode = hs.MaChiTieu,
                    areaCode = "86",
                    periodId = 1,
                    value = int.Parse(value),
                    textValue = textValue
                });
            }
        }
    }
}
