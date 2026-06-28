using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectDto;
using ProjectDto.Dtos;


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
