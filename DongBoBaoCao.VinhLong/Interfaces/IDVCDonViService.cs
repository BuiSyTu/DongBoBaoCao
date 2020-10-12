using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IDVCDonViService
    {
        List<DVCDonVi> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
