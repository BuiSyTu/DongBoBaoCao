using DongBoBaoCao.Core.Interfaces;
using System;
using System.Globalization;

namespace DongBoBaoCao.Core.Services
{
    public class DateTimeService : IDateTimeService
    {
        public string GetStartDateOfMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            return startDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public string GetStartDateOfMonth(int month, int year, string format)
        {
            var startDate = new DateTime(year, month, 1);
            return startDate.ToString(format, CultureInfo.InvariantCulture);
        }

        public string GetLastDateOfMonth(int month, int year)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return endDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public string GetLastDateOfMonth(int month, int year, string format)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return endDate.ToString(format, CultureInfo.InvariantCulture);
        }

        public DateTime StringToDateTime(string dateString, string format)
        {
            var provider = CultureInfo.InvariantCulture;
            var result = DateTime.ParseExact(dateString, format, provider);
            return result;
        }

        public string DateTimeToString(DateTime date, string format)
        {
            return date.ToString(format);
        }
    }
}
