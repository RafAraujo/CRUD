using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebMVC.WebApi
{
    public static class WebApiCommunicator
    {
        private static readonly HttpClient _client;

        static WebApiCommunicator()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51456/api/")
            };
        }

        public static async Task<WebApiResponse<TResponse>> Get<TResponse>(string url)
        {
            var response = new WebApiResponse<TResponse>();

            try
            {
                var httpResponseMessage = await Send(HttpMethod.Get, url, null);
                await response.Load(httpResponseMessage);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public static async Task<WebApiResponse<TResponse>> Post<TRequest, TResponse>(string url, TRequest request)
        {
            var response = new WebApiResponse<TResponse>();

            try
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var httpResponseMessage = await Send(HttpMethod.Post, url, requestContent);
                await response.Load(httpResponseMessage);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public static async Task<WebApiResponse<TResponse>> Put<TRequest, TResponse>(string url, TRequest request)
        {
            var response = new WebApiResponse<TResponse>();

            try
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var httpResponseMessage = await Send(HttpMethod.Put, url, requestContent);
                await response.Load(httpResponseMessage);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public static async Task<WebApiResponse> Delete(string url)
        {
            var response = new WebApiResponse();

            try
            {
                var httpResponseMessage = await Send(HttpMethod.Delete, url, null);
                await response.Load(httpResponseMessage);
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        private static async Task<HttpResponseMessage> Send(HttpMethod httpMethod, string url, StringContent requestContent)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = httpMethod,
                RequestUri = new Uri($"{_client.BaseAddress}{url}")
            };

            if (requestContent != null)
            {
                httpRequestMessage.Content = requestContent;
            }

            var httpResponseMessage = await _client.SendAsync(httpRequestMessage);
            return httpResponseMessage;
        }
    }
}