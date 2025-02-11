using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Services.Interfaces;
using System;

namespace MovimentacaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaldoController : ControllerBase
    {
        private readonly IContaCorrenteService _contaCorrenteService;
        private readonly IMovimentoService _movimentoService;

        public SaldoController(IContaCorrenteService contaCorrenteService, IMovimentoService movimentoRepository)
        {
            _contaCorrenteService = contaCorrenteService;
            _movimentoService = movimentoRepository;
        }

        /// <summary>
        /// Consulta o saldo de uma conta corrente.
        /// </summary>
        /// <param name="contaCorrenteId">ID da conta corrente para consulta</param>
        /// <returns>Retorna o saldo atual da conta, ou um erro se a conta não for encontrada ou estiver inativa</returns>
        /// <response code="200">Retorna o saldo da conta corrente com detalhes do titular</response>
        /// <response code="400">Conta corrente inválida ou inativa</response>
        [HttpGet("{contaCorrenteId}")]
        public IActionResult ConsultarSaldo(string contaCorrenteId)
        {
            var conta = _contaCorrenteService.ObterPorId(contaCorrenteId);
            if (conta == null)
                return BadRequest(new ContaCorrenteBadRequestResponse { mensagem = "Conta corrente não cadastrada.", tipo = "INVALID_ACCOUNT" });

            if (!conta.ativo)
                return BadRequest(new ContaCorrenteBadRequestResponse { mensagem = "Conta corrente inativa.", tipo = "INACTIVE_ACCOUNT" });

            var saldo = _movimentoService.ObterPorConta(contaCorrenteId);

            return Ok(new ContaCorrenteResponse
            {
                NumeroConta = conta.numero,
                NomeTitular = conta.nome,
                DataHoraResposta = DateTime.Now,
                SaldoAtual = saldo
            });
        }
    }
}
