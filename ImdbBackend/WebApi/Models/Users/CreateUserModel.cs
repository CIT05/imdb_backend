namespace WebApi.Models.Users
{
    public class CreateUserModel
    {

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Language { get; set; } = string.Empty;

    }
}
