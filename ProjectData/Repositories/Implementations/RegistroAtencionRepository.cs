using ProjectData.Entities;
using ProjectData.Context;
using ProjectData.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace ProjectData.Repositories.Implementations
{
    public class RegistroAtencionRepository: IRegistroAtencionRepository
    {
        private readonly AppDbContext _context;
        
        public RegistroAtencionRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<RegistroAtencion> ObtenerTodos(bool incluirInactivos = true)
        {
            if (incluirInactivos)
            {
                return _context.RegistrosAtencion.ToList();
            }

            return _context.RegistrosAtencion.Where(p => p.Activo).ToList();
        }

        public RegistroAtencion ObtenerPorId(int idAtencion)
        {
            return _context.RegistrosAtencion.FirstOrDefault(ra => ra.IdAtencion == idAtencion);
        }

        public RegistroAtencion ObtenerRegistroPendiente(int idAtencion)
        {
            return _context.RegistrosAtencion.FirstOrDefault(ra => ra.IdAtencion == idAtencion && !ra.Atendido);
        }

        public List<RegistroAtencion> ObtenerRegistrosPorPaciente(int idPaciente)
        {
            return _context.RegistrosAtencion.Where(ra => ra.IdPaciente == idPaciente).ToList();
        }

        public RegistroAtencion CrearRegistro(RegistroAtencion atencion)
        {
            _context.RegistrosAtencion.Add(atencion);

            _context.SaveChanges();

            return atencion;
        }

        public RegistroAtencion ActualizarRegistro(RegistroAtencion atencion)
        {
            _context.Entry(atencion).State = EntityState.Modified;

            _context.SaveChanges();

            return atencion;
        }

        public bool InactivarRegistro(int idAtencion)
        {
            RegistroAtencion atencion = ObtenerPorId(idAtencion);

            if (atencion == null)
            {
                return false;
            }

            atencion.Activo = false;

            ActualizarRegistro(atencion);

            return true;
        }
    }
}
