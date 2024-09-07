using Carter;
using CommunityConnect.Data;
using CommunityConnect.Features.Resident.Contracts;
using CommunityConnect.Features.Resident.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("AuthService", client =>
{
    client.BaseAddress = new Uri("http://localhost:5001/");
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddCarter();
builder.Services.AddScoped<IComplaint, ComplaintService>();
builder.Services.AddDbContext<CommunityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

var app = builder.Build();

app.MapCarter();

app.Run();