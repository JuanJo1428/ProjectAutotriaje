using ProjectData.Entities.PretriajeModel;
using System.Collections.Generic;

namespace ProjectData.Repositories.Interfaces.PretriajeInterfaces
{
    public interface IPreguntaPretriajeRepository
    {
        PreguntaPretriaje ObtenerPorId(int idPregunta);

        PreguntaPretriaje ObtenerPrimeraPregunta(int idFlujo);

        List<PreguntaPretriaje> ObtenerPreguntasPorFlujo(int idFlujo);

    }
}
