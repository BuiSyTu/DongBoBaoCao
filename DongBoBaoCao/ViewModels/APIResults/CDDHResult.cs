using System.Collections.Generic;

namespace DongBoBaoCao.ViewModels
{
    public class CDDHResult
    {
        public List<CDDHViewModel> data { get; set; }
        public double total { get; set; }
        public ErrorResult error { get; set; }
    }
}
