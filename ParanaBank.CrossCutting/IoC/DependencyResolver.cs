using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ParanaBank.Application.Commands.Create;
using ParanaBank.Application.Commands.Delete;
using ParanaBank.Application.Commands.Update;
using ParanaBank.Domain.Interfaces;
using ParanaBank.Infrastructure.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ParanaBank.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterRepositories(services);
        }

        public static void AddMediatRApi(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DeleteCommandHandle).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCommandHandle).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateCommandHandle).GetTypeInfo().Assembly);
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IClientRepository, ClientRepository>();
        }

        public static void AddSqlServerConnection(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnection>(prov => new SqlConnection(connectionString));
        }
    }
}
