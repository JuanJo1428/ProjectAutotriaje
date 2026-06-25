using ProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Repositories.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> ObtenerTodos();

        int ObtenerId(string descripcion);

        string ObtenerDescripcion(int idGenero);

    }
}
