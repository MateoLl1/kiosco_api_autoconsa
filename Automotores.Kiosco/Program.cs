using Automotores.Kiosco.Data;
using Automotores.Kiosco.Options;
using Automotores.Kiosco.Services;
using Automotores.Kiosco.Services.Interfaces;
using Automotores.Kiosco.Services.Turnero;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// PERMITIR LECTURA DE ARCHIVOS HASTA 500MB

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 524288000;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 524288000;
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});


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
builder.Services.AddScoped<TurnoConCitaService>();
builder.Services.AddScoped<TurnoGeneradorService>();
builder.Services.AddScoped<TurnoLlegadaAutomaticaService>();
builder.Services.AddScoped<TurneroMediaService>();

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