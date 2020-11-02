using DongBoBaoCao.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.Interfaces
{
    public interface ICDDHService
    {
        void CreateDanhSachDuLieu();
        ICollection<CDDHViewModel> GetDanhSachDuLieu(int? page);
        void RandomChiTieuBaoCao();
        void AddChiTieuBaoCao();
    }
}
