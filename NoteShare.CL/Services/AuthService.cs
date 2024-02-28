using NoteShare.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NoteShare.CL.Services
{
    internal class AuthService : IAuthService
    {
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<HttpResponseMessage> Login(LoginDto ldto)
        {
            var response = await _httpClient.PostAsJsonAsync("Auth/login", ldto);
            var AuthResponse = JsonConvert.DeserializeObject<LoginResponseDto>(await response.Content.ReadAsStringAsync());
            var token = AuthResponse.Result.Token;
            if (response.IsSuccessStatusCode)
            {
                await SecureStorage.SetAsync("token", AuthResponse.Result.Token.ToString());

                //return AuthResponse
                return response;
            }
            else
            {
                return response;
            }
        }

        public Task<AuthResponseDto> Register(RegisterDto rdto)
        {
            throw new NotImplementedException();
        }
    }
}
