using Microsoft.EntityFrameworkCore;
using TodoApplication.DataAccess;
using TodoApplication.DataAccess.EFImplementations;
using TodoApplication.DataAccess.Implementations;
using TodoApplication.DataAccess.Interfaces;
using TodoApplication.Domain;
using TodoApplication.Services;
using TodoApplication.Services.Interfaces;
using TodoApplication.WebApp.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

#region Register Database
string connectionString = builder.Configuration.GetConnectionString("TodoAppConnectionString");

builder.Services.AddDbContext<TodoAppDbContext>(options => options.UseSqlServer(connectionString));
#endregion


#region Register Repositories
builder.Services.AddTransient<ITodoRepository, EFTodoRepository>();
builder.Services.AddTransient<IRepository<Status>, EFStatusRepository>();
builder.Services.AddTransient<IRepository<Category>, EFCategoryRepository>();
#endregion


#region Register Services
builder.Services.AddTransient<ITodoService, TodoService>();
builder.Services.AddTransient<IFilterService, FilterService>();
#endregion

var app = builder.Build();

app.UseMiddleware<UrlLoggerMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
