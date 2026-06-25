using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        Paciente CrearPaciente(Paciente paciente);

        Paciente ActualizarPaciente(Paciente paciente);

        bool InactivarPaciente(int idPaciente);

        Paciente ObtenerPorId(int idPaciente);

        Paciente ObtenerPorDocumento(int idTipoDocumento, string nroDocumento);

        List<Paciente> ObtenerTodos();

    }
}
