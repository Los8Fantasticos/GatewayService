using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Api.CrossCutting.Helpers
{
    public static class RequestHelper
    {
        public static async Task<T> PostRequest<T, TK>(string url, TK model)
        {
            try
            {
                T result = default;
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);
                string postBody = JsonConvert.SerializeObject(model);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> PostRequest<T>(string url, object model)
        {
            try
            {
                T result = default;
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);
                string postBody = JsonConvert.SerializeObject(model);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> PostRequestFiles<T>(string url, IFormCollection form)
        {
            try
            {
                MultipartFormDataContent multiContent = new MultipartFormDataContent();
                List<IFormFile> list = form.Files.ToList();
                foreach (IFormFile file in list)
                {
                    StreamContent streamContent = new StreamContent(file.OpenReadStream());
                    multiContent.Add(streamContent, file.Name, file.FileName);
                }

                T result = default;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(url, multiContent);
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> PutRequest<T, TK>(string url, TK model)
        {
            try
            {
                T result = default;
                HttpClient client = new HttpClient();
                string postBody = JsonConvert.SerializeObject(model);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> DeleteRequest<T>(string url)
        {
            try
            {
                T result = default;
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync(url);
                string contents = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<T> GetRequest<T>(string urlRequest)
        {
            try
            {
                T result = default;

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(urlRequest);
                string contents = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<T>(contents);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
