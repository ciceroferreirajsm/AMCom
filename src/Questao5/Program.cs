using FluentAssertions.Common;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Questao5.Application.Repository;
using Questao5.Application.Services;
using Questao5.Application.Services.Interfaces;
using Questao5.Infrastructure.Sqlite;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();
builder.Services.AddSingleton<IContaCorrenteRepository, ContaCorrenteRepository>();
builder.Services.AddSingleton<IMovimentoRepository, MovimentoRepository>();
builder.Services.AddSingleton<IIdempotenciaRepository, IdempotenciaRepository>();
builder.Services.AddSingleton<IContaCorrenteService, ContaCorrenteService>();
builder.Services.AddSingleton<IMovimentoService, MovimentoService>();
builder.Services.AddSingleton<IIdempotenciaService, IdempotenciaService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Movimentação API",
        Version = "v1",
        Description = "API para gerenciamento de movimentações bancárias",
    });

    var xmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MovimentacaoAPI.xml");

    Console.WriteLine("Diretório base: " + AppDomain.CurrentDomain.BaseDirectory);
    Console.WriteLine("Caminho do XML: " + xmlFile);

    if (File.Exists(xmlFile))
    {
        c.IncludeXmlComments(xmlFile);
    }
    else
    {
        Console.WriteLine("Arquivo XML não encontrado: " + xmlFile);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// sqlite
#pragma warning disable CS8602 // Dereference of a possibly null reference.
app.Services.GetService<IDatabaseBootstrap>().Setup();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.Run();
