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
        Task<IEnumerable<Client>> GetAll();
        Task Add(Client client);
        Task Update(Client client);
        Task Delete(string email);
    }
}
