using DongBoBaoCao.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.Interfaces
{
    public interface IDVCService
    {
        void AddChiTieuBaoCao();
        void CreateDanhSachDuLieu();
        ICollection<DVCViewModel> GetDanhSachDuLieu(int? page);
        ICollection<DVCViewModel> GetDanhSachDuLieu(int month, int year, int? page);
        void RandomChiTieuBaoCao();
    }
}
