using FastEndpoints;
using FluentResults;
using Human.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.LeaveTypes.CreateLeaveType;

public class CreateLeaveTypeCommand : ICommand<Result<LeaveType>>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int Days { get; set; }
}

[Mapper]
internal static partial class CreateLeaveTypeCommandMapper
{
    public static partial LeaveType ToLeaveType(this CreateLeaveTypeCommand command);
}
