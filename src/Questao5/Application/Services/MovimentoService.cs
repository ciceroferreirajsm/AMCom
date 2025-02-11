using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using System.Linq;

namespace Questao5.Application.Services
{
    /// <summary>
    /// Serviço responsável pela lógica de negócios relacionada aos movimentos bancários.
    /// Inclui operações de adição de movimento e cálculo do saldo de uma conta corrente com base nos movimentos realizados.
    /// </summary>
    public class MovimentoService : IMovimentoService
    {
        private readonly IMovimentoRepository _MovimentoRepository;

        /// <summary>
        /// Inicializa o serviço de movimento com o repositório necessário para acessar os dados dos movimentos bancários.
        /// </summary>
        /// <param name="MovimentoRepository">Repositório que fornece acesso aos dados de movimentos bancários.</param>
        public MovimentoService(IMovimentoRepository MovimentoRepository)
        {
            _MovimentoRepository = MovimentoRepository;
        }

        /// <summary>
        /// Adiciona um novo movimento bancário à base de dados.
        /// </summary>
        /// <param name="movimento">O objeto de movimento bancário a ser adicionado.</param>
        /// <returns>Retorna o ID do movimento recém-adicionado.</returns>
        public int Adicionar(Movimento movimento)
        {
            return _MovimentoRepository.Adicionar(movimento);
        }

        /// <summary>
        /// Calcula o saldo de uma conta corrente com base nos movimentos realizados (créditos e débitos).
        /// </summary>
        /// <param name="contaCorrenteId">O identificador da conta corrente para calcular o saldo.</param>
        /// <returns>O saldo atual da conta corrente considerando os movimentos de crédito e débito.</returns>
        public decimal ObterPorConta(string contaCorrenteId)
        {
            decimal saldo = 0.00M;
            var movimentos = _MovimentoRepository.ObterPorConta(contaCorrenteId);

            if (movimentos != null && movimentos.Any())
                foreach (var movimento in movimentos)
                {
                    if (movimento.tipomovimento.ToUpper() == "C") // Crédito
                    {
                        saldo += movimento.valor;
                    }
                    else if (movimento.tipomovimento.ToUpper() == "D") // Débito
                    {
                        saldo -= movimento.valor;
                    }
                }
            return saldo;
        }
    }
}
