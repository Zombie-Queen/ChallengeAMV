using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Infrastructure.Persistence;
using AMV.TourReservation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMV.TourReservation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLServer");
            service.AddDbContext<TourReservationDbContext>(dbOption => dbOption.UseSqlServer(connectionString));
            return service;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection service)
        {
            // Registro de repositorios
            service.AddScoped<IReservationRepository, ReservationRepository>();
            service.AddScoped<ITourRepository, TourRepository>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return service;
        }
    }
}
