using System;

namespace DongBoBaoCao.Models
{
    public class CDDH
    {
        public string ID { get; set; }
        public string PhanMem { get; set; }
        public string LoaiDuLieu { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string LinhVuc { get; set; }
        public string MucDo { get; set; }
        public string NguonDuLieu { get; set; }
        public string MaDonVi { get; set; }
        public string NguoiKy { get; set; }
        public string NguoiTao { get; set; }
        public string NguoiGiao { get; set; }
        public string DonViXuLyChinh { get; set; }
        public string NguoiXuLyChinh { get; set; }
        public string DonViPhoiHop { get; set; }
        public string NguoiPhoiHop { get; set; }
        public DateTime? NgayGiao { get; set; }
        public DateTime? ThoiHan { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public string MaTrangThaiChung { get; set; }
        public string MaTrangThaiPhanMem { get; set; }
        public string MaTinhTrang { get; set; }
        public string PhanLoaiTheoSoNguoiKy { get; set; }
        public string PhanLoaiTheoThamQuyen { get; set; }
        public string DiaBan { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
