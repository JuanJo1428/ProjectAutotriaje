using ProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Repositories.Interfaces
{
    public interface ITipoDocumentoRepository
    {
        List<TipoDocumento> ObtenerTodos();

        string ObtenerNombre(int idTipoDocumento);

        string ObtenerDescripcion(int idTipoDocumento);

        TipoDocumento ObtenerPorId(int idTipoDocumento);
    }
}