using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.ViewModels
{
    public class DVCDonVi
    {
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
    }

    public class ApiDVCDonVi
    {
        public int total { get; set; }
        public List<DVCDonVi> items { get; set; }
    }
}
