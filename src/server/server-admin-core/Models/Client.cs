namespace server_admin_core.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Token { get; set; } = null!;
}