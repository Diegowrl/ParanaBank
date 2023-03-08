using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Application.Commands.Update
{
    public class UpdateCommandHandle : IRequestHandler<UpdateCommand, ResultCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<UpdateCommandHandle> _logger;

        public UpdateCommandHandle(IClientRepository clientRepository, ILogger<UpdateCommandHandle> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<ResultCommand> Handle(UpdateCommand command, CancellationToken cancellationToken)
        {

            var client = new Client { UserName = command.Client.User, Email = command.Client.Email };

            if (_clientRepository.GetByEmailAndUser(client).Result is not null)
            {
                _logger.LogError("User allready registered by email or user");
                return ResultCommand.Found("User allready registered by email or user");
            }

            client.UpdateUser();

            try
            {
                await _clientRepository.UpdateByEmailAndUser(client);

                return ResultCommand.Ok("User Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultCommand.Error("Error to Update user on database");
            }
        }
    }
}
