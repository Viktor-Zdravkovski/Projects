using FlashGameCofe.DataBase.Context;
using FlashGameCofe.Helpers;
using FlashGameCofe.Shared.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var flashGameCofeSettings = builder.Configuration.GetSection("FlashGameAppSettings");
builder.Services.Configure<FlashGameAppSettings>(flashGameCofeSettings);
FlashGameAppSettings flashGameCofeAppSettings = flashGameCofeSettings.Get<FlashGameAppSettings>();

builder.Services.ConfigureSwagger();

builder.Services.AddDbContext<FlashGameCofeDbContext>(options =>
    options.UseSqlServer(flashGameCofeAppSettings.ConnectionString));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

builder.Services.ConfigureAuthentication(flashGameCofeAppSettings.SecretKey);

builder.Services.ConfigureCORSPolicy();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
