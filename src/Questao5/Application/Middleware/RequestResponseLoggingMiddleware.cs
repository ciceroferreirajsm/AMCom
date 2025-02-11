using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO.Pipelines;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using Azure.Core;
using System;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IIdempotenciaService _idempotenciaService;
    private readonly IContaCorrenteService _contaCorrenteService;

    public RequestResponseLoggingMiddleware(RequestDelegate next,
                                             IIdempotenciaService idempotenciaService,
                                             IContaCorrenteService contaCorrenteService)
    {
        _next = next;
        _idempotenciaService = idempotenciaService;
        _contaCorrenteService = contaCorrenteService;
    }

    public async Task InvokeAsync(HttpContext contextoHttp)
    {
        var opcoes = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        if (contextoHttp.Request.Path.Equals("/api/Movimento"))
        {
            contextoHttp.Request.EnableBuffering();
            var requestBody = await ReadRequestBodyAsync(contextoHttp.Request);
            Stream originalBody = contextoHttp.Response.Body;

            contextoHttp.Request.EnableBuffering();
            var movimentoRequest = JsonConvert.DeserializeObject<MovimentoRequest>(requestBody);

            var validationContext = new ValidationContext(movimentoRequest)
            {
                Items = { { "IContaCorrenteService", _contaCorrenteService } }
            };

            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(movimentoRequest, validationContext, validationResults, true);
            var chave = string.Empty;

            if (isValid)
            {
                if (!string.IsNullOrWhiteSpace(requestBody))
                    chave = _idempotenciaService.AdicionarRequest(requestBody);
            }

            try
            {
                using var memStream = new MemoryStream();
                contextoHttp.Response.Body = memStream;

                await _next(contextoHttp);

                memStream.Position = 0;
                string responseBody = new StreamReader(memStream).ReadToEnd();

                if (isValid)
                    _idempotenciaService.AdicionarResponse(responseBody, chave);

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
            }
            finally
            {
                contextoHttp.Response.Body = originalBody;
            }
        }
        else
        {
            await _next(contextoHttp);
        }
    }

    private async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();
        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }
}
