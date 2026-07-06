using ProjectData.Entities;
using ProjectData.Repositories.Implementations;
using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos;
using ProjectDto.Dtos.GhipsDtos;
using ProjectServices.Interfaces;
using System;

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
            _pacienteRepository = new PacienteRepository();

            _ghipsService = new GhipsService();

            _tipoDocumentoRepository = new TipoDocumentoRepository();
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


            if (respuesta.EncontradoAutotriaje)
            {
                respuesta.PacientePrincipal = respuesta.PacienteAutotriaje;
            }
            else if (respuesta.EncontradoGhips)
            {
                respuesta.PacientePrincipal = respuesta.PacienteGhips;
            }
            else
            {
                TipoDocumento tipoDocumento =
                    _tipoDocumentoRepository.ObtenerPorId(datosBusqueda.IdTipoDocumento);

                respuesta.PacientePrincipal = new PacienteDto
                {
                    IdTipoDocumento = datosBusqueda.IdTipoDocumento,
                    DescripcionTipoDocumento = tipoDocumento.Descripcion,
                    NroDocumento = datosBusqueda.NroDocumento
                };

                respuesta.Observacion = "Paciente no Encontrado en Bases de Datos.";
            }


            return respuesta;

        }


        public CrearPacienteRespuestaDto CrearPaciente(CrearPacienteDto datosPaciente)
        {
            CrearPacienteRespuestaDto respuesta = new CrearPacienteRespuestaDto();

            Paciente paciente = MapearAPaciente(datosPaciente);

            paciente = _pacienteRepository.CrearPaciente(paciente);

            paciente = _pacienteRepository.ObtenerPorId(paciente.IdPaciente);

            respuesta.Paciente = MapearAPacienteDto(paciente);
            respuesta.Creado = true;

            return respuesta;
        }


        public PacienteProcesadoRespuestaDto ProcesarPaciente(PacienteValidadoDto pacienteValidado)
        {

            PacienteProcesadoRespuestaDto respuesta = new PacienteProcesadoRespuestaDto();

            
            NormalizarPaciente(pacienteValidado);


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
            if (!pacienteEncontrado.IdPaciente.HasValue)
            {
                throw new InvalidOperationException(
                    "El paciente no pertenece a la BaseDatosAutotriaje.");
            }


            Paciente paciente = _pacienteRepository.ObtenerPorId(pacienteEncontrado.IdPaciente.Value);

            if (paciente == null)
            {
                throw new InvalidOperationException(
                    "No se encontró el paciente para actualizar.");
            }

            // Actualiza sus propiedades
            MapearAPaciente(paciente, pacienteValidado);

            // Guarda los cambios
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
                LugarNacimiento = paciente.LugarNacimiento,

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
                FechaActualizacion = null,
                LugarNacimiento = datosPaciente.LugarNacimiento
            };
        }

        private void MapearAPaciente(Paciente paciente, PacienteValidadoDto pacienteValidado)
        {
            paciente.IdTipoDocumento = pacienteValidado.IdTipoDocumento;
            paciente.NroDocumento = pacienteValidado.NroDocumento;

            paciente.PrimerNombre = pacienteValidado.PrimerNombre;
            paciente.SegundoNombre = pacienteValidado.SegundoNombre;

            paciente.PrimerApellido = pacienteValidado.PrimerApellido;
            paciente.SegundoApellido = pacienteValidado.SegundoApellido;

            paciente.IdGenero = pacienteValidado.IdGenero;
            paciente.FechaNacimiento = pacienteValidado.FechaNacimiento;
            paciente.LugarNacimiento = pacienteValidado.LugarNacimiento;

            paciente.Activo = true;
            paciente.FechaActualizacion = DateTime.UtcNow;
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
                FechaNacimiento = paciente.FechaNacimiento,
                LugarNacimiento = paciente.LugarNacimiento
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

            if (pacienteEncontrado.LugarNacimiento != paciente.LugarNacimiento)
                return true;

            return false;
        }

        private int ObtenerCodigoTipoDocumentoGhips(int idTipoDocumento)
        {
            TipoDocumento tipoDocumento =
                _tipoDocumentoRepository.ObtenerPorId(idTipoDocumento);

            return tipoDocumento.Codigo;
        }

        private void NormalizarPaciente(PacienteValidadoDto paciente)
        {
            paciente.PrimerNombre = paciente.PrimerNombre?.Trim().ToUpper();
            paciente.SegundoNombre = paciente.SegundoNombre?.Trim().ToUpper();

            paciente.PrimerApellido = paciente.PrimerApellido?.Trim().ToUpper();
            paciente.SegundoApellido = paciente.SegundoApellido?.Trim().ToUpper();

            paciente.LugarNacimiento = paciente.LugarNacimiento?.Trim().ToUpper();
        }

    }
}
