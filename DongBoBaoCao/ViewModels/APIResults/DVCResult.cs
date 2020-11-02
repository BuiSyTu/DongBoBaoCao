using System.Collections.Generic;

namespace DongBoBaoCao.ViewModels
{
    public class DVCResult
    {
        public List<DVCViewModel> data { get; set; }
        public double total { get; set; }
        public ErrorResult error { get; set; }
    }
}
