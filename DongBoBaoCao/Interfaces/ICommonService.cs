using DongBoBaoCao.Core.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.Interfaces
{
    public interface ICommonService
    {
        void CreateDanhSachDuLieu(string baseAddress, string bearToken);
        void CreateDanhSachDuLieuTrongNgay(string baseAddress, string bearToken);
        ICollection<VanBan> GetDanhSachDuLieu(string baseAddress, string bearToken, int page);
        ICollection<VanBan> GetDanhSachDuLieuTrongNgay(string baseAddress, string bearToken, int page);
    }
}
