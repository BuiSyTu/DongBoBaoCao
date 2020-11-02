using DongBoBaoCao.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface ICommonService
    {
        int CreateDanhSachDuLieu(string address, string bearToken);
        int CreateDanhSachDuLieuTrongNgay(string address, string bearToken);
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string bearToken, int page);
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string bearToken, int page);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit);
        ChartOutput GetDuLieuLoc(ChartInput input);
        bool? AddOrUpdateIndicator(OUDataItem oUDataItem);
    }
}
