using API.ProjetoCursosIdioma.Data;
using API.ProjetoCursosIdioma.Mappings;
using Microsoft.EntityFrameworkCore;
using API.ProjetoCursosIdioma.Repositories;
using API.ProjetoCursosIdioma.Models.Domain;

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

builder.Services.AddScoped<API.ProjetoCursosIdioma.Repositories.AlunoRepFolder.IAlunoRepository, API.ProjetoCursosIdioma.Repositories.AlunoRepFolder.SQLAlunoRepository>();
builder.Services.AddScoped<API.ProjetoCursosIdioma.Repositories.TurmaRepFolder.ITurmaRepository, API.ProjetoCursosIdioma.Repositories.TurmaRepFolder.SQLTurmaRepository>();

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
