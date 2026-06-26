using ProjectData.Entities;
using System.Collections.Generic;


namespace ProjectData.Repositories.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> ObtenerTodos();

        int ObtenerId(string descripcion);

        string ObtenerDescripcion(int idGenero);

    }
}
