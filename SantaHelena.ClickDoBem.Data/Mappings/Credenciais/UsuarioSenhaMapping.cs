using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SantaHelena.ClickDoBem.Data.Mappings.Credenciais
{
    public class UsuarioSenhaMapping : IEntityTypeConfiguration<UsuarioSenha>
    {

        public void Configure(EntityTypeBuilder<UsuarioSenha> builder)
        {

            builder.Property(c => c.UsuarioId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.Senha)
                    .HasColumnType("char(32)")
                    .IsRequired();

            builder.HasKey(k => k.UsuarioId);

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("UsuarioSenha");

        }

    }
}
