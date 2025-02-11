using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;

namespace Questao5.Application.Services
{
    /// <summary>
    /// Serviço responsável pela lógica de negócios relacionada a contas correntes.
    /// Inclui operações para obter detalhes de uma conta corrente específica.
    /// </summary>
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        /// <summary>
        /// Inicializa o serviço de conta corrente com o repositório necessário para acessar os dados das contas correntes.
        /// </summary>
        /// <param name="contaCorrenteRepository">Repositório que fornece acesso aos dados de contas correntes.</param>
        public ContaCorrenteService(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        /// <summary>
        /// Obtém uma conta corrente com base no seu identificador.
        /// </summary>
        /// <param name="contaCorrenteId">O identificador da conta corrente a ser recuperada.</param>
        /// <returns>Retorna a conta corrente correspondente ao identificador fornecido, ou <c>null</c> se não encontrar.</returns>
        public ContaCorrente? ObterPorId(string contaCorrenteId)
        {
            return _contaCorrenteRepository.ObterPorId(contaCorrenteId);
        }
    }
}
