using Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//CORS

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();

                      });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DBContext
builder.Services.AddDbContext<milagrofinancierog1Context>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("BankConnection")));

//Service Layer

builder.Services.AddScoped<Services.ISucursalService, Services.SucursalService>();
builder.Services.AddScoped<Services.ITransaccionService, Services.TransaccionService>();
builder.Services.AddScoped<Services.ICuentaService, Services.CuentaService>();
builder.Services.AddScoped<Services.ITipoTransaccionService, Services.TipoTransaccionService>();
builder.Services.AddScoped<Services.IBancoService, Services.BancoService>();
builder.Services.AddScoped<Services.IEmpleadoService, Services.EmpleadoService>();
builder.Services.AddScoped<Services.IProvinciaService, Services.ProvinciaService>();
builder.Services.AddScoped<Services.ITipoCuentaService,Services.TipoCuentaService>();
builder.Services.AddScoped<Services.ILocalidadService, Services.LocalidadService>();
builder.Services.AddScoped<Services.IClienteCuentaService, Services.ClienteCuentaService>();
builder.Services.AddScoped<Services.IClienteService, Services.ClienteService>();
builder.Services.AddScoped<Services.IPaisService,Services.PaisService>();
builder.Services.AddScoped<Services.ITipoMotivoService, Services.TipoMotivoService>();
builder.Services.AddScoped<Services.IContactoService, Services.ContactoService>();
builder.Services.AddScoped<Services.ILoginService, Services.LoginService>();
builder.Services.AddScoped<Hashing.Hashear>();
builder.Services.AddScoped<GeneradorNumeros.AlgoritmoGenerador>();
builder.Services.AddScoped<Services.IHomeService, Services.HomeService>();



var app = builder.Build();

//Cada vez que se inicie el proyecto se va a ejecutar esto que ejecuta la migraci�n es decir crear la BD o actualizarla
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<MilagrofinancieroG1Context>();
//    context.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();