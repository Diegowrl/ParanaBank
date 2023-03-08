using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using ParanaBank.Application.Commands.Delete;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Tests.Aplication
{
    public class DeleteCommandHandleTest
    {
        private readonly Mock<IClientRepository> _clientRepository;
        private readonly Mock<ILogger<DeleteCommandHandle>> _ilogger;
        public DeleteCommandHandleTest()
        {
            this._clientRepository = new Mock<IClientRepository>();
            this._ilogger = new Mock<ILogger<DeleteCommandHandle>>();
        }

        [Fact]
        public async Task HandleShouldBeNotFound()
        {

            this._clientRepository.Setup(m => m.DeleteByEmail("Diegowrl@hotmail.com"));
            this._clientRepository.Setup(m => m.GetByEmail("Diegowrl@hotmail.com"));

            var controller = new DeleteCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new DeleteCommand("Diegowrl@hotmail.com"), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.DeleteByEmail(It.IsAny<string>()), Times.Never);
            _clientRepository.Verify(m => m.GetByEmail(It.IsAny<string>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task HandleShouldBeOk()
        {
            var client = new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" };
            var taskClient = Task.FromResult<Client>(client);

            this._clientRepository.Setup(m => m.DeleteByEmail(It.IsAny<string>()));
            this._clientRepository.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(taskClient);

            var controller = new DeleteCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new DeleteCommand("Diegowrl@hotmail.com"), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.DeleteByEmail(It.IsAny<string>()), Times.Once);
            _clientRepository.Verify(m => m.GetByEmail(It.IsAny<string>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task HandleShouldBeException()
        {
            var client = new Client { UserName = "Diego", Email = "Diegowrl@hotmail.com" };
            var taskClient = Task.FromResult<Client>(client);

            this._clientRepository.Setup(m => m.DeleteByEmail(It.IsAny<string>())).Throws(new Exception());
            this._clientRepository.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(taskClient);

            var controller = new DeleteCommandHandle(this._clientRepository.Object, _ilogger.Object);

            var result = await controller.Handle(new DeleteCommand("Diegowrl@hotmail.com"), It.IsAny<CancellationToken>());

            _clientRepository.Verify(m => m.DeleteByEmail(It.IsAny<string>()), Times.Once);
            _clientRepository.Verify(m => m.GetByEmail(It.IsAny<string>()), Times.Once);
            result.Status.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
