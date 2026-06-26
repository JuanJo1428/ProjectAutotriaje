using System;
using System.Collections.Generic;
using System.Linq;
using ProjectData.Repositories.Interfaces;
using ProjectData.Entities;
using ProjectData.Context;

namespace ProjectData.Repositories.Implementations
{
    public class TipoDocumentoRepository : ITipoDocumentoRepository
    {
        private readonly AppDbContext _context;

        public TipoDocumentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<TipoDocumento> ObtenerTodos()
        {
            return _context.TiposDocumento.Where(td => td.Activo).ToList();
        }

        public int ObtenerId(string descripcion)
        {
            TipoDocumento tipoDocumento = _context.TiposDocumento.FirstOrDefault(td => td.Descripcion == descripcion);

            if (tipoDocumento != null)
            {
                return tipoDocumento.IdTipoDocumento;
            }

            return 0;
        }

        public string ObtenerNombre(int idTipoDocumento)
        {
            TipoDocumento tipoDocumento = ObtenerPorId(idTipoDocumento);

            if (tipoDocumento != null)
            {
                return tipoDocumento.Nombre;
            }

            return null;
        }

        public string ObtenerDescripcion(int idTipoDocumento)
        {
            TipoDocumento tipoDocumento = ObtenerPorId(idTipoDocumento);

            if (tipoDocumento != null)
            {
                return tipoDocumento.Descripcion;
            }

            return null;
        }

        public TipoDocumento ObtenerPorId(int idTipoDocumento)
        {
            return _context.TiposDocumento.FirstOrDefault(td => td.IdTipoDocumento == idTipoDocumento);
        }
    }
}
