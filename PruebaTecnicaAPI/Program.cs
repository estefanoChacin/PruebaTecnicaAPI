using Microsoft.OpenApi.Models;
using PruebaAPI.IOC;
using NLog.Web;
using PruebaAPI.Middlewares;
using PruebaAPI.Filters;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InjectDependency(builder.Configuration);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
builder.Logging.AddNLog("NLog.config");

//Desactivar validacion automatica de los modelos
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//registrar el filtro de validacion y personalizacion de la respuesta.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidaterFilter>();
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
// Configurar Swagger para que reconozca que se usa autenticacion por JWT
builder.Services.AddSwaggerGen(c =>
{
    // Definir el esquema de seguridad JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,  
        Scheme = "Bearer",  
        BearerFormat = "JWT", 
        Description = "JWT Ingrese el token devuelto por el endporint de authenticate para poder realizar solicitudes a los otros endpoints"
    });

    // Aplicar el esquema de seguridad globalmente
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Tenica API v1");
    });
}

//registrar middleware en la configuracion
app.UseMiddleware<LogMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
