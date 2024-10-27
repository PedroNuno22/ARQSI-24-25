using Microsoft.EntityFrameworkCore;
using PatientManagement.Domain.Interfaces;
using BackOfficeApi.Infrastructure;
using PatientManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure in-memory database for PatientDbContext
builder.Services.AddDbContext<PatientDbContext>(options =>
    options.UseInMemoryDatabase("PatientDatabase"));

// Register the repository with the DI container
builder.Services.AddScoped<IPatientRepository, EfPatientRepository>();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
