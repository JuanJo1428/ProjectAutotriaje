using ProjectData.Entities;
using System.Collections.Generic;


namespace ProjectData.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        List<Paciente> ObtenerTodos(bool incluirInactivos);

        Paciente CrearPaciente(Paciente paciente);

        Paciente ActualizarPaciente(Paciente paciente);

        bool InactivarPaciente(int idPaciente);

        Paciente ObtenerPorId(int idPaciente);

        Paciente ObtenerPorDocumento(int idTipoDocumento, string nroDocumento);

    }
}
