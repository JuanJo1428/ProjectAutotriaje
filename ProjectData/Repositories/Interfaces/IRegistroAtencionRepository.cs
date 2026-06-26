using Project.Data.Entities;
using System.Collections.Generic;


namespace ProjectData.Repositories.Interfaces
{
    public interface IRegistroAtencionRepository
    {
        List<RegistroAtencion> ObtenerTodos();

        RegistroAtencion CrearRegistro(RegistroAtencion atencion);

        RegistroAtencion ActualizarRegistro(RegistroAtencion registro);

        bool EliminarRegistro(int idRegistro);

        RegistroAtencion ObtenerPorId(int idRegistro);

        List<RegistroAtencion> ObtenerRegistrosPorPaciente(int idPaciente);

    }
}