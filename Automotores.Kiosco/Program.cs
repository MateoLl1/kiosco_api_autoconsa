using Automotores.Kiosco.Modelos;
using Automotores.Kiosco.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ServicioCliente>();
builder.Services.AddHttpClient<DatabookService>((serviceProvider, client) =>
{
    var config = serviceProvider
        .GetRequiredService<IConfiguration>();

    var baseUrl = config["Apis:Databook:BaseUrl"];
    var timeout = int.Parse(config["Apis:Databook:TimeoutSeconds"] ?? "10");

    client.BaseAddress = new Uri(baseUrl!);
    client.Timeout = TimeSpan.FromSeconds(timeout);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();