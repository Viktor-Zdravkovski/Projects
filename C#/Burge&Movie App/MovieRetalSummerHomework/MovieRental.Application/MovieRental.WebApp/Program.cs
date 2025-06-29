using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieRental.DataBase;
using MovieRental.DataBase.EFImplementations;
using MovieRental.DataBase.Interfaces;
using MovieRental.Domain;
using MovieRental.Service;
using MovieRental.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("MovieRentalConnectionString");

builder.Services.AddDbContext<MovieRentalDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddSession();

builder.Services.AddTransient<IRepository<Movie>, EFMovieRepository>();
builder.Services.AddTransient<IRepository<User>, EFUserRepository>();
builder.Services.AddTransient<IRepository<Rental>, EFRentalRepository>();

builder.Services.AddTransient<IFilterService, FilterService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRentalService, RentalService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });


var app = builder.Build();
app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/login");
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
