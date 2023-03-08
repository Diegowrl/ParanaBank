using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Application.Commands.Create
{
    public class CreateCommandHandle : IRequestHandler<CreateCommand, ResultCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<CreateCommandHandle> _logger;

        public CreateCommandHandle(IClientRepository clientRepository, ILogger<CreateCommandHandle> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<ResultCommand> Handle(CreateCommand command, CancellationToken cancellationToken)
        {

            var client = new Client { UserName = command.Client.User , Email = command.Client.Email };

            if (_clientRepository.GetByEmailOrUser(client).Result is not null)
            {
                _logger.LogError("User allready found by email or user");
                return ResultCommand.Found("User allready found by email or user");
            }

            client.CreateUser();

            try
            {
                await _clientRepository.Add(client);

                return ResultCommand.Created("User created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResultCommand.Error("Error to add user on database");
            }
        }
    }
}
