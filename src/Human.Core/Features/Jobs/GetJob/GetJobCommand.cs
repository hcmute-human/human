using FastEndpoints;
using FluentResults;
using Human.Domain.Models;

namespace Human.Core.Features.Jobs.GetJob;

public sealed class GetJobCommand : ICommand<Result<Job>>
{
    public required Guid Id { get; set; }
}
