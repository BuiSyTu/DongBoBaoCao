using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.ViewModels.VinhLong
{
    public class DVCThuTucHanhChinh
    {
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaThuTuc { get; set; }
        public string TenThuTuc { get; set; }
        public string TenLinhVuc { get; set; }
        public string LinhVuc { get; set; }
    }

    public class ApiDVCThuTucHanhChinh
    {
        public int total { get; set; }
        public List<DVCThuTucHanhChinh> items { get; set; }
    }
}
