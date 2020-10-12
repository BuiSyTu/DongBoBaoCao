using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.ViewModels
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
