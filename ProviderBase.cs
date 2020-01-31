using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApp.Models
{
    public class ProviderBase
    {
        #region Private Members
        private static readonly HttpClient _client;
        private readonly static string baseAddress = "https://localhost:44342/";
        private static readonly int maxRetryAttempts = 3;
        private static TimeSpan pauseBetweenFailures = TimeSpan.FromSeconds(5);
        #endregion

        static ProviderBase()
        {
            _client= new HttpClient();
        }
        public static async Task<Envelope<T>> PostAsync<T>(string requestUrl, object data, string AccessToken)
        {
                AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODA0NjA0NzUsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.kXqovdJ7FK6zzTn6q-RLGGD-5HM7G6ZOBNkEnzrvv08";//string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

                if (_client.DefaultRequestHeaders.Contains("ChannelType"))
                {
                    _client.DefaultRequestHeaders.Remove("ChannelType");
                }
                _client.DefaultRequestHeaders.Add("ChannelType", "Web");
                var json = JsonConvert.SerializeObject(data);
                var payload = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(string.Concat(baseAddress, requestUrl), payload).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonConvert.DeserializeObject<Envelope<T>>(result);
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new UnAuthorisedException(requestUrl);
                }
                return null;

        }
        public static async Task<Envelope<T>> PostAsync<T>(string requestUrl, MultipartFormDataContent data, string AccessToken)
        {
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODA0NjA0NzUsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.kXqovdJ7FK6zzTn6q-RLGGD-5HM7G6ZOBNkEnzrvv08";//string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
            //AccessToken = string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            if (_client.DefaultRequestHeaders.Contains("ChannelType"))
            {
                _client.DefaultRequestHeaders.Remove("ChannelType");
            }
            _client.DefaultRequestHeaders.Add("ChannelType", "Web");
            var response = await _client.PostAsync(string.Concat(baseAddress, requestUrl), data).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Envelope<T>>(result);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //throw new UnAuthorisedException(requestUrl);
            }
            return null;
        }
        public static async Task<Envelope<T>> GetAsync<T>(string requestUrl, string AccessToken)
        {
            //AccessToken = string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODA0NjA0NzUsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.kXqovdJ7FK6zzTn6q-RLGGD-5HM7G6ZOBNkEnzrvv08";//string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            if (_client.DefaultRequestHeaders.Contains("ChannelType"))
            {
                _client.DefaultRequestHeaders.Remove("ChannelType");
            }
            _client.DefaultRequestHeaders.Add("ChannelType", "Web");
            HttpResponseMessage response = new HttpResponseMessage();
            RetryOnException(maxRetryAttempts, pauseBetweenFailures, () =>
            {
                response = _client.GetAsync(string.Concat(baseAddress, requestUrl)).Result;
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Envelope<T>>(result);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                //throw new UnAuthorisedException(requestUrl);
            }
            return null;
        }
        public static async Task<Envelope<T>> GetAsync<T>(string requestUrl, object data, string AccessToken)
        {
            // AccessToken = string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE1ODA0NjA0NzUsImlzcyI6IlRlc3QuY29tIiwiYXVkIjoiVGVzdC5jb20ifQ.kXqovdJ7FK6zzTn6q-RLGGD-5HM7G6ZOBNkEnzrvv08";//string.IsNullOrWhiteSpace(AccessToken) ? GetToken() : AccessToken;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            if (_client.DefaultRequestHeaders.Contains("ChannelType"))
            {
                _client.DefaultRequestHeaders.Remove("ChannelType");
            }
            _client.DefaultRequestHeaders.Add("ChannelType", "Web");
            HttpResponseMessage response = new HttpResponseMessage();
            RetryOnException(maxRetryAttempts, pauseBetweenFailures, () =>
            {
                response = _client.GetAsync(string.Concat(baseAddress, requestUrl)).Result;
            });
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Envelope<T>>(result);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
               // throw; //new UnAuthorisedException(requestUrl);
            }
            return null;
        }

        private static void RetryOnException(int times, TimeSpan delay, Action operation)
        {
            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    operation();
                    break; // Sucess! Lets exit the loop!
                }
                catch (Exception ex)
                {
                    Task.Delay(delay).Wait();
                }
            } while (attempts <= times);
        }
    }
}
