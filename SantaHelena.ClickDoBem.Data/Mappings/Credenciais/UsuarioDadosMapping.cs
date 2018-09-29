using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;

namespace SantaHelena.ClickDoBem.Data.Mappings.Credenciais
{
    public class UsuarioDadosMapping : IEntityTypeConfiguration<UsuarioDados>
    {

        public void Configure(EntityTypeBuilder<UsuarioDados> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("char(36)")
                .IsRequired();

            builder.Property(c => c.DataNascimento)
                .HasColumnType("date");

            builder.Property(c => c.Logradouro)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Numero)
                .HasColumnType("varchar(30)");

            builder.Property(c => c.Complemento)
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Bairro)
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Cidade)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.UF)
                .HasColumnType("char(2)");

            builder.Property(c => c.CEP)
                .HasColumnType("char(8)");

            builder.Property(c => c.TelefoneCelular)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.TelefoneFixo)
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Email)
                .HasColumnType("varchar(1000)");

            builder.HasOne(d => d.Usuario)
                    .WithOne(p => p.UsuarioDados)
                    .HasForeignKey<UsuarioDados>(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UsuarioDados_Usuario_Id");

            builder.HasIndex(i => i.DataInclusao).HasName("IX_Usuario_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_Usuario_DtAlteracao");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("UsuarioDados");

        }

    }
}
