using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteShare.CL.Services
{
    public interface ISecureStorageService
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
        void RemoveToken();
    }
    internal class SecureStorageService : ISecureStorageService
    {
        public async Task<string> GetTokenAsync()
        {
            return await SecureStorage.GetAsync("token");
        }
        public async Task SetTokenAsync(string token)
        {
            await SecureStorage.SetAsync("token", token);
        }
        public void RemoveToken()
        {
            SecureStorage.Remove("token");
        }

       
    }
}
