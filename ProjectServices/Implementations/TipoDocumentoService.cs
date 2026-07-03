using ProjectData.Entities;
using ProjectData.Repositories.Interfaces;
using ProjectDto.Dtos;
using ProjectServices.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectServices.Implementations
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepository _tipoDocumentoRepository;

        public TipoDocumentoService(ITipoDocumentoRepository tipoDocumentoRepository)
        {
            _tipoDocumentoRepository = tipoDocumentoRepository;
        }

        public List<TipoDocumentoListaDto> ObtenerTiposDocumento()
        {
            return _tipoDocumentoRepository.ObtenerTodos().Select(td => new TipoDocumentoListaDto
                {
                    IdTipoDocumento = td.IdTipoDocumento,
                    Descripcion = td.Descripcion
                })
                .ToList();
        }

        public TipoDocumento ObtenerPorId(int idTipoDocumento)
        {
            return _tipoDocumentoRepository.ObtenerPorId(idTipoDocumento);
        }

        public TipoDocumento ObtenerPorCodigo(int codigo)
        {
            return _tipoDocumentoRepository.ObtenerPorCodigo(codigo);
        }
    }
}