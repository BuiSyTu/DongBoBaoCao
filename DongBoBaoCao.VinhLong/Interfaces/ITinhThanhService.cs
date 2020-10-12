using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface ITinhThanhService
    {
        List<TinhThanh> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
