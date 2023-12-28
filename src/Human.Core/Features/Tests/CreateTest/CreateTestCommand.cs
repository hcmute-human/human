using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Tests.CreateTest;

public class CreateTestCommand : ICommand<Result<Test>>
{
    public required Guid CreatorId { get; set; }
    public required Guid JobId { get; set; }
    public required string Name { get; set; }
}

[Mapper]
internal static partial class CreateTestCommandMapper
{
    public static partial Test ToTest(this CreateTestCommand command);
}
