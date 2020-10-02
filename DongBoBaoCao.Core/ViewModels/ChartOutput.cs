using System.Collections.Generic;

namespace DongBoBaoCao.Core.ViewModels
{
    public class ChartOutput
    {
        public string available { get; set; }
        public IList<ChartOutputItem> data { get; set; }
        public string function { get; set; }
        public string title { get; set; }
        public string type { get; set; }

    }

    public class ChartOutputItem
    {
        public string title { get; set; }
        public string code { get; set; }
        public int value { get; set; }
    }
}
