using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SantaHelena.ClickDoBem.Data.Mappings.Credenciais
{
    public class UsuarioLoginMapping : IEntityTypeConfiguration<UsuarioLogin>
    {

        public void Configure(EntityTypeBuilder<UsuarioLogin> builder)
        {

            builder.HasKey(k => k.UsuarioId);

            builder.Property(c => c.UsuarioId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.Login)
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Senha)
                .HasColumnType("char(32)")
                .IsRequired();

            builder.HasOne(d => d.Usuario)
                    .WithOne(p => p.UsuarioLogin)
                    .HasForeignKey<UsuarioLogin>(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Login_Usuario_Id");

            builder.HasIndex(i => i.Login).HasName("UK_Login_Login").IsUnique();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("UsuarioLogin");

        }

    }
}
