using Infrastructure.ProjetoCursosIdioma.Data;
using Application.ProjetoCursosIdioma.Mappings;
using Microsoft.EntityFrameworkCore;
using Infrastructure.ProjetoCursosIdioma.Repositories;
using Domain.ProjetoCursosIdioma.Entities;
using Domain.ProjetoCursosIdioma.Repositories;
using Application.ProjetoCursosIdioma.Interfaces;
using Application.ProjetoCursosIdioma.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adições
//Connection String
builder.Services.AddDbContext<PCI_DbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PCI_ConnectionString")));

builder.Services.AddScoped<Domain.ProjetoCursosIdioma.Repositories.IAlunoRepository, Infrastructure.ProjetoCursosIdioma.Repositories.SQLAlunoRepository>();
builder.Services.AddScoped<Domain.ProjetoCursosIdioma.Repositories.ITurmaRepository, Infrastructure.ProjetoCursosIdioma.Repositories.SQLTurmaRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
