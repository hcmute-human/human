using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.Jobs.CreateJob;

public class CreateJobCommand : ICommand<Result<Job>>
{
    public required Guid CreatorId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required JobStatus Status { get; set; }
    public required Guid PositionId { get; set; }
}

[Mapper]
internal static partial class CreateJobCommandMapper
{
    public static partial Job ToJob(this CreateJobCommand command);
}
