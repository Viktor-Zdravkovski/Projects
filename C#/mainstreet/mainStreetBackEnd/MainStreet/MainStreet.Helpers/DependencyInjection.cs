using MainStreet.DataBase.Interfaces;
using MainStreet.Domain.Models;
using MainStreet.Services.Implementations;
using MainStreet.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MainStreet.Helpers
{
    public static class DependencyInjection
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Order>, IOrderRepository>();
            services.AddTransient<IRepository<Payment>, IPaymentRepository>();
            services.AddTransient<IRepository<User>, IUserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
