using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface IHttpService
    {
        Task<string> GetAsync(string baseAddress, string uri, string bearToken, object jObject);
        Task<string> PostAsync(string baseAddress, string uri, string bearToken, object jObject);
        Task<string> PostAsync(string address, string bearToken, object jObject);
        string Get(string baseAddress, string uri, string bearToken, object jObject);
        string Post(string baseAddress, string uri, string bearToken, object jObject);
        string Post(string address, string bearToken, object jObject);
        string Put(string address, string bearToken, object jObject);
        string Delete(string address, string bearToken, object jObject);
    }
}
