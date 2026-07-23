using ProjectData.Entities.PretriajeModel;

namespace ProjectData.Repositories.Interfaces.PretriajeInterfaces
{
    public interface IDecisionPretriajeRepository
    {
        DecisionPretriaje ObtenerDecision(int idPregunta, int idOpcionSeleccionada);
    }
}
