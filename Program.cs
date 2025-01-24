using Microsoft.EntityFrameworkCore;
using NetflixAPISolution.Models;
using NetflixAPISolution.Repository;

var builder = WebApplication.CreateBuilder(args);


//Code to load the repository class into controller class.
builder.Services.AddSingleton<NetflixRepository, NetflixRepository>();

var filePath = @"C:\Users\zwane\Downloads\netflix.xlsx";
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

//Push this to github...
