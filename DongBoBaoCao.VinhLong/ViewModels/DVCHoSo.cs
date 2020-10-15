using System;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.ViewModels
{
    public class DVCHoSo
    {
        public string ID { get; set; }
        public string HoSoID { get; set; }
        public string SoBienNhan { get; set; }
        public string ThuTuc { get; set; }
        public string LinhVuc { get; set; }
        public string NgayNhan { get; set; }
        public string NgayHoanThanh { get; set; }
        public string NoiNhan { get; set; }
        public Boolean NhanQuaMang { get; set; }
        public string NgayHenTra { get; set; }
    }

    public class ApiDVCHoSo
    {
        public int total { get; set; }
        public List<DVCHoSo> items { get; set; }
    }
}
