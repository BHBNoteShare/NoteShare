using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NoteShare.CL.Services
{
    public interface IAPIService
    {
        Task<T> PostAsync<T>(string endpoint, object data);
    }
    internal class APIService : IAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public APIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["ApiBaseUrl"]);
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
                // Handle error response
                return default; // Or throw an exception
            }
        }
    }
}
