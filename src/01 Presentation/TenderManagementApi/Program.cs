using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TenderManagementApi.ProgramExtensions;
using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementService.AuthenticationServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}
else if (builder.Environment.IsProduction())
{
    builder.Configuration.AddJsonFile("appsettings.Production.json", optional: true, reloadOnChange: true);
}
else if (builder.Environment.IsStaging())
{
    builder.Configuration.AddJsonFile("appsettings.Staging.json", optional: true, reloadOnChange: true);
}

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection")!;

//add write sql services
builder.Services.AddSqlServer(connectionString);
builder.Services.AddUnitOfWorks();

//add read sql services
builder.Services.AddDapperConnectionAndRepos(connectionString);

// Identity + EF
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<TenderManagementDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Register your AuthService
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();





var app = builder.Build();

// Run data seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.SeedData();
}

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
