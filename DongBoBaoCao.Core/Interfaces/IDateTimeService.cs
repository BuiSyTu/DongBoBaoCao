using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface IDateTimeService
    {
        string GetStartDateOfMonth(int month, int year);
        string GetLastDateOfMonth(int month, int year);
    }
}
