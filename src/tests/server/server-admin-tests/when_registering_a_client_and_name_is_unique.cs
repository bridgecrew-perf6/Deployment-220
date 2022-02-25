using Ardalis.Result;
using FluentAssertions;
using server_admin_application;
using Xunit;

namespace server_admin_tests;

public class when_registering_a_client_and_name_is_unique
{
    private RegisterClient.Handler Subject;
    private Result<string> Result;

    public when_registering_a_client_and_name_is_unique()
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
}