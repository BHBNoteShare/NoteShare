using Newtonsoft.Json;

namespace NoteShare.Models.Auth
{
    public class LoginResponseDto
    {
        public string Message { get; set; }
        public AuthResponseDto Result { get; set; }
    }
}
