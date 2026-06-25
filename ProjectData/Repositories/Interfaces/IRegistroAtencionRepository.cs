using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Repositories.Interfaces
{
    public interface IRegistroAtencionRepository
    {
        RegistroAtencion CrearRegistro(RegistroAtencion atencion);

        RegistroAtencion ActualizarRegistro(RegistroAtencion registro);

        bool EliminarRegistro(int idRegistro);

        RegistroAtencion ObtenerPorId(int idRegistro);

        List<RegistroAtencion> ObtenerRegistrosPorPaciente(int idPaciente);

        List<RegistroAtencion> ObtenerTodos();

    }
}