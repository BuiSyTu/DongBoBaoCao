using DongBoBaoCao.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DongBoBaoCao.Core.Services
{
    public class DuLieuChungService : IDuLieuChungService
    {
        private readonly IConfiguration _config;
        private readonly string _truncateAddress;
        private readonly IHttpService _httpService;

        public DuLieuChungService(IConfiguration config, IHttpService httpService)
        {
            _config = config;
            _httpService = httpService;

            _truncateAddress = _config.GetSection("Truncate:url").Value;
        }

        public bool? Truncate()
        {
            var rs = _httpService.Delete(_truncateAddress, null, null);

            if (string.IsNullOrEmpty(rs))
            {
                return null;
            }

            bool result = JsonConvert.DeserializeObject<bool>(rs);
            return result;
        }
    }
}
