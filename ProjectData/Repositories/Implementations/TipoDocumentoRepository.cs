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
    internal class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly DbContext _context;

        public TipoDocumentoRepository(DbContext context)
        {
            _context = context;
        }

        public List<TipoDocumento> ObtenerTodos()
        {
            return _context.Set<TipoDocumento>().ToList();
        }

        public int ObtenerId(string descripcion)
        {
            var tipoDocumento = _context.Set<TipoDocumento>().FirstOrDefault(td => td.Descripcion == descripcion);
            return tipoDocumento != null ? tipoDocumento.IdTipoDocumento : 0;
        }

        public string ObtenerNombre(int idTipoDocumento)
        {
            var tipoDocumento = _context.Set<TipoDocumento>().FirstOrDefault(td => td.IdTipoDocumento == idTipoDocumento);
            return tipoDocumento != null ? tipoDocumento.Nombre : null;
        }

        public string ObtenerDescripcion(int idTipoDocumento)
        {
            var tipoDocumento = _context.Set<TipoDocumento>().FirstOrDefault(td => td.IdTipoDocumento == idTipoDocumento);
            return tipoDocumento != null ? tipoDocumento.Descripcion : null;
        }

        public TipoDocumento ObtenerPorId(int idTipoDocumento)
        {
            return _context.Set<TipoDocumento>().FirstOrDefault(td => td.IdTipoDocumento == idTipoDocumento);
        }
    }
}
