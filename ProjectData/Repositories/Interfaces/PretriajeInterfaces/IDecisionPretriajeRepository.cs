using ProjectData.Entities.PretriajeModel;

namespace ProjectData.Repositories.Interfaces.PretriajeInterfaces
{
    public interface IDecisionPretriajeRepository
    {
        DecisionPretriaje ObtenerDecisionSiNo(int idPregunta, bool respuesta);

        DecisionPretriaje ObtenerDecisionLista(int idPregunta, int idOpcionSeleccionada);
    }
}
