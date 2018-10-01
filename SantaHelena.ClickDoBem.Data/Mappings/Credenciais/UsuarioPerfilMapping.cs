using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;

namespace SantaHelena.ClickDoBem.Data.Mappings.Credenciais
{
    public class UsuarioPerfilMapping : IEntityTypeConfiguration<UsuarioPerfil>
    {

        public void Configure(EntityTypeBuilder<UsuarioPerfil> builder)
        {

            builder.HasKey(k => new { k.UsuarioId, k.Perfil });

            builder.Property(c => c.UsuarioId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.Perfil)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.HasOne(d => d.Usuario)
                .WithMany(p => p.Perfis)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UP_Usuario");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("UsuarioPerfil");

        }

    }
}
