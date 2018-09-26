using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;

namespace SantaHelena.ClickDoBem.Data.Mappings.Credenciais
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasIndex(i => i.DataInclusao).HasName("IX_Usuario_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_Usuario_DtAlteracao");
            builder.HasIndex(i => i.Nome).HasName("IX_Usuario_Nome");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Usuario");

        }
    }
}
