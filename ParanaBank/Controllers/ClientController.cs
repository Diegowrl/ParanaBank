using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParanaBank.Application.Commands.Create;
using ParanaBank.Application.Commands.Delete;
using ParanaBank.Application.Commands.Update;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {

        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _clientRepository;
        private readonly IMediator _mediator;

        public ClientController(ILogger<ClientController> logger, IClientRepository clientRepository, IMediator mediator)
        {
            _logger = logger;
            _clientRepository = clientRepository;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClientModel client)
        {
            _logger.LogInformation($"Request receive to to register user {client.User}");

            var result = await _mediator.Send(new CreateCommand(client));

            return StatusCode(result.Status, result.Message);
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            _logger.LogInformation($"Request receive to get all clients on Datetime {DateTime.Now} ");

            var result = await _clientRepository.GetAll();

           return result;
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<Client> GetByEmail(string email)
        {
            _logger.LogInformation($"Email {email} was receive to get on Database");

            var result = await _clientRepository.GetByEmail(email);

            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ClientModel client)
        {
            _logger.LogInformation($"Request receive to to update user {client.User}");

            var result = await _mediator.Send(new UpdateCommand(client));

            return StatusCode(result.Status, result.Message);
        }


        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            _logger.LogInformation($"Request receive to to delete user {email}");

            var result = await _mediator.Send(new DeleteCommand(email));

            return StatusCode(result.Status, result.Message);
        }
    }
}