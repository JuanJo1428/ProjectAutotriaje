using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ProjectData.Repositories.Implementations;
using System.Text;
using System.Threading.Tasks;
using ProjectData.Repositories.Interfaces;
using ProjectData.Entities;

namespace ProjectData.Repositories.Implementations
{
    internal class GeneroRepository : IGeneroRepository
    {
        private readonly DbContext _context;

        public GeneroRepository(DbContext context)
        {
            _context = context;
        }

        public List<Genero> ObtenerTodos()
        {
            return _context.Set<Genero>().ToList();
        }

        public int ObtenerId(string descripcion)
        {
            var genero = _context.Set<Genero>().FirstOrDefault(g => g.Descripcion == descripcion);
            return genero != null ? genero.IdGenero : 0;
        }

        public string ObtenerDescripcion(int idGenero)
        {
            var genero = _context.Set<Genero>().FirstOrDefault(g => g.IdGenero == idGenero);
            return genero != null ? genero.Descripcion : null;
        }
    }
}
