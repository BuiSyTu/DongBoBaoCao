using DongBoBaoCao.Core.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DongBoBaoCao.Core.Services
{
    public class HttpService: IHttpService
    {
        public string Get(string address, string bearToken, object jObject)
        {
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, address) { };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            var res = client.SendAsync(req);

            var result = string.Empty;
            if (res.Result.IsSuccessStatusCode)
            {
                result = res.Result.Content.ReadAsStringAsync().Result;
            }

            return result;
        }

        public string Post(string address, string bearToken, object jObject)
        {
            string dataString = JsonConvert.SerializeObject(jObject);
            StringContent data = new StringContent(dataString, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, address) { Content = data };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            if (!string.IsNullOrEmpty(bearToken))
            {
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            }

            var res = client.SendAsync(req);

            string result = string.Empty;
            
            if (res.Result.IsSuccessStatusCode)
            {
                result = res.Result.Content.ReadAsStringAsync().Result;
            }

            return result;
        }

        public string Delete(string address, string bearToken, object jObject)
        {
            string dataString = JsonConvert.SerializeObject(jObject);
            StringContent data = new StringContent(dataString, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Delete, address) { Content = data };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            if (!string.IsNullOrEmpty(bearToken))
            {
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            }
            var res = client.SendAsync(req);

            var result = string.Empty;

            if (res.Result.IsSuccessStatusCode)
            {
                result = res.Result.Content.ReadAsStringAsync().Result;
            }

            return result;
        }

        public string Put(string address, string bearToken, object jObject)
        {
            string dataString = JsonConvert.SerializeObject(jObject);
            StringContent data = new StringContent(dataString, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Put, address) { Content = data };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            if (!string.IsNullOrEmpty(bearToken))
            {
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            }
            var res = client.SendAsync(req);

            var result = string.Empty;

            if (res.Result.IsSuccessStatusCode)
            {
                result = res.Result.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
    }
}
