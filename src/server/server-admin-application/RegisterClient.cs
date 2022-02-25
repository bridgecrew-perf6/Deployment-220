using Ardalis.Result;
using MediatR;

namespace server_admin_application;

public class RegisterClient
{
    public class Command : IRequest<Result<string>>
    {
        public string Name { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        public Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                Result<string>.Error("Client already exists with this name"));
        }
    }
}