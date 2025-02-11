using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Questao5.Application.Repository
{
    /// <summary>
    /// Repositório responsável pelas operações de banco de dados relacionadas a movimentos bancários.
    /// Utiliza Dapper para mapear os dados entre o banco de dados e os objetos do domínio.
    /// </summary>
    /// <inheritdoc/>
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Inicializa o repositório com a string de conexão obtida da configuração.
        /// </summary>
        /// <param name="configuration">A configuração usada para recuperar a string de conexão com o banco de dados.</param>
        /// <exception cref="Exception">Lança uma exceção se não for possível obter a string de conexão.</exception>
        /// <inheritdoc/>
        public MovimentoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DatabaseName").Value ?? throw new Exception("Erro ao obter string de conexão.");
        }

        /// <summary>
        /// Adiciona um novo movimento bancário no banco de dados.
        /// </summary>
        /// <param name="movimento">Objeto que representa o movimento bancário a ser inserido.</param>
        /// <returns>Retorna o ID do movimento recém-adicionado.</returns>
        /// <inheritdoc/>
        public int Adicionar(Movimento movimento)
        {
            using var dbConnection = new SqliteConnection(_connectionString);

            var query = @"
        INSERT INTO movimento (idcontacorrente, valor, tipomovimento, datamovimento)
        VALUES (@ContaCorrenteId, @Valor, @Tipo, @DataMovimento);
        SELECT last_insert_rowid();";

            var id = dbConnection.ExecuteScalar<int>(query, new
            {
                ContaCorrenteId = movimento.idcontacorrente,
                Valor = movimento.valor,
                Tipo = movimento.tipomovimento,
                DataMovimento = movimento.datamovimento
            });

            return id;
        }

        /// <summary>
        /// Obtém todos os movimentos bancários relacionados a uma conta corrente específica.
        /// </summary>
        /// <param name="contaCorrenteId">O identificador da conta corrente para filtrar os movimentos.</param>
        /// <returns>Uma coleção de objetos <see cref="Movimento"/> correspondentes à conta corrente.</returns>
        /// <inheritdoc/>
        public IEnumerable<Movimento> ObterPorConta(string contaCorrenteId)
        {
            using var dbConnection = new SqliteConnection(_connectionString);
            var query = "SELECT * FROM movimento WHERE idcontacorrente = @ContaCorrenteId";

            return dbConnection.Query<Movimento>(query, new { ContaCorrenteId = contaCorrenteId }).ToList();
        }
    }
}
