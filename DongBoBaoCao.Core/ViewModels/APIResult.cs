using System.Collections.Generic;

namespace DongBoBaoCao.Core.ViewModels
{
    public class APIResult
    {
        public List<VanBan> data { get; set; }
        public int total { get; set; }
        public ErrorResult error { get; set; }
    }
}
