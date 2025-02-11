using MovimentacaoAPI.Controllers;
using Questao5.Domain.Entities;

namespace Questao5.Application.Services.Interfaces
{
    public interface IMovimentoService
    {
        int Adicionar(Movimento movimento);
        decimal ObterPorConta(string contaCorrenteId);
    }

}
