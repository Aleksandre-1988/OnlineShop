using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OnlineShop.DAL;
using OnlineShop.Domain.Model;

var builder = WebApplication.CreateBuilder(args);

//SQL Server Connection
builder.Services.AddDbContext<MainContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

//Add DAL Repository
builder.Services.AddRepository();

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

    app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7299", "http://localhost:7299")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .WithHeaders(HeaderNames.ContentType)
    );
}

app.UseCors(policy => 
    policy.WithOrigins("https://localhost:7299","http://localhost:7299/")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithHeaders(HeaderNames.ContentType)
    );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
