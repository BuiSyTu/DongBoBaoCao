namespace DongBoBaoCao.Interfaces
{
    public interface IHttpService
    {
        string Get(string baseAddress, string uri, string bearToken, object jObject);
        string Post(string baseAddress, string uri, string bearToken, object jObject);
        string Post(string address, string bearToken, object jObject);
    }
}
