using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class JsonRequester
    {
        HttpClient client;


        public JsonRequester(string baseUrl)
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(baseUrl);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<string> Send(HttpMethod method, string url, string data = "")
        {
            var request = new HttpRequestMessage(method, url);

            if (data != string.Empty)
            {
                request.Content = new StringContent(data, Encoding.UTF8, "application/json");
            }

            var response = await this.client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public string Get(string url)
        {
            return Send(HttpMethod.Get, url).Result;
        }

        public string Post(string url, string data)
        {
            return Send(HttpMethod.Post, url, data).Result;
        }
    }
}
