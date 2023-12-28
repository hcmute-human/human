using FastEndpoints;
using FluentResults;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Human.Core.Features.Jobs.GetJobs;

public sealed class GetJobsHandler(IAppDbContext dbContext) : ICommandHandler<GetJobsCommand, Result<GetJobsResult>>
{
    public async Task<Result<GetJobsResult>> ExecuteAsync(GetJobsCommand command, CancellationToken ct)
    {
        var query = dbContext.Jobs.AsQueryable();
        if (command.Title is not null)
        {
            query = query.Where(x => EF.Functions.ILike(x.Title, '%' + command.Title + '%'));
        }
        if (!string.IsNullOrEmpty(command.PositionName))
        {
            query = query.Where(x => EF.Functions.ILike(x.Position.Name, '%' + command.PositionName + '%'));
        }
        if (!string.IsNullOrEmpty(command.DepartmentName))
        {
            query = query.Where(x => EF.Functions.ILike(x.Position.Department.Name, '%' + command.DepartmentName + '%'));
        }
        if (command.ExcludeDescription == true)
        {
            query = query.Select(x => new Job
            {
                Id = x.Id,
                CreatedTime = x.CreatedTime,
                UpdatedTime = x.UpdatedTime,
                CreatorId = x.CreatorId,
                PositionId = x.PositionId,
                Position = command.IncludePosition == true ? new DepartmentPosition
                {
                    Id = x.Position.Id,
                    CreatedTime = x.Position.CreatedTime,
                    UpdatedTime = x.Position.UpdatedTime,
                    DepartmentId = x.Position.DepartmentId,
                    Name = x.Position.Name,
                    Department = command.IncludeDepartment == true ? x.Position.Department : null!,
                } : null!,
                Status = x.Status,
                Title = x.Title,
            });
        }
        else
        {
            query = (command.IncludePosition, command.IncludeDepartment) switch
            {
                (true, true) or (false, true) => query.Include(x => x.Position).ThenInclude(x => x.Department),
                (true, false) => query.Include(x => x.Position),
                _ => query,
            };
        }

        var totalCount = await query.CountAsync(ct).ConfigureAwait(false);
        if (command.CountOnly == true)
        {
            return new GetJobsResult
            {
                TotalCount = totalCount,
                Items = Array.Empty<Job>(),
            };
        }

        query = Array.Find(command.Order, x => x.Name.Equals(nameof(command.PositionName), StringComparison.OrdinalIgnoreCase))?.Sort(query, x => x.Position.Name) ?? query;
        query = Array.Find(command.Order, x => x.Name.Equals(nameof(command.DepartmentName), StringComparison.OrdinalIgnoreCase))?.Sort(query, x => x.Position.Department.Name) ?? query;
        query = command.Order
            .Where(x => !x.Name.EqualsEither([nameof(command.PositionName), nameof(command.DepartmentName)], StringComparison.OrdinalIgnoreCase))
            .SortOrDefault(query, x => x.OrderBy(x => x.CreatedTime));
        var jobs = await query
            .Skip(command.Offset)
            .Take(command.Size)
            .ToArrayAsync(ct).ConfigureAwait(false);
        return new GetJobsResult
        {
            TotalCount = totalCount,
            Items = jobs,
        };
    }
}
