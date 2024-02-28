using Newtonsoft.Json;

namespace NoteShare.Models.Auth
{
    public class LoginResponseDto
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("result")]
        public AuthResponseDto Result { get; set; }
    }
}
