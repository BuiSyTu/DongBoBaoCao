using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.ViewModels.VinhLong
{
    public class DVCLinhVuc
    {
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string MaLinhVuc { get; set; }
        public string TenLinhVuc { get; set; }
    }

    public class ApiDVCLinhVuc
    {
        public int total { get; set; }
        public List<DVCLinhVuc> items { get; set; }
    }
}
