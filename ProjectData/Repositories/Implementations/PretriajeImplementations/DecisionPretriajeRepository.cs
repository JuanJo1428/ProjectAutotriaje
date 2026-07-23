using ProjectData.Context;
using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using System.Linq;

namespace ProjectData.Repositories.Implementations
{
    public class DecisionPretriajeRepository : IDecisionPretriajeRepository
    {
        private readonly AppDbContext _context;

        public DecisionPretriajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public DecisionPretriajeRepository()
        {
            _context = new AppDbContext();
        }


        public DecisionPretriaje ObtenerDecision(int idPregunta, int idOpcionSeleccionada)
        {
            return _context.DecisionesPretriaje
                .FirstOrDefault(d =>
                    d.IdPregunta == idPregunta &&
                    d.IdOpcionEsperada == idOpcionSeleccionada &&
                    d.EsActivo);
        }
    }
}