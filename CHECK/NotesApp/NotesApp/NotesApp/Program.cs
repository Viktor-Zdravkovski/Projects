using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NotesApp.DataAccess.Context;
using NotesApp.Helpers;
using NotesApp.Shared.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===> Another approach for managing configurations
// Retrieve the "NotesAppSettings" section from appsettings.json
var notesSettings = builder.Configuration.GetSection("NotesAppSettings");
// Bind the "NotesAppSettings" section to the NotesAppSettings class using IOptions pattern
builder.Services.Configure<NotesAppSettings>(notesSettings);
// Directly use instance of NotesAppSettings in Program.cs
NotesAppSettings notesAppSettings = notesSettings.Get<NotesAppSettings>();

// ===> Configure Serilog
// Log.Logger => the globally shared logger
Log.Logger = ConfigurationsHelper.GetSerilogConfiguration(notesAppSettings.ConnectionString);
// Use Serilog as the logging provider
builder.Host.UseSerilog();

// ===> Register Database
//string connectionString = "Server=.\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;Integrated Security=True;Encrypt=False;"; BAD WAY
//string connectionString = builder.Configuration.GetConnectionString("NotesAppSqlExpress");
//builder.Services.AddDbContext<NotesAppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<NotesAppDbContext>(options => options.UseSqlServer(notesAppSettings.ConnectionString));

// ===> DEPENDENCY INJECTION
//DependencyInjectionHelper.InjectRepositories(builder.Services);
//DependencyInjectionHelper.InjectServices(builder.Services);
builder.Services.InjectRepositories();
builder.Services.InjectServices();

// ===> Configure JWT
builder.Services.ConfigureAuthentication(notesAppSettings.SecretKey);

// ===> Add CORS Policy
builder.Services.ConfigureCORSPolicy();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseSerilogRequestLogging(); // Logs every request

app.UseCors("AllowSpecificOrigins"); // Use the defined CORS policy
//app.UseCors("AllowAll"); // Use the "AllowAll" CORS policy

app.UseHttpsRedirection();

app.UseAuthentication(); // to be able to use JWT authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
