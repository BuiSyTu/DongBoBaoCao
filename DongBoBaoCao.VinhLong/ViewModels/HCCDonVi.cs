using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.VinhLong.ViewModels
{
    public class HCCDonVi
    {
        public string Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
    }

    public class ApiHCCDonVi
    {
        public int total { get; set; }
        public List<HCCDonVi> items { get; set; }
    }
}
