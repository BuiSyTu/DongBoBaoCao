namespace DongBoBaoCao.Core.ViewModels
{
    public class DanhSachDuLieuInput
    {
        public string token { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
