using Microsoft.AspNetCore.Mvc;
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

        public ClientController(ILogger<ClientController> logger, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
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
        public async Task<int> Delete(string email)
        {
            var result = _clientRepository.Delete(email);

            return await Task.FromResult(0);
        }
    }
}