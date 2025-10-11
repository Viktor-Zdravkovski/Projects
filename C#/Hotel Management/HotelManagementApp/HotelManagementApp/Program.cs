using HotelManagement.DataBase.Context;
using HotelManagement.Helpers;
using HotelManagement.Mappers;
using HotelManagement.Shared.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Load application settings
var hotelManagementSettings = builder.Configuration.GetSection("HotelManagementAppSettings");
builder.Services.Configure<HotelManagementAppSettings>(hotelManagementSettings);
HotelManagementAppSettings hotelAppSettings = hotelManagementSettings.Get<HotelManagementAppSettings>();

// Configure Swagger for API documentation
builder.Services.ConfigureSwagger();

// Add DbContext with SQL Server configuration
builder.Services.AddDbContext<HotelManagementDbContext>(options =>
    options.UseSqlServer(hotelAppSettings.ConnectionString));

// Program.cs / Startup.cs
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

// Register repositories and services via dependency injection
DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

// Configure authentication (assuming JWT or other method)
builder.Services.ConfigureAuthentication(hotelAppSettings.SecretKey);

// Configure CORS policy (adjust as needed for your app)
builder.Services.ConfigureCORSPolicy();

// Add controllers for MVC
builder.Services.AddControllers();

// Add Swagger support for API exploration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard middleware setup
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();
