using Infrastructure.ProjetoCursosIdioma.Data;
using Infrastructure.ProjetoCursosIdioma.Repositories;
using Application.ProjetoCursosIdioma.Mappings;
using Application.ProjetoCursosIdioma.Interfaces;
using Application.ProjetoCursosIdioma.Services;
using Domain.ProjetoCursosIdioma.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adições
//Connection String
builder.Services.AddDbContext<PCI_DbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("PCI_ConnectionString")));

builder.Services.AddScoped<Domain.ProjetoCursosIdioma.Repositories.IAlunoRepository, Infrastructure.ProjetoCursosIdioma.Repositories.SQLAlunoRepository>();
builder.Services.AddScoped<Domain.ProjetoCursosIdioma.Repositories.ITurmaRepository, Infrastructure.ProjetoCursosIdioma.Repositories.SQLTurmaRepository>();
builder.Services.AddScoped<Domain.ProjetoCursosIdioma.Repositories.INivelTurmaRepository, Infrastructure.ProjetoCursosIdioma.Repositories.SQLNivelTurmaRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<INivelTurmaService, NivelTurmaService>();

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
