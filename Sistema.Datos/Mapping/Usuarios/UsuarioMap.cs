using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prueba.Entidades.Usuarios;


namespace Sistema.Datos.Mapping.Usuarios
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios")
               .HasKey(u => u.id);            
        }
    }
}
