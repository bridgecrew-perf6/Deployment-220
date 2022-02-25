using Ardalis.Result;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using server_admin_application;
using server_admin_core.Context;
using server_admin_core.Models;
using Xunit;

namespace server_admin_tests;

public class when_registering_a_client_with_non_unique_name
{
    private RegisterClient.Handler Subject = null!;
    private Result<string> Result = null!;
    private AdministrationContext Context = null!;
    private readonly string _duplicateClientName = "DuplicateName";

    public when_registering_a_client_with_non_unique_name()
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

        ConfigureData();

        Subject = new RegisterClient.Handler(Context);
    }

    private void Act()
    {
        Result = Subject.Handle(new RegisterClient.Command()
        {
            Name = _duplicateClientName
        }, CancellationToken.None).GetAwaiter().GetResult();
    }

    [Fact]
    public void registration_is_not_successful()
    {
        Result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void message_is_returned_stating_client_exists_with_this_name()
    {
        Result.Errors.Count().Should().Be(1);

        Result.Errors.Should().Contain("Client already exists with this name");
    }

    private void ConfigureData()
    {
        Context.Clients.Add(new Client()
        {
            Id = 1,
            Name = _duplicateClientName,
            Token = "123"
        });

        Context.SaveChanges();
    }
}