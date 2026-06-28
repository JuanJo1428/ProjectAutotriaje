using ProjectServices.Interfaces;
using System;
using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos;
using ProjectData.Entities;

namespace ProjectServices.Implementations
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }


        public BuscarPacienteRespuestaDto BuscarPaciente(BuscarPacienteDto datosBusqueda)
        {
            BuscarPacienteRespuestaDto respuesta = new BuscarPacienteRespuestaDto();

            Paciente paciente = _pacienteRepository.ObtenerPorDocumento(datosBusqueda.IdTipoDocumento, datosBusqueda.NroDocumento);

            if (paciente == null)
            {
                return respuesta;
            }

            respuesta.Paciente = MapearAPacienteDto(paciente);
            respuesta.Encontrado = true;
            respuesta.Origen = "BaseDatosAutotriaje";

            if (!paciente.Activo)
            {
                respuesta.Observacion = "El paciente se encuentra inactivo en la BaseDatosAutotriaje.";
            }

            return respuesta;

        }


        public CrearPacienteRespuestaDto CrearPaciente(CrearPacienteDto datosPaciente)
        {
            CrearPacienteRespuestaDto respuesta = new CrearPacienteRespuestaDto();

            Paciente paciente = MapearAPaciente(datosPaciente);

            paciente = _pacienteRepository.CrearPaciente(paciente);

            respuesta.Paciente = MapearAPacienteDto(paciente);
            respuesta.Creado = true;

            return respuesta;
        }


        public PacienteProcesadoRespuestaDto ProcesarPaciente(PacienteValidadoDto paciente)
        {
            PacienteProcesadoRespuestaDto respuesta = new PacienteProcesadoRespuestaDto();

            //Consulta y validación BaseDatosAutotriaje
            BuscarPacienteRespuestaDto pacienteAutotriaje = BuscarPaciente(new BuscarPacienteDto
            {
                IdTipoDocumento = paciente.IdTipoDocumento,
                NroDocumento = paciente.NroDocumento
            });

            if (!pacienteAutotriaje.Encontrado)
            {

                CrearPacienteRespuestaDto pacienteCreado = CrearPaciente(MapearACrearPacienteDto(paciente));

                respuesta.Paciente = pacienteCreado.Paciente;

                respuesta.AccionRealizada = "Creado";

                return respuesta;
            }


            //Existe en la BaseDatosAutotriaje
            PacienteDto pacienteEncontrado = pacienteAutotriaje.Paciente;

            bool requiereActualizacion = !pacienteEncontrado.Activo || CompararDatosPaciente(pacienteEncontrado, paciente);

            if (requiereActualizacion)
            {
                respuesta.Paciente = ActualizarPaciente(pacienteEncontrado, paciente);

                respuesta.AccionRealizada = "Actualizado";
            }

            else
            {
                respuesta.Paciente = pacienteEncontrado;

                respuesta.AccionRealizada = "SinCambios";

                respuesta.Observacion = "Los datos del paciente coinciden con la BaseDatosAutotriaje";

            }

            return respuesta;
            
        }

        
        public PacienteDto ActualizarPaciente(PacienteDto pacienteEncontrado, PacienteValidadoDto pacienteValidado)
        {
            Paciente paciente = MapearAPaciente(pacienteEncontrado, pacienteValidado);

            paciente = _pacienteRepository.ActualizarPaciente(paciente);

            return MapearAPacienteDto(paciente);
        }


        //Metodos de Mapeo
        private PacienteDto MapearAPacienteDto(Paciente paciente)
        {
            return new PacienteDto
            {
                IdPaciente = paciente.IdPaciente,
                IdTipoDocumento = paciente.IdTipoDocumento,
                NroDocumento = paciente.NroDocumento,
                PrimerNombre = paciente.PrimerNombre,
                SegundoNombre = paciente.SegundoNombre,
                PrimerApellido = paciente.PrimerApellido,
                SegundoApellido = paciente.SegundoApellido,
                IdGenero = paciente.IdGenero,
                FechaNacimiento = paciente.FechaNacimiento,
                FechaCreacion = paciente.FechaCreacion,
                FechaActualizacion = paciente.FechaActualizacion,
                Activo = paciente.Activo
            };
        }

        private Paciente MapearAPaciente(CrearPacienteDto datosPaciente)
        {
            return new Paciente
            {
                IdTipoDocumento = datosPaciente.IdTipoDocumento,
                NroDocumento = datosPaciente.NroDocumento,
                PrimerNombre = datosPaciente.PrimerNombre,
                SegundoNombre = datosPaciente.SegundoNombre,
                PrimerApellido = datosPaciente.PrimerApellido,
                SegundoApellido = datosPaciente.SegundoApellido,
                IdGenero = datosPaciente.IdGenero,
                FechaNacimiento = datosPaciente.FechaNacimiento,
                Activo = true,
                FechaCreacion = DateTime.UtcNow,
                FechaActualizacion = null
            };
        }

        private Paciente MapearAPaciente(PacienteDto pacienteEncontrado, PacienteValidadoDto pacienteValidado)
        {
            return new Paciente
            {
                IdPaciente = pacienteEncontrado.IdPaciente,

                IdTipoDocumento = pacienteValidado.IdTipoDocumento,
                NroDocumento = pacienteValidado.NroDocumento,

                PrimerNombre = pacienteValidado.PrimerNombre,
                SegundoNombre = pacienteValidado.SegundoNombre,
                PrimerApellido = pacienteValidado.PrimerApellido,
                SegundoApellido = pacienteValidado.SegundoApellido,

                IdGenero = pacienteValidado.IdGenero,
                FechaNacimiento = pacienteValidado.FechaNacimiento,

                Activo = true,

                FechaCreacion = pacienteEncontrado.FechaCreacion, 
                FechaActualizacion = DateTime.UtcNow
            };
        }

        private CrearPacienteDto MapearACrearPacienteDto(PacienteValidadoDto paciente)
        {
            return new CrearPacienteDto
            {
                IdTipoDocumento = paciente.IdTipoDocumento,
                NroDocumento = paciente.NroDocumento,
                PrimerNombre = paciente.PrimerNombre,
                SegundoNombre = paciente.SegundoNombre,
                PrimerApellido = paciente.PrimerApellido,
                SegundoApellido = paciente.SegundoApellido,
                IdGenero = paciente.IdGenero,
                FechaNacimiento = paciente.FechaNacimiento
            };
        }

        private bool CompararDatosPaciente(PacienteDto pacienteEncontrado, PacienteValidadoDto paciente)
        {
            if (pacienteEncontrado.IdTipoDocumento != paciente.IdTipoDocumento)
                return true;

            if (pacienteEncontrado.NroDocumento != paciente.NroDocumento)
                return true;

            if (pacienteEncontrado.PrimerNombre != paciente.PrimerNombre)
                return true;

            if (pacienteEncontrado.SegundoNombre != paciente.SegundoNombre)
                return true;

            if (pacienteEncontrado.PrimerApellido != paciente.PrimerApellido)
                return true;

            if (pacienteEncontrado.SegundoApellido != paciente.SegundoApellido)
                return true;

            if (pacienteEncontrado.IdGenero != paciente.IdGenero)
                return true;

            if (pacienteEncontrado.FechaNacimiento != paciente.FechaNacimiento)
                return true;

            return false;
        }



    }
}
