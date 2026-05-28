using Automotores.Kiosco.Data;
using Automotores.Kiosco.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    options.AddPolicy("KioscoWeb", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<KeycloakTokenService>();
builder.Services.AddScoped<AgenciaService>();
builder.Services.AddScoped<TurnoService>();
builder.Services.AddScoped<TurnoGeneradorService>();
builder.Services.AddScoped<TurnoConCitaService>();
builder.Services.AddScoped<PantallaTurnosService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("KioscoWeb");

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();