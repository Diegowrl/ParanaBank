using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParanaBank.Application.Commands.Create;
using ParanaBank.Application.Commands.Delete;
using ParanaBank.Application.Commands.Update;
using ParanaBank.Application.Models;
using ParanaBank.Controllers;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;

namespace ParanaBank.Tests.Controller
{
    public class ClientControllerTest
    {
        private readonly Mock<IClientRepository> _clientQuery;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ClientController>> _ilogger;
        public ClientControllerTest()
        {
            this._clientQuery = new Mock<IClientRepository>();
            this._mediatorMock = new Mock<IMediator>();
            this._ilogger = new Mock<ILogger<ClientController>>();
        }

        [Fact]
        public async Task GetAllClientesShouldBeSucess()
        {

            var list = new List<Client>();
            list.Add(It.IsAny<Client>());
            list.Add(It.IsAny<Client>());

            var task = Task.FromResult<IEnumerable<Client>>(list);

            this._clientQuery.Setup(m => m.GetAll()).Returns(task);

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Get();

            _clientQuery.Verify(m => m.GetAll(), Times.Once);
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task GetByEmailShouldBeSucess()
        {

            var task = Task.FromResult<Client>(It.IsAny<Client>());

            this._clientQuery.Setup(m => m.GetByEmail(It.IsAny<string>())).Returns(task);

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.GetByEmail(It.IsAny<string>());

            _clientQuery.Verify(m => m.GetByEmail(It.IsAny<string>()), Times.Once);
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }


        [Fact]
        public async Task PostShouldBeSucess()
        {
            _mediatorMock
                 .Setup(m => m.Send(It.IsAny<CreateCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new ResultCommand { Message = "User created", Status = StatusCodes.Status200OK });

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Post(new ClientModel { Email = "Diegowrl@hotmail.com", User = "Diego" });

            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task PostShouldNotBeSucess()
        {
            _mediatorMock
                 .Setup(m => m.Send(It.IsAny<CreateCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new ResultCommand { Message = "Error to add user on database", Status = StatusCodes.Status500InternalServerError });

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Post(new ClientModel { Email = "Diegowrl@hotmail.com", User = "Diego" });

            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task UpdateShouldBeSucess()
        {
            _mediatorMock
                 .Setup(m => m.Send(It.IsAny<UpdateCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new ResultCommand { Message = "User Updated", Status = StatusCodes.Status200OK });

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Update(new ClientModel { Email = "Diegowrl@hotmail.com", User = "Diego" });

            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }


        [Fact]
        public async Task UpdateShouldNotBeSucess()
        {
            _mediatorMock
                 .Setup(m => m.Send(It.IsAny<UpdateCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new ResultCommand { Message = "Error to Update user on database", Status = StatusCodes.Status500InternalServerError });

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Update(new ClientModel { Email = "Diegowrl@hotmail.com", User = "Diego" });

            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task DeleteShouldBeSucess()
        {
            _mediatorMock
                 .Setup(m => m.Send(It.IsAny<DeleteCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new ResultCommand { Message = "Error to Update user on database", Status = StatusCodes.Status200OK });

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Delete("Diegowrl@hotmail.com");

            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task DeleteShouldNotBeSucess()
        {
            _mediatorMock
                 .Setup(m => m.Send(It.IsAny<DeleteCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(new ResultCommand { Message = "Error to Update user on database", Status = StatusCodes.Status500InternalServerError });

            var controller = new ClientController(_ilogger.Object, this._clientQuery.Object, _mediatorMock.Object);

            var result = await controller.Delete("Diegowrl@hotmail.com");

            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
