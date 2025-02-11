using MovimentacaoAPI.Controllers;
using Questao5.Domain.Entities;

namespace Questao5.Application.Services.Interfaces
{
    public interface IContaCorrenteService
    {
        ContaCorrente? ObterPorId(string contaCorrenteId);
    }

}
