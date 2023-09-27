namespace Human.Infrastructure.Models;

public sealed class SmtpOptions
{
    public const string Section = "Smtp";

    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string SenderName { get; set; }
    public required string SenderAddress { get; set; }
}
