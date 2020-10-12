using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IGiaoVienService
    {
        List<GiaoVien> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
