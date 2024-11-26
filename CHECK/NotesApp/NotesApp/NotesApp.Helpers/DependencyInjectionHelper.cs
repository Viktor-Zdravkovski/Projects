﻿using Microsoft.Extensions.DependencyInjection;
using NotesApp.DataAccess;
using NotesApp.DataAccess.Implementations.AdoNetImplementation;
using NotesApp.DataAccess.Implementations.DapperImplementation;
using NotesApp.DataAccess.Implementations.EFImplementation;
using NotesApp.DataAccess.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Services.Implementation;
using NotesApp.Services.Interfaces;

namespace NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
