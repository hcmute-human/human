using FastEndpoints;
using Human.Core.Features.Users.CreateUser;
using Human.Core.Interfaces;
using Human.Domain.Constants;
using Human.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Human.WebServer.Api.V1.Employees.CreateEmployee;

using Results = Results<CreatedAtEndpoint<GetEmployee.Endpoint, Response>, ProblemDetails>;

internal sealed class Endpoint : Endpoint<Request, Results>
{
    private readonly IAppDbContext dbContext;

    public Endpoint(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public override void Configure()
    {
        Permissions(Permit.CreateEmployee);
        Post("employees");
        Verbs(Http.POST);
        Version(1);
    }

    public override async Task<Results> ExecuteAsync(Request request, CancellationToken ct)
    {
        User? user;
        if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
        {
            var createUserResult = await new CreateUserCommand { Email = request.Email, Password = request.Password }
                .ExecuteAsync(ct)
                .ConfigureAwait(false);
            if (createUserResult.IsFailed)
            {
                return this.ProblemDetails(createUserResult.Errors);
            }
            user = createUserResult.Value;
        }
        else
        {
            user = await dbContext.Users
                .Where(x => x.Id == request.UserId)
                .Select(x => new User { Id = x.Id })
                .FirstOrDefaultAsync(ct)
                .ConfigureAwait(false);
            if (user is null)
            {
                AddError(x => x.UserId!, "User does not exist", "invalid_id");
                return new ProblemDetails(ValidationFailures);
            }
        }

        var result = await request.ToCommand(user).ExecuteAsync(ct).ConfigureAwait(false);
        if (result.IsFailed)
        {
            return this.ProblemDetails(result.Errors);
        }
        return this.CreatedAt<GetEmployee.Endpoint, Response>(new { result.Value.Id }, result.Value.ToResponse());
    }
}
