using System;
using System.Collections.Generic;
using System.Text;

namespace DongBoBaoCao.Core.ViewModels
{
    public class Indicator
    {
        public string Code { get; set; }
        public int value { get; set; }
        public int error { get; set; }
        public List<Indicator> Childrens { get; set; }
    }
}
