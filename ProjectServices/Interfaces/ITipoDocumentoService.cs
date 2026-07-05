using ProjectData.Entities;
using ProjectDto.Dtos;
using System.Collections.Generic;

namespace ProjectServices.Interfaces
{
    public interface ITipoDocumentoService
    {
        List<TipoDocumentoListaDto> ObtenerTiposDocumento();

        TipoDocumento ObtenerPorId(int idTipoDocumento);

        TipoDocumento ObtenerPorCodigo(int codigo);

        TipoDocumentoListaDto ObtenerTipoDocumento(int idTipoDocumento);
    }
}