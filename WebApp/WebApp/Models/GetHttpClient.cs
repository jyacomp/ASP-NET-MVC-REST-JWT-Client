using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebApp.Controllers
{
    public static class GetHttpClient
    {
        // PUT Request
        public static IList<T> GetRequest<T>(HttpClient httpClient, Uri uri, string requestUri)
        {
            IList<T> response = null;
            httpClient.BaseAddress = uri;
            var responseTask = httpClient.GetAsync(requestUri);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<T>>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
        // GET Request
        public static T GetRequest<T>(HttpClient httpClient, Uri uri, string requestUri, string value)
        {
            T response = Activator.CreateInstance<T>();
            httpClient.BaseAddress = uri;
            var responseTask = httpClient.GetAsync(requestUri + "/" + value);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
        // POST Request
        public static T PostRequest<T>(HttpClient httpClient, Uri uri, string requestUri, object data)
        {
            T response = Activator.CreateInstance<T>();
            httpClient.BaseAddress = uri;

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var responseTask = httpClient.PostAsync(requestUri, content);
            responseTask.Wait();
            var result = responseTask.Result;
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
        // PUT Request
        public static T PutRequest<T>(HttpClient httpClient, Uri uri, string requestUri, object data, string value)
        {
            T response = Activator.CreateInstance<T>();
            httpClient.BaseAddress = uri;

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            var responseTask = httpClient.PutAsJsonAsync(requestUri + value, (T)data);
            responseTask.Wait();
            var result = responseTask.Result;
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<T>();
                readTask.Wait();
                response = readTask.Result;
            }
            else
            {
                throw new HttpRequestException();
            }
            httpClient.Dispose();
            return response;
        }
    }
}