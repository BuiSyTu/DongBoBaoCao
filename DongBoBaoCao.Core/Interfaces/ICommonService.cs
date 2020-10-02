using DongBoBaoCao.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface ICommonService
    {
        Task <int> CreateDanhSachDuLieuAsync(string baseAddress, string bearToken);
        Task <int> CreateDanhSachDuLieuTrongNgayAsync(string baseAddress, string bearToken);
        Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string bearToken, int page);
        Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit);
        Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string bearToken, int page);
        Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit);
        int CreateDanhSachDuLieu(string baseAddress, string bearToken);
        int CreateDanhSachDuLieuTrongNgay(string baseAddress, string bearToken);
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string bearToken, int page);
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string bearToken, int page);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit);
        ChartOutput GetDuLieuLoc(ChartInput input);
        bool? AddOrUpdateIndicator(OUDataItem oUDataItem);
    }
}
