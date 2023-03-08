using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using ParanaBank.Application.Commands.Update;
using ParanaBank.Application.Models;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Tests.Aplication
{
    public class UpdateCommandHandleTest
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<ILogger<UpdateCommandHandle>> _ilogger;
        public UpdateCommandHandleTest()
        {
            this._clientRepository = new Mock<IClientRepository>();
            this._ilogger = new Mock<ILogger<UpdateCommandHandle>>();
        }

        [Fact]
        public async Task HandleShouldBeOk()
        {
            var client = new ClientModel { User = "Diego", Email = "Diegowrl@hotmail.com" };

            this._clientRepository.Setup(m => m.GetByEmailAndUser(It.IsAny<Client>()));
            this._clientRepository.Setup(m => m.UpdateByEmailAndUser(It.IsAny<Client>()));

            var controller = new UpdateCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new UpdateCommand(client), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.GetByEmailAndUser(It.IsAny<Client>()), Times.Once);
            _clientRepository.Verify(m => m.UpdateByEmailAndUser(It.IsAny<Client>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task HandleShouldBeFound()
        {
            var client = new ClientModel { User = "Diego", Email = "Diegowrl@hotmail.com" };

            var taskClient = Task.FromResult<Client>(new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" });

            this._clientRepository.Setup(m => m.GetByEmailAndUser(It.IsAny<Client>())).Returns(taskClient);
            this._clientRepository.Setup(m => m.UpdateByEmailAndUser(It.IsAny<Client>()));

            var controller = new UpdateCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new UpdateCommand(client), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.GetByEmailAndUser(It.IsAny<Client>()), Times.Once);
            _clientRepository.Verify(m => m.UpdateByEmailAndUser(It.IsAny<Client>()), Times.Never);
            result.Status.Should().Be(StatusCodes.Status302Found);
        }

        [Fact]
        public async Task HandleShouldBeException()
        {
            var client = new ClientModel { User = "Diego", Email = "Diegowrl@hotmail.com" };

            var taskClient = Task.FromResult<Client>(new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" });

            this._clientRepository.Setup(m => m.GetByEmailAndUser(It.IsAny<Client>()));
            this._clientRepository.Setup(m => m.UpdateByEmailAndUser(It.IsAny<Client>())).Throws(new Exception());

            var controller = new UpdateCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new UpdateCommand(client), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.GetByEmailAndUser(It.IsAny<Client>()), Times.Once);
            _clientRepository.Verify(m => m.UpdateByEmailAndUser(It.IsAny<Client>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
