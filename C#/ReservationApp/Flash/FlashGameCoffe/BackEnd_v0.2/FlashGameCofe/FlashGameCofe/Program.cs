using FlashGameCofe.DataBase.Context;
using FlashGameCofe.Helpers;
using FlashGameCofe.Mappers;
using FlashGameCofe.Shared.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load application settings
var flashGameCofeSettings = builder.Configuration.GetSection("FlashGameAppSettings");
builder.Services.Configure<FlashGameAppSettings>(flashGameCofeSettings);
FlashGameAppSettings flashGameCofeAppSettings = flashGameCofeSettings.Get<FlashGameAppSettings>();

// Configure Swagger for API documentation
builder.Services.ConfigureSwagger();

// Add DbContext with SQL Server configuration
builder.Services.AddDbContext<FlashGameCofeDbContext>(options =>
    options.UseSqlServer(flashGameCofeAppSettings.ConnectionString));

// Register AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register repositories and services via dependency injection
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

// Configure authentication (assuming JWT or other method)
builder.Services.ConfigureAuthentication(flashGameCofeAppSettings.SecretKey);

// Configure CORS policy (adjust as needed for your app)
builder.Services.ConfigureCORSPolicy();

// Add controllers for MVC
builder.Services.AddControllers();

// Add Swagger support for API exploration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard middleware setup
app.UseHttpsRedirection();
app.UseRouting();



// Set up authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();
