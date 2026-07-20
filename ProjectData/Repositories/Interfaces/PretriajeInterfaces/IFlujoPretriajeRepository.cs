using ProjectData.Entities.PretriajeModel;
using System.Collections.Generic;

namespace ProjectData.Repositories.Interfaces.PretriajeInterfaces
{
    public interface IFlujoPretriajeRepository
    {
        FlujoPretriaje ObtenerPorId(int idFlujo);

        List<FlujoPretriaje> ObtenerTodos();

        FlujoPretriaje ObtenerPorNombre(string nombre);

        FlujoPretriaje ObtenerPorCodigo(string codigo);
    }
}
