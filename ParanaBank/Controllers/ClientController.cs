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
            var result = await _mediator.Send(new CreateCommand(client));

            return StatusCode(result.Status);
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> Get()
        {
            var result = await _clientRepository.GetAll();

           return result;
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<Client> GetByEmail(string email)
        {

            var result = await _clientRepository.GetByEmail(email);

            return result;
        }

        [HttpPut]
        public async Task<int> Update([FromBody] ClientModel client)
        {
            var result = await _mediator.Send(new UpdateCommand(client));

            return await Task.FromResult(0);
        }


        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> Delete(string email)
        {

            var result = await _mediator.Send(new DeleteCommand(email));

            return StatusCode(result.Status);
        }
    }
}