using ProjectData.Context;
using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ProjectData.Repositories.Implementations
{
    public class PreguntaPretriajeRepository : IPreguntaPretriajeRepository
    {
        private readonly AppDbContext _context;

        public PreguntaPretriajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public PreguntaPretriajeRepository()
        {
            _context = new AppDbContext();
        }

        public PreguntaPretriaje ObtenerPorId(int idPregunta)
        {
            return _context.PreguntasPretriaje
                .Include(p => p.Opciones)
                .FirstOrDefault(p => p.IdPregunta == idPregunta && p.Activo);
        }

        public PreguntaPretriaje ObtenerPrimeraPregunta(int idFlujo)
        {
            return _context.PreguntasPretriaje
                .Include(p => p.Opciones)
                .Where(p => p.IdFlujo == idFlujo && p.Activo)
                .FirstOrDefault();
        }

        public List<PreguntaPretriaje> ObtenerPreguntasPorFlujo(int idFlujo)
        {
            return _context.PreguntasPretriaje
                .Include(p => p.Opciones)
                .Where(p => p.IdFlujo == idFlujo && p.Activo)
                .ToList();
        }
    }
}