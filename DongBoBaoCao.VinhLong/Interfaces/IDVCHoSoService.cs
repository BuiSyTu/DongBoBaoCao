using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IDVCHoSoService
    {
        List<DVCHoSo> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
        void ThemChiTieuBaoCao();
    }
}
