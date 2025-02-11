using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Questao5.Application.Repository
{
    /// <summary>
    /// Implementação do repositório para operações relacionadas à idempotência.
    /// Esta classe lida com a adição de requisições e respostas ao banco de dados,
    /// e com a recuperação de todos os registros de idempotência.
    /// </summary>
    public class IdempotenciaRepository : IIdempotenciaRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Inicializa o repositório com a string de conexão do banco de dados.
        /// </summary>
        /// <param name="configuration">Configuração que contém a string de conexão do banco de dados.</param>
        /// <exception cref="Exception">Lança uma exceção se não for possível obter a string de conexão.</exception>
        /// <inheritdoc/>
        public IdempotenciaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DatabaseName").Value ?? throw new Exception("Erro ao obter string de conexão.");
        }

        /// <summary>
        /// Adiciona uma nova requisição ao controle de idempotência.
        /// Gera uma chave única para a requisição e a armazena no banco de dados.
        /// </summary>
        /// <param name="request">A requisição a ser registrada no banco de dados.</param>
        /// <returns>Retorna a chave gerada para a requisição registrada.</returns>
        /// <inheritdoc/>
        public string AdicionarRequest(string request)
        {
            using var dbConnection = new SqliteConnection(_connectionString);
            var chave = Guid.NewGuid().ToString();
            var query = @"
                    INSERT INTO idempotencia (requisicao, chave_idempotencia)
                    VALUES (@Request, @Chave);
                    SELECT last_insert_rowid();";

            var id = dbConnection.ExecuteScalar<string>(query, new
            {
                @Request = request,
                @Chave = chave
            });

            return chave;
        }

        /// <summary>
        /// Adiciona uma resposta associada a uma chave de idempotência.
        /// Atualiza o registro da chave com o resultado da resposta.
        /// </summary>
        /// <param name="response">A resposta a ser registrada no banco de dados.</param>
        /// <param name="chave">A chave associada à requisição, que será atualizada com a resposta.</param>
        /// <inheritdoc/>
        public void AdicionarResponse(string response, string chave)
        {
            using var dbConnection = new SqliteConnection(_connectionString);

            var query = @"UPDATE idempotencia
                          SET resultado = @Response
                          WHERE chave_idempotencia = @Chave;";

            dbConnection.Execute(query, new
            {
                Response = response,
                @Chave = chave
            });
        }

        /// <summary>
        /// Recupera todos os registros de idempotência armazenados no banco de dados.
        /// </summary>
        /// <returns>Uma coleção de registros de idempotência.</returns>
        /// <inheritdoc/>
        public IEnumerable<Idempotencia> ObterTodos()
        {
            using var dbConnection = new SqliteConnection(_connectionString);

            var query = @"SELECT * FROM idempotencia";

            return dbConnection.Query<Idempotencia>(query);
        }
    }
}
