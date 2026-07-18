using ProjectData.Entities;
using ProjectData.Repositories.Implementations;
using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos;
using ProjectDto.Dtos.RegistroAtencionDtos;
using ProjectCommon.Constants;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectServices.Implementations
{
    public class RegistroAtencionService : IRegistroAtencionService
    {
        private readonly IRegistroAtencionRepository _registroAtencionRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public RegistroAtencionService(IRegistroAtencionRepository registroAtencionRepository, IPacienteRepository pacienteRepository)
        {
            _registroAtencionRepository = registroAtencionRepository;
            _pacienteRepository = pacienteRepository;
        }

        public RegistroAtencionService()
        {
            _registroAtencionRepository = new RegistroAtencionRepository();

            _pacienteRepository = new PacienteRepository();
        }


        public CrearRegistroAtencionRespuestaDto CrearRegistroAtencion(CrearRegistroAtencionDto datosRegistro)
        {
            CrearRegistroAtencionRespuestaDto respuesta = new CrearRegistroAtencionRespuestaDto();

            Paciente paciente = _pacienteRepository.ObtenerPorId(datosRegistro.IdPaciente);

            if (paciente == null)
            {
                respuesta.Observacion = "El paciente no existe.";

                return respuesta;
            }

            //Calcula Edad y Verifica Priorización Etaria
            int edadPaciente = CalcularEdad(paciente.FechaNacimiento);

            bool priorizacionEtaria = PriorizacionPorGrupoEtario(edadPaciente);


            //Verifica Condición de Maternidad
            ValidarCondicionMaternidad(paciente, datosRegistro);


            bool pacientePriorizado = priorizacionEtaria || datosRegistro.CondicionMaternidad || datosRegistro.CondicionMental || datosRegistro.CondicionOncologica;

            respuesta.Priorizado = pacientePriorizado;


            if (pacientePriorizado)
            {
                respuesta.Observacion = "Paciente priorizado por:";

                if (priorizacionEtaria)
                {
                    respuesta.Observacion += "\n- Grupo etario";
                }

                if (datosRegistro.CondicionMaternidad)
                {
                    respuesta.Observacion += "\n- Condición de maternidad";
                }

                if (datosRegistro.CondicionMental)
                {
                    respuesta.Observacion += "\n- Condición mental";
                }

                if (datosRegistro.CondicionOncologica)
                {
                    respuesta.Observacion += "\n- Condición oncológica";
                }
            }


            RegistroAtencion atencionCreada = MapearARegistroAtencion(datosRegistro, edadPaciente);

            atencionCreada = _registroAtencionRepository.CrearRegistro(atencionCreada);


            respuesta.RegistroAtencion = MapearARegistroAtencionDto(atencionCreada);

            return respuesta;
        }


        public ConsultarRegistroPendienteRespuestaDto ConsultarRegistroPendiente(PacienteProcesadoRespuestaDto pacienteProcesado)
        {

            ConsultarRegistroPendienteRespuestaDto respuesta = new ConsultarRegistroPendienteRespuestaDto();


            if (!pacienteProcesado.Paciente.IdPaciente.HasValue)
            {
                throw new InvalidOperationException(
                    "El paciente procesado no tiene un Id asignado.");
            }


            RegistroAtencion registroPendiente = _registroAtencionRepository.ObtenerRegistroPendiente(pacienteProcesado.Paciente.IdPaciente.Value);

            if (registroPendiente == null)
            {
                return respuesta;
            }


            respuesta.RegistroAtencion = MapearARegistroAtencionDto(registroPendiente);

            respuesta.TieneRegistroPendiente = true;

            respuesta.MostrarOpcionContinuarAutotriaje = !registroPendiente.AutotriajeIniciado;

            return respuesta;
        }


        public RegistroAtencionDto ActualizarRegistro(ActualizarRegistroAtencionDto datosRegistro)
        {

            RegistroAtencion registro = _registroAtencionRepository.ObtenerPorId(datosRegistro.IdAtencion);

            if (registro == null)
            {
                return null;
            }


            registro.MotivoConsulta = datosRegistro.MotivoConsulta;

            registro.AutotriajeIniciado = datosRegistro.AutotriajeIniciado;

            registro.Atendido = datosRegistro.Atendido;

            registro.FechaActualizacion = DateTime.Now;

            registro = _registroAtencionRepository.ActualizarRegistro(registro);


            return MapearARegistroAtencionDto(registro);
        }


        public List<RegistroAtencion> ObtenerPacientesSalaEspera()
        {
            return _registroAtencionRepository.ObtenerPacientesSalaEspera();
        }


        //Metodos de Mapeo Y Apoyo
        private RegistroAtencion MapearARegistroAtencion(CrearRegistroAtencionDto datosRegistro, int edad)
        {
            return new RegistroAtencion
            {
                IdPaciente = datosRegistro.IdPaciente,
                FechaRegistro = DateTime.UtcNow,
                EdadPaciente = edad,
                CondicionMaternidad = datosRegistro.CondicionMaternidad,
                CondicionMental = datosRegistro.CondicionMental,
                CondicionOncologica = datosRegistro.CondicionOncologica,
                AutotriajeIniciado = datosRegistro.AutotriajeIniciado,
                Atendido = false,
                Activo = true,
                FechaActualizacion = null
            };
        }

        private RegistroAtencionDto MapearARegistroAtencionDto(RegistroAtencion registro)
        {
            return new RegistroAtencionDto
            {
                IdAtencion = registro.IdAtencion,
                IdPaciente = registro.IdPaciente,
                FechaRegistro = registro.FechaRegistro,
                EdadPaciente = registro.EdadPaciente,
                CondicionMaternidad = registro.CondicionMaternidad,
                CondicionMental = registro.CondicionMental,
                CondicionOncologica = registro.CondicionOncologica,
                AutotriajeIniciado = registro.AutotriajeIniciado,
                MotivoConsulta = registro.MotivoConsulta,
                Atendido = registro.Atendido,
                Activo = registro.Activo
            };
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.UtcNow;

            int edad = fechaActual.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > fechaActual.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }

        private bool PriorizacionPorGrupoEtario(int edad)
        {
            return edad <= 5 || edad >= 65;
        }

        private void ValidarCondicionMaternidad(Paciente paciente, CrearRegistroAtencionDto datosRegistro)
        {
            // Un paciente de género masculino no puede presentar condición de maternidad.
            if (paciente.IdGenero == (int)Generos.Masculino)
            {
                datosRegistro.CondicionMaternidad = false;
            }
        }
    }
}
