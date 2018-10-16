using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class DocumentoHabilitadoMapping : IEntityTypeConfiguration<DocumentoHabilitado>
    {

        public void Configure(EntityTypeBuilder<DocumentoHabilitado> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.CpfCnpj)
               .HasColumnType("varchar(14)")
               .IsRequired();

            builder.Property(c => c.Ativo)
                .HasColumnType("bit");

            builder.HasIndex(i => i.DataInclusao).HasName("IX_DH_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_DH_DtAlteracao");
            builder.HasIndex(i => i.CpfCnpj).HasName("UK_DH_CpfCnpj").IsUnique();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("DocumentoHabilitado");

        }

    }
}
