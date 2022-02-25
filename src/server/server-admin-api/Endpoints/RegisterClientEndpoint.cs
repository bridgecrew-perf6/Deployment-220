using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using server_admin_application;

namespace server_admin_api.Endpoints;

[Route("client")]
public class RegisterClientEndpoint : EndpointBaseAsync
    .WithRequest<RegisterClient.Command>
    .WithActionResult<string>
{
    private readonly IMediator _mediator;

    public RegisterClientEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public override async Task<ActionResult<string>> HandleAsync(RegisterClient.Command request, CancellationToken cancellationToken = new CancellationToken())
    {
        return await Task.FromResult(Ok("Hello World"));
    }
}
