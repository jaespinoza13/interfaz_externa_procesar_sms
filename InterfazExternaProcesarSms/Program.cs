using Microsoft.OpenApi.Models;
using WsInterfazProcesarSms.Model;
using InterfazExternaProcesarSms.Middleware;

var builder = WebApplication.CreateBuilder(args);
var allowSpecificOrigins = "_AllowSpecifiOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Servicio de Interfaz para Procesar Sms",
        Version = "v1",
        Description = "Interfaz externa para la integración entre Eclipsoft y CoopMEGO"
    });

    s.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Basic",
        In = ParameterLocation.Header,
        Description = "Seguridad básica del API"
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddControllers();
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:BasicAuth"));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:Endpoints"));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:PathLogs"));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:Servicios"));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:Bloqueo"));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:MenuEF"));
builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings:Estados"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        allowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        }
        );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowSpecificOrigins);

app.UseHttpsRedirection();

app.UseMiddleware<BasicAuthentication>();

app.UseAuthorization();

app.MapControllers();

app.Run();
