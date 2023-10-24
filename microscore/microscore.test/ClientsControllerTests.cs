using microscore.api.Controllers.v1;
using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.application.models.dtos.people;
using Microsoft.AspNetCore.Mvc;

namespace microscore.test
{
    public class ClientsControllerTests
    {
        [Fact]
        public async Task GetAllClient_Returns_OkResult()
        {
            // Arrange
            var mockClientsServices = new Mock<IClientsServices>();
            var expectedClients = new List<ClientDTO>
        {
            new ClientDTO
            {
                ClientId = Guid.Parse("2E707444-89BA-4174-AB63-7DFA75C58D07"),
                Name = "Jose Lema ",
                Identification = "12345",
                Address = "Otavalo sn y principal ",
                Phone = "098254785",
                State = true
            },
            new ClientDTO
            {
                ClientId = Guid.Parse("0C154AD4-D8BE-459B-A7CF-03717EA559D7"),
                Name = "Marianela Montalvo",
                Identification = "54321",
                Address = "Amazonas y  NNUU",
                Phone = "097548965",
                State = true
            }
        };
            mockClientsServices.Setup(s => s.GetAll(true)).ReturnsAsync(expectedClients);
            var controller = new ClientsController(mockClientsServices.Object);

            // Act
            var result = await controller.GetAllClient(true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<MsDtoResponse<List<ClientDTO>>>(okResult.Value);
            var actualClients = response.Data;

            Assert.Equal(expectedClients, actualClients);
        }

        [Fact]
        public async Task GetClient_Returns_OkResult()
        {
            // Arrange
            var mockClientsServices = new Mock<IClientsServices>();
            var clientId = Guid.Parse("2E707444-89BA-4174-AB63-7DFA75C58D07");
            var expectedClient = new ClientDTO
            {
                ClientId = clientId,
                Name = "Jose Lema ",
                Identification = "12345",
                Address = "Otavalo sn y principal ",
                Phone = "098254785",
                State = true
            };
            mockClientsServices.Setup(s => s.GetClient(clientId)).ReturnsAsync(expectedClient);
            var controller = new ClientsController(mockClientsServices.Object);

            // Act
            var result = await controller.GetClient(clientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<MsDtoResponse<ClientDTO>>(okResult.Value);
            var actualClient = response.Data;

            Assert.Equal(expectedClient, actualClient);
        }
    }
}
