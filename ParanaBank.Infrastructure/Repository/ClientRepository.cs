using Dapper;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;
using ParanaBank.Infrastructure.Query;
using System.Data;

namespace ParanaBank.Infrastructure.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection Connection;

        public ClientRepository(IDbConnection connection)
        {
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
           return await Connection.QueryAsync<Client>(ClientQuery.SQL_GET_ALL);
        }

        public async Task<Client> GetByEmail(string email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            return await Connection.QueryFirstAsync<Client>(ClientQuery.SQL_GET_BY_EMAIL, parameters);
        }
        public async Task Add(Client client)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", client.Email, DbType.String);
            parameters.Add("@User", client.User, DbType.String);
            parameters.Add("@CreatedAt", client.CreatedAt, DbType.DateTime);
            parameters.Add("@Id", Guid.NewGuid(), DbType.Guid);

            await Connection.ExecuteAsync(ClientQuery.SQL_GET_BY_EMAIL, parameters);
        }

        public async Task Delete(string email)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            await Connection.ExecuteAsync(ClientQuery.SQL_DELETE_BY_EMAIL, parameters);
        }

        public async Task Update(Client client)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Email", client.Email, DbType.String);
            parameters.Add("@User", client.User, DbType.String);
            parameters.Add("@UpdatedAt", DateTime.Now, DbType.DateTime);
            await Connection.ExecuteAsync(ClientQuery.SQL_UPDATE, parameters);
        }
    }
}
