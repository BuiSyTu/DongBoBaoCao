using System.Collections.Generic;

namespace DongBoBaoCao.ViewModels
{
    public class APIResult<T>
    {
        public List<T> data { get; set; }
        public double total { get; set; }
        public ErrorResult error { get; set; }
    }
}
