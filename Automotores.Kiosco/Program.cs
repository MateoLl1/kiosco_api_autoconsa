using Automotores.Kiosco.Data;
using Automotores.Kiosco.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SiacProduccion")
    )
);

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<AgenciaService>();
builder.Services.AddScoped<TurnoService>();
builder.Services.AddScoped<TurnoGeneradorService>();
builder.Services.AddScoped<TurnoConCitaService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();