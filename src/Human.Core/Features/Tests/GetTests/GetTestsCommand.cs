using FastEndpoints;
using FluentResults;
using Human.Core.Models;

namespace Human.Core.Features.Tests.GetTests;

public class GetTestsCommand : Collective, ICommand<Result<GetTestsResult>>
{
    public string? Name { get; set; }
    public bool? CountOnly { get; set; }
}
