using Azure.Core;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using System.Collections.Generic;

namespace Questao5.Application.Services
{
    /// <summary>
    /// Serviço responsável pela lógica de idempotência, garantindo que requisições e respostas sejam manipuladas de forma idêntica para evitar duplicidade de processamento.
    /// </summary>
    /// <inheritdoc/>
    public class IdempotenciaService : IIdempotenciaService
    {
        private readonly IIdempotenciaRepository _IdempotenciaRepository;

        /// <summary>
        /// Construtor que inicializa o serviço de idempotência com o repositório necessário.
        /// </summary>
        /// <param name="IdempotenciaRepository">Repositório responsável por armazenar e recuperar dados de idempotência.</param>
        /// <inheritdoc/>
        public IdempotenciaService(IIdempotenciaRepository IdempotenciaRepository)
        {
            _IdempotenciaRepository = IdempotenciaRepository;
        }

        /// <summary>
        /// Adiciona uma requisição para o controle de idempotência.
        /// </summary>
        /// <param name="request">A requisição a ser adicionada.</param>
        /// <returns>Retorna uma chave ou identificador para a requisição.</returns>
        /// <inheritdoc/>
        public string AdicionarRequest(string request) =>
            _IdempotenciaRepository.AdicionarRequest(request);

        /// <summary>
        /// Adiciona uma resposta associada a uma chave para controle de idempotência.
        /// </summary>
        /// <param name="response">A resposta a ser armazenada.</param>
        /// <param name="chave">A chave associada à requisição original.</param>
        /// <inheritdoc/>
        public void AdicionarResponse(string response, string chave) =>
            _IdempotenciaRepository.AdicionarResponse(response, chave);

        /// <summary>
        /// Obtém todos os registros de idempotência armazenados.
        /// Não foi solicitado, mas poderiamos tratar as requisições que falharam com uma fila, ao inves de salvar na base
        /// enviar para uma fila com fila morta, desse jeito nunca perdendo os dados.
        /// </summary>
        /// <returns>Retorna uma coleção de registros de idempotência.</returns>
        /// <inheritdoc/>
        public IEnumerable<Idempotencia> ObterTodos() =>
            _IdempotenciaRepository.ObterTodos();
    }
}
