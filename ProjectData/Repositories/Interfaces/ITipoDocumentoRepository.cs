using ProjectData.Entities;
using System.Collections.Generic;


namespace ProjectData.Repositories.Interfaces
{
    public interface ITipoDocumentoRepository
    {
        List<TipoDocumento> ObtenerTodos();

        int ObtenerId(string descripcion);

        string ObtenerNombre(int idTipoDocumento);

        string ObtenerDescripcion(int idTipoDocumento);

        TipoDocumento ObtenerPorId(int idTipoDocumento);

        TipoDocumento ObtenerPorCodigo(int codigo);
    }
}



