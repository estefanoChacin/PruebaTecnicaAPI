using Microsoft.OpenApi.Models;
using PruebaAPI.IOC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.InjectDependency(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configurar Swagger
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

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
