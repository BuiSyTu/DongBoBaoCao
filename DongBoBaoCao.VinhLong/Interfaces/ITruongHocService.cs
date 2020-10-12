using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface ITruongHocService
    {
        List<TruongHoc> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
