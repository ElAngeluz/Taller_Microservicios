using microscore.api.Controllers.v1;
using microscore.application.interfaces.services;
using microscore.application.models.dtos;
using microscore.application.models.dtos.accounts;
using Microsoft.AspNetCore.Mvc;

namespace microscore.test
{
    public class AccountsControllerTests
    {
        //TODO: optimizacion de las llamadas a las interfaces
        [Fact]
        public async Task GetAccountByNumbers_Returns_OkResult()
        {
            // Arrange
            var mockAccountServices = new Mock<IAccountServices>();
            mockAccountServices.Setup(s => s.GetAccountByNumber(It.IsAny<string>()))
                .ReturnsAsync(new AccountDTO()
                {
                    Number = "478758",
                    TypeAccount = "Ahorro",
                    Balance = 2000,
                    State = true,
                    ClientName = "Jose Lema "
                });
            var controller = new AccountsController(mockAccountServices.Object);

            // Act
            var result = await controller.GetAccountbyNumbers("478758");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<MsDtoResponse<AccountDTO>>(okResult.Value);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async Task GetAccountByNumbers_Returns_NotFound()
        {
            // Arrange
            var mockAccountServices = new Mock<IAccountServices>();
            mockAccountServices.Setup(s => s.GetAccountByNumber(It.IsAny<string>()))
                .ReturnsAsync((AccountDTO)null); // Simulación de respuesta nula
            var controller = new AccountsController(mockAccountServices.Object);

            // Act
            var result = await controller.GetAccountbyNumbers("789012");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}