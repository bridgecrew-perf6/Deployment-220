using Microsoft.EntityFrameworkCore;
using server_admin_core.Models;

namespace server_admin_core.Context;

public class AdministrationContext : DbContext
{
    public DbSet<Client> Clients { get; set; } = null!;

    public AdministrationContext(DbContextOptions<AdministrationContext> options) 
        : base(options)
    { }
}