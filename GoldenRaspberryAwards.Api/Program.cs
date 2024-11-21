using GoldenRaspberryAwards.Api.Contracts;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configura��o do banco de dados em mem�ria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GoldenRaspberryAwardsDb"));

// 2. Registro do servi�o de carregamento de dados
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

// C�digo para inicializar o DataLoaderService ao iniciar a aplica��o
using (var scope = app.Services.CreateScope())
{
    var dataLoader = scope.ServiceProvider.GetRequiredService<IDataLoaderService>();
    await dataLoader.LoadDataAsync();
}

app.MapControllers();

app.Run();
