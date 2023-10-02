namespace Human.Core.Features.Auth.Login;

public class LoginResult
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}