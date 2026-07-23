using ProjectData.Context;
using ProjectData.Entities.PretriajeModel;
using ProjectData.Repositories.Interfaces.PretriajeInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ProjectData.Repositories.Implementations.PretriajeImplementations
{
    public class RespuestaPreguntaPretriajeRepository : IRespuestaPreguntaPretriajeRepository
    {
        private readonly AppDbContext _context;

        public RespuestaPreguntaPretriajeRepository(AppDbContext context)
        {
            _context = context;
        }

        public RespuestaPreguntaPretriajeRepository()
        {
            _context = new AppDbContext();
        }

        public RespuestaPreguntaPretriaje CrearRespuesta(RespuestaPreguntaPretriaje respuesta)
        {
            _context.RespuestasPreguntasPretriaje.Add(respuesta);

            _context.SaveChanges();

            return respuesta;
        }

        public List<RespuestaPreguntaPretriaje> ObtenerPorRegistro(int idRegistro)
        {
            return _context.RespuestasPreguntasPretriaje
                .Include(r => r.Pregunta)
                .Include(r => r.OpcionSeleccionada)
                .Where(r => r.IdRegistro == idRegistro && r.Activo)
                .ToList();
        }

        public bool InactivarRespuesta(int idRespuesta)
        {
            RespuestaPreguntaPretriaje respuesta = _context.RespuestasPreguntasPretriaje.FirstOrDefault(r => r.IdRespuesta == idRespuesta);

            if (respuesta == null)
                return false;

            respuesta.Activo = false;

            _context.SaveChanges();

            return true;
        }
    }
}