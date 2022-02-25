using MediatR;
using Microsoft.EntityFrameworkCore;
using server_admin_application.Infrastructure;
using server_admin_core.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPooledDbContextFactory<AdministrationContext>(cfg =>
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("administration"))
        .LogTo(Console.WriteLine)
);

builder.Services.AddMediatR(typeof(ApplicationRegistration).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
