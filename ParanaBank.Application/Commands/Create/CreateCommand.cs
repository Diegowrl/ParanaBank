using MediatR;
using ParanaBank.Application.Models;

namespace ParanaBank.Application.Commands.Create
{
    public class CreateCommand : IRequest<ResultCommand>
    {
        public CreateCommand(ClientModel client)
        {
            Client = client;
        }
        public ClientModel Client { get; }
    }
}