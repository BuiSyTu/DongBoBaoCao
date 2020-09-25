using DongBoBaoCao.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface IPAKNService
    {
        Task<int> CreateDanhSachDuLieuAsync();
        Task<int> CreateDanhSachDuLieuTrongNgayAsync();
        Task<ICollection<VanBan>> GetDanhSachDuLieuAsync(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit);
        Task<ICollection<VanBan>> GetDanhSachDuLieuTrongNgayAsync(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit);
        int CreateDanhSachDuLieu();
        int CreateDanhSachDuLieuTrongNgay();
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string danhSachDuLieu, string bearToken, string fromDate, string toDate, int page, int limit);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string danhSachDuLieuTrongNgay, string bearToken, int page, int limit);
    }
}
