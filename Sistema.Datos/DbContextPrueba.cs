using Microsoft.EntityFrameworkCore;
using Prueba.Entidades.Usuarios;
using Sistema.Datos.Mapping.Usuarios;

namespace Sistema.Datos
{
    public class DbContextPrueba: DbContext
    {        
        public DbSet<Usuario> Usuarios { get; set; }

        public DbContextPrueba(DbContextOptions<DbContextPrueba> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);                        
            modelBuilder.ApplyConfiguration(new UsuarioMap());            
        }

    }
}
