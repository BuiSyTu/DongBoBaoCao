using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DongBoBaoCao.Core.ViewModels
{
    [DataContract]
    [KnownType(typeof(List<VanBan>))]
    [KnownType(typeof(ChartOutput))]
    [KnownType(typeof(bool))]
    [Serializable]
    public class APIResult
    {
        public object data { get; set; }
        public int total { get; set; }
        public ErrorResult error { get; set; }
    }
}
