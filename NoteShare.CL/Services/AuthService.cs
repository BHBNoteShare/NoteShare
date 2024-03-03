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

        public Task<AuthResponseDto> Register(RegisterDto rdto)
        {
            //TODO: Implementálni a Regisztráció metódust
            throw new NotImplementedException();
        }
    }
}
