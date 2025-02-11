using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using MovimentacaoAPI.Controllers;
using Questao5.Application.Services;

namespace Questao5Teste.Infrastructure.Services.Controllers
{
    public class MovimentoControllerTests
    {
        private readonly Mock<IContaCorrenteService> _mockContaCorrenteService;
        private readonly Mock<IMovimentoService> _mockMovimentoService;
        private readonly MovimentoController _controller;

        public MovimentoControllerTests()
        {
            _mockContaCorrenteService = new Mock<IContaCorrenteService>();
            _mockMovimentoService = new Mock<IMovimentoService>();
            _controller = new MovimentoController(_mockContaCorrenteService.Object, _mockMovimentoService.Object);
        }

        [Fact]
        public void RegistrarMovimento_ContaCorrenteInexistente_RetornaBadRequest()
        {
            // Arrange
            var movimentoRequest = new MovimentoRequest
            {
                ContaCorrenteId = "123",
                Valor = 100,
                Tipo = "X"
            };

            _mockContaCorrenteService
                .Setup(service => service.ObterPorId(It.IsAny<string>()))
                .Returns((ContaCorrente?)null);

            // Act
            var result = _controller.Registrar(movimentoRequest);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var response = okResult.Value as MovimentoResponse;
            Assert.NotNull(response);
        }
    }
}
