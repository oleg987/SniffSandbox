using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SniffSandbox;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<SniffDbContext>(opt => 
    opt.UseInMemoryDatabase("users_db"));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = true;
        opt.Password.RequiredLength = 6;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        
        opt.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<SniffDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();