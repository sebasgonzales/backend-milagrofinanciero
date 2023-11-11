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

// Service Layer
builder.Services.AddScoped<CuentaService>();

var app = builder.Build();

//Cada vez que se inicie el proyecto se va a ejecutar esto que ejecuta la migraci�n es decir crear la BD o actualizarla
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MilagrofinancieroG1Context>();
    context.Database.Migrate();
}

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
