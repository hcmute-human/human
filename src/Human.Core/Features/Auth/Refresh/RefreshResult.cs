namespace Human.Core.Features.Auth.Refresh;

public class RefreshResult
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
