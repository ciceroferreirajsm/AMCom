using MovimentacaoAPI.Controllers;
using Questao5.Domain.Entities;

namespace Questao5.Application.Services.Interfaces
{
    /// <summary>
    /// Interface responsável pela manipulação de dados relacionados a contas correntes.
    /// Define métodos para acessar e recuperar informações sobre uma conta corrente a partir de um identificador.
    /// </summary>
    public interface IContaCorrenteRepository
    {
        /// <summary>
        /// Obtém uma conta corrente pelo seu identificador único.
        /// </summary>
        /// <param name="id">O identificador único da conta corrente a ser recuperada.</param>
        /// <returns>Retorna a conta corrente correspondente ao identificador fornecido, ou <c>null</c> se não encontrada.</returns>
        /// <inheritdoc/>
        ContaCorrente? ObterPorId(string id);
    }
}
