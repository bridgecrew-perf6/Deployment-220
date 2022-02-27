using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using Xunit;

namespace server_admin_tests;

public class when_generating_a_token
{
    public ClientTokenGenerator Subject = null!;

    public when_generating_a_token()
    {
        Arrange();
    }

    private void Arrange()
    {
        Subject = new ClientTokenGenerator();
    }

    [Fact]
    public void token_is_not_empty()
    {
        var token = Subject.GenerateToken("hello", "world");

        token.Length.Should().NotBe(0);
    }

    [Fact]
    public void token_can_be_verified()
    {
        var token = Subject.GenerateToken("hello", "world");

        Subject.VerifyToken(token, "hello", "world").Should().BeTrue();
    }
}

public class ClientTokenGenerator
{
    public string GenerateToken(params object[] values)
    {
        return GetHashString(Prepare(values));
    }

    public bool VerifyToken(string token, params object[] values)
    {
        var hash = GetHashString(Prepare(values));

        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        return comparer.Compare(hash, token) == 0;
    }

    private byte[] GetHash(string inputString)
    {
        using HashAlgorithm algorithm = SHA256.Create();

        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    private string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();

        foreach (byte b in GetHash(inputString))
            sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    private static string Prepare(params object[] input)
    {
        return string.Join("_", input);
    }
}