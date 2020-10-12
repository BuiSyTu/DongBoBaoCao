using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface ILopHocService
    {
        List<LopHoc> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
