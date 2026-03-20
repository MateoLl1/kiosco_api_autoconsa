using Automotores.Kiosco.Data;
using Automotores.Kiosco.Models;
using Microsoft.EntityFrameworkCore;

namespace Automotores.Kiosco.Services
{
    public class AgenciaService
    {
        private readonly DataContext _context;

        public AgenciaService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SI_AGENCIA>> ObtenerTodasAsync()
        {
            return await _context.SI_AGENCIA
                .AsNoTracking()
                .OrderBy(x => x.AgNombre)
                .ToListAsync();
        }
    }
}