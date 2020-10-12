using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.ViewModels.VinhLong
{
    public class HocSinh
    {
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaTruong { get; set; }
        public string TenTruong { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DanToc { get; set; }
        public string NoiSinh { get; set; }
        public string TruongHoc { get; set; }
        public string LopHoc { get; set; }
        public string TrangThai { get; set; }
        public string TenLopHoc { get; set; }
        public string KhoiLop { get; set; }
        public string SdtBo { get; set; }
        public string SdtMe { get; set; }
        public string NamHoc { get; set; }
    }

    public class ApiHocSinh
    {
        public List<HocSinh> listDanhSachHocSinh { get; set; }
        public string returnCode { get; set; }
    }
}
