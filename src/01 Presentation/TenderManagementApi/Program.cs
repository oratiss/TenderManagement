using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using TenderManagementApi.ProgramExtensions;
using TenderManagementDAL.Contexts;
using TenderManagementDAL.Models;
using TenderManagementService.AuthenticationServices;
using TenderManagementService.BidServices;
using TenderManagementService.CategoryServices;
using TenderManagementService.StatusServices;
using TenderManagementService.TenderServices;
using TenderManagementService.VendorServices;

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

string _allowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _allowSpecificOrigins, cp =>
    {
        cp.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Identity + EF
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<TenderManagementDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // Add this line
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
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

builder.Services.AddHttpContextAccessor();


// Register your AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection")!;

//add write sql services
builder.Services.AddSqlServer(connectionString);
builder.Services.AddUnitOfWorks();

//add read sql services
builder.Services.AddDapperConnectionAndRepos(connectionString);

//add business layer services
builder.Services.AddScoped<ITenderService, TenderService>();
builder.Services.AddScoped<IVendorService, VendorService>();
builder.Services.AddScoped<IBidService, BidService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStatusService, StatusService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.AddSwagger();

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(builder =>
{
    builder.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
});

// Run data seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await services.SeedData();
}

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Trade Management API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

