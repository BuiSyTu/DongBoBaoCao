using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IHocSinhService
    {
        List<HocSinh> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
