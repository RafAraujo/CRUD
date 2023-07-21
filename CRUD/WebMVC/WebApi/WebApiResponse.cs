using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.WebApi
{
    public class WebApiResponse<T> : WebApiResponse
    {
        public T Result { get; set; }

        public override async Task Load(HttpResponseMessage httpResponseMessage)
        {
            HttpStatusCode = httpResponseMessage.StatusCode;
            Succcess = httpResponseMessage.IsSuccessStatusCode;

            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Result = JsonConvert.DeserializeObject<T>(httpContent);
            }
            else
            {
                ErrorMessage = httpContent;
            }
        }
    }

    public class WebApiResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }

        public bool Succcess { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }

        public virtual async Task Load(HttpResponseMessage httpResponseMessage)
        {
            HttpStatusCode = httpResponseMessage.StatusCode;
            Succcess = httpResponseMessage.IsSuccessStatusCode;

            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                ErrorMessage = httpContent;
            }
        }
    }
}