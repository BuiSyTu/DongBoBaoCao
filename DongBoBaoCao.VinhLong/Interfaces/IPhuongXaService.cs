using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IPhuongXaService
    {
        List<PhuongXa> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
