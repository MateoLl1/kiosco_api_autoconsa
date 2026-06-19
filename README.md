

# KIOSCO.API
API backend para el sistema de kiosco de autoatención de Automotores Continental.

Este proyecto expone servicios para:

- consulta de clientes
- Integración con SIAC (lectura controlada)
- Futura gestión de turnos, citas y recepción


## 🚀 Tecnologías
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger (Swashbuckle)




1. Configurar cadena de conexión
Editar: <Automotores.Kiosco/appsettings.json>

```
"ConnectionStrings": 
    { 
        "DefaultConnection": "Server=SERVIDOR;Database=BD;User Id=USER; Password=PASS;" 
    }
```


2. ▶️ Ejecutar proyecto
Desde la raíz:
```
dotnet run
```


## 🗄️ Base de Datos (Database First)
Este proyecto utiliza Entity Framework Core en modo Database First contra la base SIAC.

1. 🔄 Generación de entidades (Scaffold)
```
dotnet ef dbcontext scaffold "Server=serv-desarrollo;Database=siac_electro;User Id=mllerena;Password=Auto2525;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c DataContext --context-dir Data --no-onconfiguring -t SI_AGENDA -t SI_CLIENTE -t SI_BAHIA -t SI_AGENCIA -f
```

2. ⚠️ Uso de tablas específicas (ESTA HAY QUE USAR)
```
dotnet ef dbcontext scaffold "Server=serv-desarrollo;Database=siac_electro;User Id=mllerena;Password=Auto2525;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o ModelsTemp -c DataContextTemp --context-dir DataTemp --no-onconfiguring -t SI_AGEND_TECN -t SI_STICKER -t SI_MODELO
```

### 📌 Parámetro
- o Models → carpeta donde se generan las entidades
- c DataContext → nombre del DbContext
- t → tablas específicas a incluir
- f → sobrescribe archivos existentes