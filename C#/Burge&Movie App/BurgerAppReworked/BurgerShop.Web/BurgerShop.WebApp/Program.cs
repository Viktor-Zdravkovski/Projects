using BurgerApp.Services;
using BurgerShop.DataBase;
using BurgerShop.DataBase.EFImplementations;
using BurgerShop.DataBase.Interfaces;
using BurgerShop.Domain;
using BurgerShop.Service;
using BurgerShop.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BurgerShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BurgerAppConnectionString")));



#region Register Services
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<IBurgerService, BurgerService>();
builder.Services.AddTransient<IOrderService, OrderService>();


#endregion
#region Register Repositories
builder.Services.AddTransient<IRepository<Location>, EFLocationRepository>();
builder.Services.AddTransient<IRepository<Burger>, EFBurgerRepository>();
builder.Services.AddTransient<IRepository<Order>, EFOrderRepository>();

#endregion

var app = builder.Build();

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

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<BurgerAppDbContext>();
//    context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS [dbo].[BurgerOrder]");
//}



app.Run();
