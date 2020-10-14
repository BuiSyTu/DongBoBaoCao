using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DongBoBaoCao.API.ViewModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
