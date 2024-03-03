using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NoteShare.CL.Services
{
    public interface IAPIService
    {
        Task<T> PostAsync<T>(string endpoint, object data);

        Task<T> GetAsync<T>(string endpoint);
        
        Task<T> PutAsync<T>(string endpoint, object data);

        Task<T> DeleteAsync<T>(string endpoint);
    }
    internal class APIService : IAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ISecureStorageService _secureStorage;

        public APIService(HttpClient httpClient, IConfiguration configuration, ISecureStorageService secureStorage)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _secureStorage = secureStorage;
            _httpClient.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
            _secureStorage.GetTokenAsync().ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    // TODO: Implementálni az authorization header beállítását konstruktorban
                }
            });
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }else
            {
                var statusCode = response.StatusCode;
                var reasonPhrase = response.ReasonPhrase;
                throw new HttpRequestException($"HTTP request failed with status code: {(int)statusCode} ({statusCode}) - {reasonPhrase}");
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(endpoint, data);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                var statusCode = response.StatusCode;
                var reasonPhrase = response.ReasonPhrase;
                throw new HttpRequestException($"HTTP request failed with status code: {(int)statusCode} ({statusCode}) - {reasonPhrase}");
            }
        }

        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var response = await _httpClient.PutAsJsonAsync(endpoint, data);
            
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                var statusCode = response.StatusCode;
                var reasonPhrase = response.ReasonPhrase;
                throw new HttpRequestException($"HTTP request failed with status code: {(int)statusCode} ({statusCode}) - {reasonPhrase}");
            }
        }

        public async Task<T> DeleteAsync<T>(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                var statusCode = response.StatusCode;
                var reasonPhrase = response.ReasonPhrase;
                throw new HttpRequestException($"HTTP request failed with status code: {(int)statusCode} ({statusCode}) - {reasonPhrase}");
            }
        }
    }
}
