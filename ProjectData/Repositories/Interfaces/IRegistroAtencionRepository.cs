using ProjectData.Entities;
using System.Collections.Generic;


namespace ProjectData.Repositories.Interfaces
{
    public interface IRegistroAtencionRepository
    {
        List<RegistroAtencion> ObtenerTodos(bool incluirInactivos);

        RegistroAtencion CrearRegistro(RegistroAtencion atencion);

        RegistroAtencion ActualizarRegistro(RegistroAtencion registro);

        bool InactivarRegistro(int idAtencion);

        RegistroAtencion ObtenerPorId(int idAtencion);

        List<RegistroAtencion> ObtenerRegistrosPorPaciente(int idPaciente);

    }
}