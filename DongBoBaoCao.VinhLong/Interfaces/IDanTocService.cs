using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IDanTocService
    {
        List<DanToc> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
