using backend_milagrofinanciero.Data;
using backend_milagrofinanciero.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DBContext
builder.Services.AddDbContext<MilagrofinancieroG1Context>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("BankConnection")));

var app = builder.Build();

// Service Layer
//builder.Services.AddScoped<CuentaService>();
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
