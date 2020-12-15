using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Interfaces
{
    public interface IHttpService
    {
        string Get(string address, string bearToken);
        string Post(string address, string bearToken, object jObject);
        string Put(string address, string bearToken, object jObject);
        string Delete(string address, string bearToken, object jObject);
    }
}
