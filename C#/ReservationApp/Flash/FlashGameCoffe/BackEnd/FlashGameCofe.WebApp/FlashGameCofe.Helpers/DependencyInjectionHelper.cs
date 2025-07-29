using FlashGameCofe.DataBase.Implementations.EFImplementations;
using FlashGameCofe.DataBase.Interfaces;
using FlashGameCofe.Domain.Models;
using FlashGameCofe.Services.Implementations;
using FlashGameCofe.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FlashGameCofe.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<Reservation>, ReservationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
