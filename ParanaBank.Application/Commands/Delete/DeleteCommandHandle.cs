using MediatR;
using Microsoft.Extensions.Logging;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Application.Commands.Delete
{
    public class DeleteCommandHandle : IRequestHandler<DeleteCommand, ResultCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<DeleteCommandHandle> _logger;

        public DeleteCommandHandle(IClientRepository clientRepository, ILogger<DeleteCommandHandle> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<ResultCommand> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {

            var client = _clientRepository.GetByEmail(request.Email).Result;

            if (client is null)
            {
                _logger.LogError("User not found by email");
                return ResultCommand.NotFound("User not found by email");
            }

            try
            {
                await _clientRepository.DeleteByEmail(client.Email);
            }
            catch (Exception)
            {
                _logger.LogError("Error to delete user on database");
                return ResultCommand.Error();
            }


            return ResultCommand.Ok();
        }
    }
}
