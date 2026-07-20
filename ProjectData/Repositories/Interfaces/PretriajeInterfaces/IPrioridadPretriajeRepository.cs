using ProjectData.Entities.PretriajeModel;
using System.Collections.Generic;

namespace ProjectData.Repositories.Interfaces.PretriajeInterfaces
{
    public interface IPrioridadPretriajeRepository
    {
        PrioridadPretriaje ObtenerPorId(int idPrioridad);

        List<PrioridadPretriaje> ObtenerTodas();
    }
}
