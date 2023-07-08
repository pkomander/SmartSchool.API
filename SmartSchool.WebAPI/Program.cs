using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Services.Interface;
using SmartSchool.WebAPI.Services.Repository;

var builder = WebApplication.CreateBuilder(args);

//adicionando banco de dados SQLITE
builder.Services.AddDbContext<DataContext>(
    context => context.UseSqlite(builder.Configuration.GetConnectionString("Default"))
);

//injetando dependencias
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IAluno, AlunoRepository>();

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

app.MapControllers();

app.Run();
