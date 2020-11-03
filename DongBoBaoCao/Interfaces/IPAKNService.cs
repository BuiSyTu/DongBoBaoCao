using DongBoBaoCao.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.Interfaces
{
    public interface IPAKNService
    {
        void CreateDanhSachDuLieu();
        ICollection<PAKNViewModel> GetDanhSachDuLieu(int? page);
        void RandomChiTieuBaoCao();
        void AddChiTieuBaoCao();
    }
}
