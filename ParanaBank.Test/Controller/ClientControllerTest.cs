using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ParanaBank.Controllers;
using ParanaBank.Domain.Entity;
using ParanaBank.Domain.Interfaces;
using Xunit;
using FluentAssertions;

namespace ParanaBank.Test.Controller
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
        public async Task GetAllClientes()
        {

            var list = new List<Client>();
            list.Add(It.IsAny<Client>());
            list.Add(It.IsAny<Client>());
            
            var task = Task.FromResult<IEnumerable<Client>>(list);

            this._clientQuery.Setup(m => m.GetAll()).Returns(task);

            var controller = new ClientController(_ilogger.Object,this._clientQuery.Object, _mediatorMock.Object);
            
            var result = await controller.Get();

            _clientQuery.Verify(m => m.GetAll(), Times.Once);
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status200OK);
        }
    }
}
