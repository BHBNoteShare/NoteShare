using NoteShare.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteShare.CL.Services
{
    public interface IAuthService
    {

        Task<AuthResponseDto> Login(LoginDto ldto);
        Task<AuthResponseDto> Register(RegisterDto rdto);
    }
}
