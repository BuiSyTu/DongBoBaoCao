using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface IDateTimeService
    {
        string GetStartDateOfMonth(int month, int year);
        string GetStartDateOfMonth(int month, int year, string format);
        string GetLastDateOfMonth(int month, int year);
        string GetLastDateOfMonth(int month, int year, string format);
        DateTime StringToDateTime(string dateString, string format);
        string DateTimeToString(DateTime date, string format);
    }
}
