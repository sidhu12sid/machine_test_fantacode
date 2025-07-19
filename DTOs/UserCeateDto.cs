namespace login_app.DTOs
{
    public class UserCeateDto
    {
        public string? Username {  get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }

    public class UserLogin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseModel
    {
        public string? AccessToken { get; set; }
        public string? UserName { get; set; }
    }
}
