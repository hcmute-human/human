namespace Human.Domain.Models;

public record class TextChoice : Choice
{
    public string Text { get; set; } = string.Empty;
}
