using FastEndpoints;
using FluentResults;

namespace Human.Core.Features.Jobs.DeleteJob;

public class DeleteJobCommand : ICommand<Result>
{
    public required Guid Id { get; set; }
}
