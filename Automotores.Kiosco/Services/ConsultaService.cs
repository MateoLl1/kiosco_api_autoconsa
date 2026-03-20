
using Automotores.Kiosco.Models;
using Microsoft.EntityFrameworkCore;

public class ClienteService
{
    private readonly DataContext _db;

    public ClienteService(DataContext db)
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