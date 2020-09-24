using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.ViewModels
{
    public class VanBan
    {
        public string NoiDung { get; set; }
        public string PhanMem { get; set; }
        public string Loai { get; set; }
        public string TaiKhoanNguoiGiao { get; set; }
        public string MaDonViXuLyChinh { get; set; }
        public string TaiKhoanNguoiXuLyChinh { get; set; }
        /// <summary>
        /// List separated by ;
        /// </summary>
        public string MaDonViPhoiHop { get; set; }
        /// <summary>
        /// List separated by ;
        /// </summary>
        public string TaiKhoanNguoiPhoiHop { get; set; }
        public string NgayGiao { get; set; }
        public string ThoiHan { get; set; }
        public string MaTrangThaiChung { get; set; }
        public string MaTrangThaiPhanMem { get; set; }
        public string MaTinhTrang { get; set; }
        public string NgayXuLy { get; set; }
    }
}
