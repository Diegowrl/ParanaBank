using ParanaBank.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBank.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Client> GetByEmail(string email);
        Task<Client> GetByEmailAndUser(Client client);
        Task<Client> GetByEmailOrUser(Client client);
        Task<IEnumerable<Client>> GetAll();
        Task Add(Client client);
        Task UpdateByEmailAndUser(Client client);
        Task DeleteByEmail(string email);
    }
}
