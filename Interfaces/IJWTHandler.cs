namespace login_app.Interfaces
{
    public interface IJWTHandler
    {
        string? GenerateJwtToken(string? userId, string? userName);
    }
}
