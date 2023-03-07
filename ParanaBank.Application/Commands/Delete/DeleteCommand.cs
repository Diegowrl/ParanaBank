using MediatR;
using ParanaBank.Application.Models;

namespace ParanaBank.Application.Commands.Delete
{
    public class DeleteCommand : IRequest<ResultCommand>
    {
        public DeleteCommand(string email)
        {
            Email = email;
        }
        public string Email { get; }
    }
}
