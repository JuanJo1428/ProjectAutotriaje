using ProjectData.Context;
using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Interfaces;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectData.Repositories.Implementations
{
    public class FlujoPretriajeRepository : IFlujoPretriajeRepository
    {
        private readonly AppDbContext _context;

        public FlujoPretriajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public FlujoPretriaje ObtenerPorId(int idFlujo)
        {
            return _context.FlujosPretriaje.FirstOrDefault(f => f.IdFlujo == idFlujo && f.Activo);
        }

        public FlujoPretriaje ObtenerPorNombre(string nombre)
        {
            return _context.FlujosPretriaje.FirstOrDefault(f => f.Nombre.ToLower() == nombre.ToLower() && f.Activo);
        }

        public List<FlujoPretriaje> ObtenerTodos()
        {
            return _context.FlujosPretriaje
                .Where(f => f.Activo)
                .ToList();
        }
    }
}