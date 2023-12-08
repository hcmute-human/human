using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.LeaveApplications.GetLeaveApplications;

public sealed class GetLeaveApplicationsHandler(IAppDbContext dbContext) : ICommandHandler<GetLeaveApplicationsCommand, Result<GetLeaveApplicationsResult>>
{
    private readonly IAppDbContext dbContext = dbContext;

    public async Task<Result<GetLeaveApplicationsResult>> ExecuteAsync(GetLeaveApplicationsCommand command, CancellationToken ct)
    {
        var query = dbContext.LeaveApplications.AsQueryable();
        if (command.IssuerId is not null)
        {
            query = query.Where(x => x.IssuerId == command.IssuerId);
        }
        if (!string.IsNullOrEmpty(command.IssuerName))
        {
            query = query.Where(x => (x.Issuer.FirstName + ' ' + x.Issuer.LastName).Contains(command.IssuerName));
        }
        if (!string.IsNullOrEmpty(command.AcquirerName))
        {
            query = query.Where(x => x.ProcessorId != null && (x.Processor!.FirstName + ' ' + x.Processor!.LastName).Contains(command.AcquirerName));
        }
        if (command.DepartmentId is not null)
        {
            query = query.Where(x => x.Issuer.Positions.Any(x => x.DepartmentPosition.DepartmentId == command.DepartmentId));
        }

        if (command.IncludeIssuer == true)
        {
            query = query.Include(x => x.Issuer);
        }
        if (command.IncludeLeaveType == true)
        {
            query = query.Include(x => x.LeaveType);
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        if (command.CountOnly == true)
        {
            return new GetLeaveApplicationsResult
            {
                TotalCount = totalCount,
                Items = Array.Empty<LeaveApplication>(),
            };
        }

        query = Array.Find(command.Order, x => x.Name.Equals("IssuerName", StringComparison.OrdinalIgnoreCase))?.Sort(query, x => x.Issuer.FirstName + " " + x.Issuer.LastName) ?? query;
        query = Array.Find(command.Order, x => x.Name.Equals("LeaveTypeName", StringComparison.OrdinalIgnoreCase))?.Sort(query, x => x.LeaveType.Name) ?? query;
        query = command.Order
            .Where(x => !x.Name.EqualsEither(["IssuerName", "LeaveTypeName"], StringComparison.OrdinalIgnoreCase))
            .SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));

        var leaveApplications = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetLeaveApplicationsResult
        {
            TotalCount = totalCount,
            Items = leaveApplications,
        };
    }
}
