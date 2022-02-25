using Ardalis.Result;
using FluentAssertions;
using Moq;
using server_admin_application;
using Xunit;

namespace server_admin_tests;

public class when_registering_a_client_with_non_unique_name
{
    private RegisterClient.Handler Subject;
    private Result<string> Result;

    public when_registering_a_client_with_non_unique_name()
    {
        Arrange();

        Act();
    }

    private void Arrange()
    {
        Subject = new RegisterClient.Handler();
    }

    private void Act()
    {
        Result = Subject.Handle(new RegisterClient.Command()
        {
            Name = "DuplicateName"
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
}