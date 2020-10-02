using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.ViewModels
{
    public class OUDataItem
    {
        public string indicatorCode { get; set; }
        public string officeCode { get; set; }
        public int periodId { get; set; }
        public int dataTypeId { get; set; }
        public int dataYear { get; set; }
        public int value { get; set; }
        public string textValue { get; set; }
    }
}
