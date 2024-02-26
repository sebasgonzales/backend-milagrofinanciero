using Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
builder.Services.AddOptions();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Milagro Financiero", Version = "v1" });
    c.CustomSchemaIds(c => c.FullName); // Utilizamos los nombres completos de los controladores
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Encabezado de autorización JWT utilizando el esquema Bearer. \r\n\r\n
                      Ingresamos la palabra 'Bearer', luego un espacio y el token.
                      \r\n\r\nPor ejemplo: 'Bearer fedERGeWefrt5t45e5g5g4f643333uyhr'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
            });
});
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

//autenticacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

    ;


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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();