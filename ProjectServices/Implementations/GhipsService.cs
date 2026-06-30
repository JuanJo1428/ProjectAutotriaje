using ProjectData.Entities;
using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos;
using ProjectDto.Dtos.GhipsDtos;
using ProjectServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Implementations
{
    public class GhipsService: IGhipsService
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        private readonly IGeneroRepository _generoRepository;

        public GhipsService(ITipoDocumentoRepository tipoDocumentoRepository, IGeneroRepository generoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;

            _generoRepository = generoRepository;
        }


        public BuscarPacienteGhipsRespuestaDto BuscarPaciente(BuscarPacienteGhipsDto datosBusqueda)
        {
            BuscarPacienteGhipsRespuestaDto respuesta =
            new BuscarPacienteGhipsRespuestaDto();

            // 1. Consumir API GHIPS

            // 2. Verificar si encontró paciente

            // 3. Mapear PacienteGhipsDto -> PacienteDto

            // 4. Llenar respuesta

            return respuesta;
        }


        //Metodos de Mapeo

        private PacienteDto MapearAPacienteDto(BuscarPacienteGhipsDto datosBusqueda, PacienteGhipsDto pacienteGhips)
        {
            TipoDocumento tipoDocumento =  _tipoDocumentoRepository.ObtenerPorCodigo(datosBusqueda.CodigoTipoDocumento);

            Genero genero = _generoRepository.ObtenerPorDescripcion(pacienteGhips.Genero);

            return new PacienteDto()
            {
               
                IdTipoDocumento = tipoDocumento.IdTipoDocumento,
                NroDocumento = datosBusqueda.NroDocumento,

                PrimerNombre = pacienteGhips.PrimerNombre,
                SegundoNombre = pacienteGhips.SegundoNombre,

                PrimerApellido = pacienteGhips.PrimerApellido,
                SegundoApellido = pacienteGhips.SegundoApellido,

                IdGenero = genero.IdGenero,
                FechaNacimiento = pacienteGhips.FechaNacimiento,
                
                Activo = true,

                DescripcionTipoDocumento = tipoDocumento.Descripcion,
                DescripcionGenero = pacienteGhips.Genero,
            };
        }

    }
}
