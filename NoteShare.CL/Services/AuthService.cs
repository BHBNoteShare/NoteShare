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
    public interface IAuthService
    {

        Task<LoginResponseDto> Login(LoginDto ldto);
        Task<AuthResponseDto> Register(RegisterDto rdto);
    }
    internal class AuthService : IAuthService
    {
		private readonly IAPIService _apiService;
        private readonly ISecureStorageService _secureStorage;

        public AuthService(IAPIService apiService, ISecureStorageService secureStorage)
        {
            _apiService = apiService;
            _secureStorage = secureStorage;
        }

		public async Task<LoginResponseDto> Login(LoginDto ldto)
        {
            LoginResponseDto lrdto = await _apiService.PostAsync<LoginResponseDto>("Auth/login", ldto);
            await _secureStorage.SetTokenAsync(lrdto.Result.Token);
            return lrdto;
        }
        /*
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
         */

        public Task<AuthResponseDto> Register(RegisterDto rdto)
        {
            throw new NotImplementedException();
        }
    }
}
