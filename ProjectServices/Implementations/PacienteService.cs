using ProjectData.Context;
using ProjectData.Entities;
using ProjectData.Repositories.Implementations;
using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos;
using ProjectDto.Dtos.GhipsDtos;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectServices.Implementations
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        private readonly IGhipsService _ghipsService;

        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public PacienteService(IPacienteRepository pacienteRepository, IGhipsService ghipsService, ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _pacienteRepository = pacienteRepository;
            _ghipsService = ghipsService;
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }

        public PacienteService()
        {
            var context = new AppDbContext();
            _tipoDocumentoRepository = new TipoDocumentoRepository(context);
        }


        public BuscarPacienteRespuestaDto BuscarPaciente(BuscarPacienteDto datosBusqueda)
        {

            BuscarPacienteRespuestaDto respuesta = new BuscarPacienteRespuestaDto();


            Paciente pacienteAutotriaje = _pacienteRepository.ObtenerPorDocumento(datosBusqueda.IdTipoDocumento, datosBusqueda.NroDocumento);

            if (pacienteAutotriaje != null)
            {
                respuesta.Existe = true;
                respuesta.EncontradoAutotriaje = true;
                respuesta.PacienteAutotriaje = MapearAPacienteDto(pacienteAutotriaje);


                if (!pacienteAutotriaje.Activo)
                {
                    respuesta.Observacion = "Paciente Inactivo en la BaseDatosAutotriaje.";
                }
            }


            BuscarPacienteGhipsDto datosBusquedaGhips = new BuscarPacienteGhipsDto
            {
                NroDocumento = datosBusqueda.NroDocumento,
                CodigoTipoDocumento = ObtenerCodigoTipoDocumentoGhips(datosBusqueda.IdTipoDocumento)
            };

            BuscarPacienteGhipsRespuestaDto respuestaGhips = _ghipsService.BuscarPaciente(datosBusquedaGhips);

            if (respuestaGhips.Encontrado)
            {
                respuesta.Existe = true;
                respuesta.EncontradoGhips = true;
                respuesta.PacienteGhips = respuestaGhips.Paciente;
            }


            if (!respuesta.EncontradoAutotriaje && !respuesta.EncontradoGhips)
            {
                respuesta.Existe = false;
                respuesta.Observacion = "Paciente no Encontrado en Bases de Datos.";
            }


            if (respuesta.EncontradoAutotriaje)
            {
                respuesta.PacientePrincipal = respuesta.PacienteAutotriaje;
            }
            else if (respuesta.EncontradoGhips)
            {
                respuesta.PacientePrincipal = respuesta.PacienteGhips;
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


        public PacienteProcesadoRespuestaDto ProcesarPaciente(PacienteValidadoDto pacienteValidado)
        {

            PacienteProcesadoRespuestaDto respuesta = new PacienteProcesadoRespuestaDto();


            //Consulta del Paciente en las distintas Bases
            BuscarPacienteRespuestaDto consulta = BuscarPaciente(new BuscarPacienteDto
            {
                IdTipoDocumento = pacienteValidado.IdTipoDocumento,
                NroDocumento = pacienteValidado.NroDocumento
            });


            //Procesado Autotriaje
            if (consulta.EncontradoAutotriaje)
            {
                bool requiereActivacion = false;

                //Verifica que el Paciente este Activo
                if (!consulta.PacienteAutotriaje.Activo)
                {
                    requiereActivacion = true;
                    respuesta.ObservacionAutotriaje = "Paciente Reactivado Automaticamente.";
                }


                if (HayInconsistenciasEnDatos(consulta.PacienteAutotriaje, pacienteValidado) || requiereActivacion)
                {
                    respuesta.Paciente = ActualizarPaciente(consulta.PacienteAutotriaje, pacienteValidado);

                    respuesta.AccionRealizada = "Actualizado.";
                }
                else
                {
                    respuesta.Paciente = consulta.PacienteAutotriaje;

                    respuesta.AccionRealizada = "SinCambios.";
                }
            }
            else
            {
                CrearPacienteRespuestaDto pacienteCreado = CrearPaciente(MapearACrearPacienteDto(pacienteValidado));

                respuesta.Paciente = pacienteCreado.Paciente;

                respuesta.AccionRealizada = "Creado.";
            }

            //Procesado Ghips
            if (consulta.EncontradoGhips)
            {

                if (HayInconsistenciasEnDatos(consulta.PacienteGhips, pacienteValidado))
                {
                    respuesta.NotificarGhips = true;
                    respuesta.ObservacionGhips = "Información Inconsistente.";
                }
            }
            else
            {
                respuesta.NotificarGhips = true;
                respuesta.ObservacionGhips = "Paciente Inexistente.";
            }

            return respuesta;

        }

        
        public PacienteDto ActualizarPaciente(PacienteDto pacienteEncontrado, PacienteValidadoDto pacienteValidado)
        {
            Paciente paciente = MapearAPaciente(pacienteEncontrado, pacienteValidado);

            paciente = _pacienteRepository.ActualizarPaciente(paciente);

            return MapearAPacienteDto(paciente);
        }


        //Metodos de Mapeo y Metodos de Apoyo
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
                Activo = paciente.Activo,

                DescripcionTipoDocumento = paciente.TipoDocumento.Descripcion,
                DescripcionGenero = paciente.Genero.Descripcion,
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
            if (!pacienteEncontrado.IdPaciente.HasValue || !pacienteEncontrado.FechaCreacion.HasValue)
            {
                throw new InvalidOperationException(
                    "El paciente no pertenece a la BaseDatosAutotriaje.");
            }

            return new Paciente
            {

                IdPaciente = pacienteEncontrado.IdPaciente.Value,

                IdTipoDocumento = pacienteValidado.IdTipoDocumento,
                NroDocumento = pacienteValidado.NroDocumento,

                PrimerNombre = pacienteValidado.PrimerNombre,
                SegundoNombre = pacienteValidado.SegundoNombre,
                PrimerApellido = pacienteValidado.PrimerApellido,
                SegundoApellido = pacienteValidado.SegundoApellido,

                IdGenero = pacienteValidado.IdGenero,
                FechaNacimiento = pacienteValidado.FechaNacimiento,

                Activo = true,

                FechaCreacion = pacienteEncontrado.FechaCreacion.Value, 
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

        private bool HayInconsistenciasEnDatos(PacienteDto pacienteEncontrado, PacienteValidadoDto paciente)
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

        private int ObtenerCodigoTipoDocumentoGhips(int idTipoDocumento)
        {
            TipoDocumento tipoDocumento =
                _tipoDocumentoRepository.ObtenerPorId(idTipoDocumento);

            return tipoDocumento.Codigo;
        }

        public List<TipoDocumento> ObtenerTodosTiposDocumentos()
        {
            var tiposDocumento = _tipoDocumentoRepository.ObtenerTodos();

            return tiposDocumento;
        }
    }
}
