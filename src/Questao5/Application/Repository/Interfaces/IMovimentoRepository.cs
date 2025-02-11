using MovimentacaoAPI.Controllers;
using Questao5.Domain.Entities;
using System.Collections.Generic;

namespace Questao5.Application.Services.Interfaces
{
    /// <summary>
    /// Interface responsável pelas operações relacionadas às movimentações bancárias.
    /// Define métodos para adicionar um movimento e recuperar todos os movimentos de uma conta corrente.
    /// </summary>
    public interface IMovimentoRepository
    {
        /// <summary>
        /// Adiciona um novo movimento bancário à base de dados.
        /// </summary>
        /// <param name="movimento">O objeto de movimento bancário a ser adicionado.</param>
        /// <returns>Retorna o ID do movimento recém-adicionado.</returns>
        /// <inheritdoc/>
        int Adicionar(Movimento movimento);

        /// <summary>
        /// Recupera todos os movimentos bancários associados a uma conta corrente específica.
        /// </summary>
        /// <param name="contaCorrenteId">O identificador da conta corrente cujos movimentos serão recuperados.</param>
        /// <returns>Uma lista de movimentos bancários associados à conta corrente.</returns>
        /// <inheritdoc/>
        IEnumerable<Movimento> ObterPorConta(string contaCorrenteId);
    }
}
