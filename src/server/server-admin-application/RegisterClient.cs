using Ardalis.Result;
using MediatR;
using server_admin_core.Context;
using server_admin_core.Models;

namespace server_admin_application;

public class RegisterClient
{
    public class Command : IRequest<Result<string>>
    {
        public string Name { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly AdministrationContext _context;

        public Handler(AdministrationContext context)
        {
            _context = context;
        }

        public Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = _context.Clients.SingleOrDefault(c => c.Name == request.Name);

            if (client is not null)
            {
                return Task.FromResult(
                    Result<string>.Error("Client already exists with this name"));
            }

            var token = GenerateToken();

            _context.Clients.Add(
                new Client
                {
                    Name = request.Name,
                    Token = token
                });

            _context.SaveChanges();

            return Task.FromResult(Result<string>.Success(token));
        }

        private string GenerateToken()
        {
            return "1234";
        }
    }
}