using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ExamenProgra6_Assent_JozefMoraAraya.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//Conexion
var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CNNSTR"));

CnnStrBuilder.Password = "progra6";

string cnnStr = CnnStrBuilder.ConnectionString;

builder.Services.AddDbContext<AnswersDbContext>(options => options.UseSqlServer(cnnStr));

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
