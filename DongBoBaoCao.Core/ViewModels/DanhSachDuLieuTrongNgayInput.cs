using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.ViewModels
{
    public class DanhSachDuLieuTrongNgayInput
    {
        public string token { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
