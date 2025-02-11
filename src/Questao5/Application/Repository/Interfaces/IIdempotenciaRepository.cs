using Questao5.Domain.Entities;
using System.Collections.Generic;

namespace Questao5.Application.Services.Interfaces
{
    /// <summary>
    /// Interface responsável pelas operações relacionadas à idempotência.
    /// Define métodos para adicionar e recuperar informações relacionadas a requisições e respostas idempotentes.
    /// </summary>
    public interface IIdempotenciaRepository
    {
        /// <summary>
        /// Adiciona uma nova requisição ao controle de idempotência.
        /// </summary>
        /// <param name="request">A string representando a requisição a ser registrada.</param>
        /// <returns>Retorna uma chave ou identificador associado à requisição registrada.</returns>
        /// <inheritdoc/>
        string AdicionarRequest(string request);

        /// <summary>
        /// Adiciona uma nova resposta ao controle de idempotência, associada a uma chave específica.
        /// </summary>
        /// <param name="response">A string representando a resposta a ser registrada.</param>
        /// <param name="chave">A chave associada à resposta que será armazenada.</param>
        /// <inheritdoc/>
        void AdicionarResponse(string response, string chave);

        /// <summary>
        /// Obtém todos os registros de idempotência armazenados.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="Idempotencia"/> representando todos os registros de idempotência.</returns>
        /// <inheritdoc/>
        IEnumerable<Idempotencia> ObterTodos();
    }
}
