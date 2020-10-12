using DongBoBaoCao.VinhLong.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.VinhLong.Interfaces
{
    public interface ITonGiaoService
    {
        List<TonGiao> Get();
        int Create();
        bool? Delete();
        int DeleteAndCreateNew();
    }
}
