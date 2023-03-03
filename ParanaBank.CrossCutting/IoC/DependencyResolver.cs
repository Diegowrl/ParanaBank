using Microsoft.Extensions.DependencyInjection;
using ParanaBank.Domain.Interfaces;
using ParanaBank.Infrastructure.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace ParanaBank.CrossCutting.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterRepositories(services);
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
