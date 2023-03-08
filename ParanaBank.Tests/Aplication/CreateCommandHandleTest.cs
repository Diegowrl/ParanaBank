using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParanaBank.Application.Commands.Create;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Tests.Aplication
{
    public class CreateCommandHandleTest
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<ILogger<CreateCommandHandle>> _ilogger;
        public CreateCommandHandleTest()
        {
            this._clientRepository = new Mock<IClientRepository>();
            this._ilogger = new Mock<ILogger<CreateCommandHandle>>();
        }

        [Fact]
        public async Task HandleShouldBeCreated()
        {
            var client = new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" };

            this._clientRepository.Setup(m => m.Add(client));
            this._clientRepository.Setup(m => m.GetByEmailOrUser(client));

            var controller = new CreateCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new CreateCommand(new ClientModel { User = "Diego", Email = "Diegowrl@hotmail.com" }), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.Add(It.IsAny<Client>()), Times.Once);
            _clientRepository.Verify(m => m.GetByEmailOrUser(It.IsAny<Client>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task HandleShouldBeFound()
        {
            var client = new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" };
            var taskClient = Task.FromResult<Client>(client);

            this._clientRepository.Setup(m => m.Add(client));
            this._clientRepository.Setup(m => m.GetByEmailOrUser(It.IsAny<Client>())).Returns(taskClient);

            var controller = new CreateCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new CreateCommand(new ClientModel { User = "Diego", Email = "Diegowrl@hotmail.com" }), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.Add(It.IsAny<Client>()), Times.Never);
            _clientRepository.Verify(m => m.GetByEmailOrUser(It.IsAny<Client>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status302Found);
        }

        [Fact]
        public async Task HandleShouldBeException()
        {
            var client = new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" };

            this._clientRepository.Setup(m => m.Add(It.IsAny<Client>())).ThrowsAsync(new Exception());
            this._clientRepository.Setup(m => m.GetByEmailOrUser(client));

            var controller = new CreateCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new CreateCommand(new ClientModel { User = "Diego", Email = "Diegowrl@hotmail.com" }), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.Add(It.IsAny<Client>()), Times.Once);
            _clientRepository.Verify(m => m.GetByEmailOrUser(It.IsAny<Client>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
