using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using System;

namespace MovimentacaoAPI.Controllers
{
    /// <summary>
    /// Controller Movimento
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoController : ControllerBase
    {
        private readonly IMovimentoService _movimentoService;

        /// <summary>
        /// Construtor Controller Movimento
        /// </summary>
        public MovimentoController(IMovimentoService movimentoRepository)
        {
            _movimentoService = movimentoRepository;
        }

        /// <summary>
        /// Registra uma nova movimentação bancária.
        /// </summary>
        /// <param name="request">Objeto contendo as informações da movimentação</param>
        /// <returns>Retorna o ID da movimentação registrada</returns>
        /// <response code="200">Movimentação registrada com sucesso</response>
        /// <response code="400">Caso os dados enviados estejam inválidos</response>
        [HttpPost]
        public IActionResult Registrar([FromBody] MovimentoRequest request)
        {
            // Se o modelo for inválido, retorna BadRequest com o modelo de erro
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var movimento = new Movimento
            {
                idcontacorrente = request.ContaCorrenteId,
                valor = request.Valor,
                tipomovimento = request.Tipo,
                datamovimento = DateTime.Now
            };

            var idMovimento = _movimentoService.Adicionar(movimento);

            return Ok(new MovimentoResponse { IdMovimento = idMovimento });
        }
    }
}
