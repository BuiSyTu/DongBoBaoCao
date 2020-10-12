using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IDVCLinhVucService
    {
        List<DVCLinhVuc> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
