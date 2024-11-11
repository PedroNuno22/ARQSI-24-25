using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Domain.Services;
using Domain.Repositories;
using Application.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)) 
    ));
    
// Register repository and service interfaces
builder.Services.AddScoped<IOperationRequestRepository, OperationRequestRepository>();
builder.Services.AddScoped<IOperationRequestService, OperationRequestService>();

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