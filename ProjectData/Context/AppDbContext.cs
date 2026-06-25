using System.Data.Entity;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectData.Entities;

namespace Project.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<RegistroAtencion> RegistrosAtencion { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<TipoDocumento> TiposDocumento { get; set; }
    }
}