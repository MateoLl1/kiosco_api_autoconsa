using Automotores.Kiosco.Modelos;
using Microsoft.EntityFrameworkCore;

public class ServicioCliente
{
    private readonly DataContext _db;

    public ServicioCliente(DataContext db)
    {
        _db = db;
    }

    public async Task<List<SI_CLIENTE>> ObtenerTodosAsync()
    {
        return await _db.SI_CLIENTE
                        .Take(10)
                        .ToListAsync();
    }
}