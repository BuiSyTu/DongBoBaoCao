using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IDonViGiaoDucService
    {
        List<DonViGiaoDuc> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
