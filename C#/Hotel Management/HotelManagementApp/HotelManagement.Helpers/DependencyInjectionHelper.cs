using HotelManagement.DataBase.Implementations.EFImplementations;
using HotelManagement.DataBase.Interfaces;
using HotelManagement.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Reservation>, ReservationRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        //public static void InjectService(this IServiceCollection services)
        //{

        //}
    }
}
