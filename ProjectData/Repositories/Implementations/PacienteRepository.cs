using Project.Data.Entities;
using ProjectData.Context;
using ProjectData.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace ProjectData.Repositories.Implementations
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Paciente> ObtenerTodos(bool incluirInactivos = true)
        {
            if (incluirInactivos)
            {
                return _context.Pacientes.ToList();
            }

            return _context.Pacientes.Where(p => p.Activo).ToList();
        }

        public Paciente ObtenerPorDocumento(int idTipoDocumento, string nroDocumento)
        {
            return _context.Pacientes.FirstOrDefault(p => p.NroDocumento == nroDocumento && p.IdTipoDocumento == idTipoDocumento);
        }

        public Paciente ObtenerPorId(int idPaciente)
        {
            return _context.Pacientes.FirstOrDefault(p => p.IdPaciente == idPaciente);
        }

        public Paciente CrearPaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);

            _context.SaveChanges();

            return paciente;
        }

        public Paciente ActualizarPaciente(Paciente paciente)
        {
            _context.Entry(paciente).State = EntityState.Modified;

            _context.SaveChanges();

            return paciente;
        }

        public bool InactivarPaciente(int idPaciente) 
        { 
            Paciente paciente = _context.Pacientes.FirstOrDefault(p => p.IdPaciente ==idPaciente);

            if (paciente == null)
            {
                return false;
            }

            paciente.Activo = false;

            ActualizarPaciente(paciente);

            return true;
        }

    }
}
