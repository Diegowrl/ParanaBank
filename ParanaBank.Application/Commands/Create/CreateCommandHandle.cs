using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using ParanaBank.Application.Commands.Delete;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Application.Commands.Create
{
    public class CreateCommandHandle : IRequestHandler<CreateCommand, ResultCommand>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCommandHandle> _logger;

        public CreateCommandHandle(IClientRepository clientRepository, IMapper mapper, ILogger<CreateCommandHandle> logger)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultCommand> Handle(CreateCommand command, CancellationToken cancellationToken)
        {

            var client = new Client { UserName = command.Client.User , Email = command.Client.Email };

            if (_clientRepository.GetByEmailOrUser(client).Result is not null)
            {
                _logger.LogError("User allready found by email or user");
                return ResultCommand.Ok("User allready found by email or user");
            }

            client.CreateUser();

            try
            {
                await _clientRepository.Add(client);
            }
            catch (Exception)
            {
                _logger.LogError("Error to add user on database");
                return ResultCommand.Error();
            }


            return ResultCommand.Ok();
        }
    }
}
