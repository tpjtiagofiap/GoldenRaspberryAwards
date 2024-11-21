using GoldenRaspberryAwards.Api.Contracts;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configuração do banco de dados em memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GoldenRaspberryAwardsDb"));

// 2. Registro do serviço de carregamento de dados
builder.Services.AddScoped<IDataLoaderService, DataLoaderService>();
builder.Services.AddScoped<IProducerIntervalService, ProducerIntervalService>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Código para inicializar o DataLoaderService ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var dataLoader = scope.ServiceProvider.GetRequiredService<IDataLoaderService>();
    await dataLoader.LoadDataAsync();
}

app.MapControllers();

app.Run();
