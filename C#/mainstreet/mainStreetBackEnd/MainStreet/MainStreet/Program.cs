using FlashGameCofe.Helpers;
using MainStreet.DataBase.Context;
using MainStreet.Helpers;
using MainStreet.Mappers;
using MainStreet.Shared.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load application settings
var mainStreetSettings = builder.Configuration.GetSection("FlashGameAppSettings");
builder.Services.Configure<MainStreetAppSettings>(mainStreetSettings);
MainStreetAppSettings mainStreetAppSettings = mainStreetSettings.Get<MainStreetAppSettings>();

// Configure Swagger for API documentation
builder.Services.ConfigureSwagger();

// Add DbContext with SQL Server configuration
builder.Services.AddDbContext<MainStreetDbContext>(options =>
    options.UseSqlServer(mainStreetAppSettings.ConnectionString));

// Register AutoMapper
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(config =>
{

}, typeof(MappingProfile));

// Register repositories and services via dependency injection
DependencyInjection.InjectRepositories(builder.Services);
DependencyInjection.InjectServices(builder.Services);

// Configure authentication (assuming JWT or other method)
builder.Services.ConfigureAuthentication(mainStreetAppSettings.SecretKey);

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
