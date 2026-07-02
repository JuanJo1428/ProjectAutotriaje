using System.Collections.Generic;
using System.Linq;
using ProjectData.Repositories.Interfaces;
using ProjectData.Entities;
using ProjectData.Context;

namespace ProjectData.Repositories.Implementations
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AppDbContext _context;

        public GeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Genero> ObtenerTodos()
        {
            return _context.TiposGenero.Where(g => g.Activo).ToList();
        }

        public int ObtenerId(string descripcion)
        {
            Genero genero = _context.TiposGenero.FirstOrDefault(g => g.Descripcion == descripcion && g.Activo);

            if (genero != null)
            {
                return genero.IdGenero;
            }

            return 0; 
        }

        public string ObtenerDescripcion(int idGenero)
        {
            Genero genero = _context.TiposGenero.FirstOrDefault(g => g.IdGenero == idGenero && g.Activo);
            
            if (genero != null)
            {
                return genero.Descripcion;
            }

            return null;
        }

        public Genero ObtenerPorDescripcion(string descripcion)
        {
            return _context.Generos
                .FirstOrDefault(g => g.Descripcion.ToLower() == descripcion.ToLower());
        }
    }
}
