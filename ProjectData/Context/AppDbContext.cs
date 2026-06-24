using System.Data.Entity;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<RegistroAtencion> Atenciones { get; set; }
    }
}