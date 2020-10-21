using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DongBoBaoCao.VinhLong.ViewModels
{
    public class AreaDataItem
    {
        public string indicatorCode { get; set; }
        public string areaCode { get; set; }
        public int periodId { get; set; }
        public int dataTypeId { get; set; }
        public int dataYear { get; set; }
        public int value { get; set; }
        public string textValue { get; set; }
    }
}
