using NoteShare.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NoteShare.CL.Services
{
    internal class AuthService : IAuthService
    {
        private string _baseUrl = "https://localhost:7183/api/";
        
        public async Task<AuthResponseDto> Login(LoginDto ldto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = await client.PostAsJsonAsync("Auth/login", ldto);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public Task<AuthResponseDto> Register(RegisterDto rdto)
        {
            throw new NotImplementedException();
        }
    }
}
