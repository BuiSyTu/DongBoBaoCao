using DongBoBaoCao.Core.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DongBoBaoCao.Core.Services
{
    public class HttpService: IHttpService
    {
        public async Task<string> GetAsync(string baseAddress, string uri, string bearToken, object jObject)
        {
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, baseAddress + uri) { };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            var res = await client.SendAsync(req);

            var result = string.Empty;
            if (res.IsSuccessStatusCode)
            {
                result = await res.Content.ReadAsStringAsync();
            }

            return result;
        }


        public async Task<string> PostAsync(string baseAddress, string uri, string bearToken, object jObject)
        {
            string dataString = JsonConvert.SerializeObject(jObject);
            StringContent data = new StringContent(dataString, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, baseAddress + uri) { Content = data };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            if (!string.IsNullOrEmpty(bearToken))
            {
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            }
            var res = await client.SendAsync(req);

            var result = string.Empty;

            if (res.IsSuccessStatusCode)
            {
                result = await res.Content.ReadAsStringAsync();
            }

            return result;
        }

        public async Task<string> PostAsync(string address, string bearToken, object jObject)
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
            var res = await client.SendAsync(req);

            var result = string.Empty;

            if (res.IsSuccessStatusCode)
            {
                result = await res.Content.ReadAsStringAsync();
            }

            return result;
        }

        public string Get(string baseAddress, string uri, string bearToken, object jObject)
        {
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, baseAddress + uri) { };
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

        public string PostVinhLong(string address, string bearToken, object jObject)
        {
            string dataString = JsonConvert.SerializeObject(jObject);
            StringContent data = new StringContent(dataString, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, address) { Content = data };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            req.Headers.Add("Username", "vlg_sync");
            req.Headers.Add("password", "vlg@2020#");
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

        public string Post(string baseAddress, string uri, string bearToken, object jObject)
        {
            string dataString = JsonConvert.SerializeObject(jObject);
            StringContent data = new StringContent(dataString, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, baseAddress + uri) { Content = data };
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
