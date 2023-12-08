using FastEndpoints;
using FluentResults;
using Human.Core.Models;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.LeaveTypes.GetLeaveTypes;

public class GetLeaveTypesCommand : Collective, ICommand<Result<GetLeaveTypesResult>>
{
    public string? Name { get; set; }
}
