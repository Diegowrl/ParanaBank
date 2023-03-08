using Dapper;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;
using ParanaBank.Infrastructure.Query;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace ParanaBank.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection Connection;

        public ClientRepository(IDbConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
        public async Task Add(Client client)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", client.Email, DbType.String);
            parameters.Add("@User", client.UserName, DbType.String);
            parameters.Add("@CreatedAt", client.CreatedAt, DbType.DateTimeOffset);
            parameters.Add("@Id", client.Id, DbType.Guid);

            await Connection.ExecuteAsync(ClientQuery.SQL_ADD, parameters);
        }
        public async Task<IEnumerable<Client>> GetAll()
        {
           return await Connection.QueryAsync<Client>(ClientQuery.SQL_GET_ALL);
        }

        public async Task<Client> GetByEmail(string email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            var result = await Connection.QueryFirstOrDefaultAsync<Client>(ClientQuery.SQL_GET_BY_EMAIL, parameters);

            return result;
        }

        public async Task<Client> GetByEmailAndUser(Client client)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", client.Email, DbType.String);
            parameters.Add("@User", client.UserName, DbType.String);

            var result = await Connection.QueryFirstOrDefaultAsync<Client>(ClientQuery.SQL_GET_BY_EMAIL_AND_USER, parameters);

            return result;
        }

        public async Task<Client> GetByEmailOrUser(Client client)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", client.Email, DbType.String);
            parameters.Add("@User", client.UserName, DbType.String);

            var result = await Connection.QueryFirstOrDefaultAsync<Client>(ClientQuery.SQL_GET_BY_EMAIL_OR_USER, parameters);

            return result;
        }

        public async Task DeleteByEmail(string email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            await Connection.ExecuteAsync(ClientQuery.SQL_DELETE_BY_EMAIL, parameters);
        }
        public async Task UpdateByEmailAndUser(Client client)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", client.Email, DbType.String);
            parameters.Add("@User", client.UserName, DbType.String);
            parameters.Add("@UpdatedAt", DateTime.Now, DbType.DateTimeOffset);

            await Connection.ExecuteAsync(ClientQuery.SQL_UPDATE, parameters);
        }
    }
}
