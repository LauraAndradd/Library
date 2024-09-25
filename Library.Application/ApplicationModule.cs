using Library.Application.Commands.AddBookCommand;
using Library.Application.Interfaces;
using Library.Application.Models;
using Library.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Library.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHandlers();
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<AddBookCommand>());
            return services;
        }
    }
}
