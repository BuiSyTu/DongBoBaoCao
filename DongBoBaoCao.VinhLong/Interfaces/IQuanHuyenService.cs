using DongBoBaoCao.VinhLong.ViewModels;
using System.Collections.Generic;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface IQuanHuyenService
    {
        List<QuanHuyen> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
