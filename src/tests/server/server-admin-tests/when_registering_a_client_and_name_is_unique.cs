using Ardalis.Result;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using server_admin_application;
using server_admin_core.Context;
using Xunit;

namespace server_admin_tests;

public class when_registering_a_client_and_name_is_unique
{
    private RegisterClient.Handler Subject = null!;
    private Result<string> Result = null!;
    private AdministrationContext Context = null!;

    public when_registering_a_client_and_name_is_unique()
    {
        Arrange();

        Act();
    }

    private void Arrange()
    {
        var options = new DbContextOptionsBuilder<AdministrationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Context = new AdministrationContext(options);

        Subject = new RegisterClient.Handler(Context);
    }

    private void Act()
    {
        Result = Subject.Handle(new RegisterClient.Command()
        {
            Name = "UniqueName"
        }, CancellationToken.None).GetAwaiter().GetResult();
    }

    [Fact]
    public void registration_is_successful()
    {
        Result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void client_registration_number_is_returned()
    {
        Result.Value.Should().NotBe(string.Empty);
    }

    [Fact]
    public void client_registration_is_stored()
    {
        Context.Clients.SingleOrDefault(c => c.Token == Result.Value)
            .Should()
            .NotBeNull();
    }
}