using ProjectData.Entities;
using ProjectDto.Dtos;
using System.Collections.Generic;


namespace ProjectServices.Interfaces
{
    public interface IPacienteService
    {
        BuscarPacienteRespuestaDto BuscarPaciente(BuscarPacienteDto datosBusqueda);

        CrearPacienteRespuestaDto CrearPaciente(CrearPacienteDto datosPaciente);

        PacienteProcesadoRespuestaDto ProcesarPaciente(PacienteValidadoDto paciente);

        PacienteDto ActualizarPaciente(PacienteDto pacienteEncontrado, PacienteValidadoDto pacienteValidado);
    }
}
