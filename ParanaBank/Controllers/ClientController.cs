using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParanaBank.Application.Commands.Delete;
using ParanaBank.Application.Models;
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
        public async Task<int> Post(ClientModel clientModel)
        {
            return await Task.FromResult(0);
        }

        [HttpGet]
        public async Task<int> Get()
        {

            var result = _clientRepository.GetAll();

           return await Task.FromResult(0);
        }

        [HttpGet]
        [Route("{email}")]
        public async Task<int> GetByEmail(string email)
        {

            var result = _clientRepository.GetByEmail(email);

            return await Task.FromResult(0);
        }

        [HttpPut]
        public async Task<int> Update([FromBody] ClientModel client)
        {
            return await Task.FromResult(0);
        }


        [HttpDelete]
        [Route("{email}")]
        public async Task<IActionResult> Delete(string email)
        {

            var command = new DeleteCommand(email);

            var xxx = await _mediator.Send(command);

            var result = _clientRepository.Delete(email);

            return StatusCode(200, result);
        }
    }
}