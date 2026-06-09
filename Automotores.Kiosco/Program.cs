using Automotores.Kiosco.Data;
using Automotores.Kiosco.Services;
using Automotores.KIOSCO.API.Config;
using Automotores.KIOSCO.API.Services;
using Automotores.KIOSCO.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Minio;

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

builder.Services.Configure<MinioOptions>(
    builder.Configuration.GetSection("Minio")
);
builder.Services.AddScoped<IMinioService, MinioService>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<KeycloakTokenService>();
builder.Services.AddScoped<AgenciaService>();
builder.Services.AddScoped<TurnoService>();
builder.Services.AddScoped<TurnoGeneradorService>();
builder.Services.AddScoped<TurnoConCitaService>();
builder.Services.AddScoped<PantallaTurnosService>();
builder.Services.AddScoped<WhatsappTurnoService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("KioscoWeb");

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();