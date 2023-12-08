using FastEndpoints;
using FluentResults;
using Human.Domain.Constants;
using Human.Domain.Models;
using NodaTime;
using Riok.Mapperly.Abstractions;

namespace Human.Core.Features.LeaveApplications.UpdateLeaveApplication;

public class UpdateLeaveApplicationCommand : ICommand<Result<LeaveApplication>>
{
    public required Guid Id { get; set; }
    public required Guid IssuerId { get; set; }
    public required Guid LeaveTypeId { get; set; }
    public required Instant StartTime { get; set; }
    public required Instant EndTime { get; set; }
    public required LeaveApplicationStatus Status { get; set; }
    public string? Description { get; set; }
    public Guid? ProcessorId { get; set; }
}

[Mapper]
internal static partial class UpdateLeaveApplicationCommandMapper
{
    public static partial LeaveApplication ToLeaveApplication(this UpdateLeaveApplicationCommand command);
}
