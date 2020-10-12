using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IDVCThuTucHanhChinhService
    {
        List<DVCThuTucHanhChinh> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
