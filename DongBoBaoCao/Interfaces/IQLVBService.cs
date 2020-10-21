using DongBoBaoCao.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DongBoBaoCao.Interfaces
{
    public interface IQLVBService
    {
        int CreateDanhSachDuLieu();
        int CreateDanhSachDuLieuTrongNgay();
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit);
        ChartOutput GetDuLieuLoc(ChartInput input);
        void AddChiTieuBaoCao();
        List<Indicator> Init();
    }
}
