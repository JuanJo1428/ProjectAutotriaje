using ProjectData.Context;
using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Repositories.Implementations.PretriajeImplementations
{
    public class PrioridadPretriajeRepository: IPrioridadPretriajeRepository
    {
        private readonly AppDbContext _context;

        public PrioridadPretriajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public PrioridadPretriajeRepository()
        {
            _context = new AppDbContext();
        }


        public PrioridadPretriaje ObtenerPorId(int idPrioridad)
        {
            return _context.PrioridadesPretriaje.FirstOrDefault(p => p.IdPrioridad == idPrioridad && p.Activo);
        }
            
        public List<PrioridadPretriaje> ObtenerTodas()
        {
            return _context.PrioridadesPretriaje
                .Where(p => p.Activo)
                .OrderBy(p => p.NivelPrioridad)
                .ToList();
        }
    }
}
