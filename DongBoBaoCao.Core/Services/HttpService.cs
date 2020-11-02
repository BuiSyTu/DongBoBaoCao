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

        #region VinhLong
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

        public string GetVinhLong(string address, string bearToken, object jObject)
        {
            using HttpClient client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, address) { };
            req.Headers.Add("Accept", "application/json;odata=verbose");
            req.Headers.Add("apikey", "00.00.E61");
            req.Headers.Add("seckey", "69780314-af6f-4cef-afbb-52e21a4a2ca4");
            req.Headers.Add("__csrf_prevent", "HTBC");
            req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearToken);
            var res = client.SendAsync(req);

            var result = string.Empty;
            if (res.Result.IsSuccessStatusCode)
            {
                result = res.Result.Content.ReadAsStringAsync().Result;
            }

            return result;
        }
        #endregion
    }
}
