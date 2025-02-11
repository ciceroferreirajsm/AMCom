using Questao5.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

public class ContaCorrenteResponse
{
    public string NumeroConta { get; set; }
    public string NomeTitular { get; set; }
    public DateTime DataHoraResposta { get; set; }
    public decimal SaldoAtual { get; set; }
}

