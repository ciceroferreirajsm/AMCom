using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Questao5.Application.Services.Interfaces;
using Questao5.Domain.Entities;
using System;
using System.Data;
using System.Linq;

namespace Questao5.Application.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly string _connectionString;

        public ContaCorrenteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("DatabaseName").Value ?? throw new Exception("Erro ao obter string de conexão.");
        }

        public ContaCorrente? ObterPorId(string id)
        {
            using var dbConnection = new SqliteConnection(_connectionString);

            var query = "SELECT * FROM contacorrente WHERE idcontacorrente = @Id";

            var contaCorrente = dbConnection.Query<ContaCorrente>(query, new { Id = id }).FirstOrDefault();

            return contaCorrente;
        }
    }
}
