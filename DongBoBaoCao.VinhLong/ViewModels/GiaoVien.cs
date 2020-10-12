using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.ViewModels
{
    public class GiaoVien
    {
        public string ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string TenGioiTinh { get; set; }
        public string SoCMND { get; set; }
        public string ChucVu { get; set; }
        public string ChuyenNganhDaoTao { get; set; }
        public string TrinhDoDaoTao { get; set; }
        public string LoaiTrinhDoDaoTao { get; set; }
        public string Email { get; set; }
        public string Sdt { get; set; }
        public string MaTruong { get; set; }
        public string TenTruong { get; set; }
        public string ToBoMon { get; set; }
        public string LoaiCongViec { get; set; }
        public string QuocTich { get; set; }
        public string DanToc { get; set; }
        public string TenDanToc { get; set; }
        public string TonGiao { get; set; }
        public string TenTonGiao { get; set; }
        public string MaTinhThanh { get; set; }
        public string TenTinhThanh { get; set; }
        public string MaQuanHuyen { get; set; }
        public string TenQuanHuyen { get; set; }
        public string NgayCapCMT { get; set; }
        public string NoiCapCMT { get; set; }
        public string QueQuan { get; set; }
        public string DiaChiTamTru { get; set; }
        public string DiaChiThuongTru { get; set; }
        public string TrinhDoDaoTaoCnChinh { get; set; }
        public string TrinhDoNgoaiNgu { get; set; }
        public string TrinhDoVanHoa { get; set; }
        public string NgayVaoNghe { get; set; }
        public string LoaiHopDong { get; set; }
        public string HeSoLuong { get; set; }
        public string BacLuong { get; set; }
        public string NgachLuong { get; set; }
        public string TrangThai { get; set; }
        public string NamHoc { get; set; }
    }

    public class ApiGiaoVien
    {
        public List<GiaoVien> danhSachGiaoVien { get; set; }
        public string returnCode { get; set; }
    }
}
