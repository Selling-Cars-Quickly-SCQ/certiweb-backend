using CertiWeb.API.Shared.Domain.Repositories;
using CertiWeb.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CertiWeb.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using CertiWeb.API.Users.Application.Internal.CommandServices;
using CertiWeb.API.Users.Application.Internal.QueryServices;
using CertiWeb.API.Users.Domain.Repositories;
using CertiWeb.API.Users.Domain.Services;
using CertiWeb.API.Users.Infrastructure.Persistence.EFC.Repositories;
using CertiWeb.API.Reservation.Application.Internal.CommandServices;
using CertiWeb.API.Reservation.Application.Internal.QueryServices;
using CertiWeb.API.Reservation.Domain.Repositories;
using CertiWeb.API.Reservation.Domain.Services;
using CertiWeb.API.Reservation.Infrastructure.Persistence.EFC.Repositories;
using CertiWeb.API.Certifications.Application.Internal.CommandServices;
using CertiWeb.API.Certifications.Application.Internal.QueryServices;
using CertiWeb.API.Certifications.Domain.Repositories;
using CertiWeb.API.Certifications.Domain.Services;
using CertiWeb.API.Certifications.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "CertiWeb.API",
            Version = "v1",
            Description = "CertiWeb Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Certi Web",
                Email = "contact@acme.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.EnableAnnotations();
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Users Bounded Context
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandServiceImpl>();
builder.Services.AddScoped<IUserQueryService, UserQueryServiceImpl>();

// Reservation Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationCommandService, ReservationCommandServiceImpl>();
builder.Services.AddScoped<IReservationQueryService, ReservationQueryServiceImpl>();

// Certifications Bounded Context Dependency Injection Configuration
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ICarCommandService, CarCommandServiceImpl>();
builder.Services.AddScoped<ICarQueryService, CarQueryServiceImpl>();
builder.Services.AddScoped<BrandQueryServiceImpl>();

var app = builder.Build();

// Verify if the database exists and create it if it doesn't
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Add Authorization Middleware to Pipeline
//app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();