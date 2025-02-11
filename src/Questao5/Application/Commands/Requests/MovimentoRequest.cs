using Questao5.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class MovimentoRequest : IValidatableObject
{
    public string ContaCorrenteId { get; set; }
    public decimal Valor { get; set; }
    public string Tipo { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        IContaCorrenteService contaCorrenteService;

        try
        {
            contaCorrenteService = validationContext.Items["IContaCorrenteService"] as IContaCorrenteService;
        }
        catch
        {
            contaCorrenteService = (IContaCorrenteService)validationContext.GetService(typeof(IContaCorrenteService));
        }
        

        if (contaCorrenteService == null)
        {
            yield return new ValidationResult("Serviço de conta corrente não disponível.", new[] { nameof(ContaCorrenteId) });
            yield break;
        }

        var conta = contaCorrenteService.ObterPorId(ContaCorrenteId);

        if (conta == null)
            yield return new ValidationResult("Conta corrente não cadastrada.", new[] { nameof(ContaCorrenteId) });
        else if (!conta.ativo)
            yield return new ValidationResult("Conta corrente inativa.", new[] { nameof(ContaCorrenteId) });

        if (Valor <= 0)
            yield return new ValidationResult("Valor inválido.", new[] { nameof(Valor) });

        if (string.IsNullOrEmpty(Tipo) || (Tipo.ToUpper() != "C" && Tipo.ToUpper() != "D"))
            yield return new ValidationResult("Tipo de movimento inválido.", new[] { nameof(Tipo) });
    }
}

