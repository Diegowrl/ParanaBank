using MediatR;
using ParanaBank.Application.Models;

namespace ParanaBank.Application.Commands.Update
{
    public class UpdateCommand : IRequest<ResultCommand>
    {
        public UpdateCommand(ClientModel client)
        {
            Client = client;
        }
        public ClientModel Client { get; }
    }
}