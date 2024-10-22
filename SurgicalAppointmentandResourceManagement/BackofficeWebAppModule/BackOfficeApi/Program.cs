var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register IPatientRepository with the DI container and use InMemoryPatientRepository as its implementation
builder.Services.AddSingleton<PatientManagement.Domain.Interfaces.IPatientRepository, PatientManagement.Infrastructure.Repositories.InMemoryPatientRepository>();

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
