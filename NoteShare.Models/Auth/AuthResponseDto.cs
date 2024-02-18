namespace NoteShare.Models.Auth
{
    public class AuthResponseDto
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Teacher,
        Student,
        Parent
    }
}
