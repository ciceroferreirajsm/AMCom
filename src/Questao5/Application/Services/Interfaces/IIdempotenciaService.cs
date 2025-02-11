using MovimentacaoAPI.Controllers;
using Questao5.Domain.Entities;
using System.Collections.Generic;

namespace Questao5.Application.Services.Interfaces
{    /// <inheritdoc/>
    public interface IIdempotenciaService

    {    /// <inheritdoc/>
        string AdicionarRequest(string request);

        /// <inheritdoc/>
        void AdicionarResponse(string response, string chave);
        /// <inheritdoc/>
        IEnumerable<Idempotencia> ObterTodos();
    }

}
