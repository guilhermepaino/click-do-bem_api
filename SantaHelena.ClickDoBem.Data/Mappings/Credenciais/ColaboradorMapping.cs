using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;

namespace SantaHelena.ClickDoBem.Data.Mappings.Credenciais
{
    public class ColaboradorMapping : IEntityTypeConfiguration<Colaborador>
    {

        public void Configure(EntityTypeBuilder<Colaborador> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Cpf)
               .HasColumnType("char(11)")
               .IsRequired();

            builder.Property(c => c.Ativo)
               .HasColumnType("bit");

            builder.HasIndex(i => i.DataInclusao).HasName("IX_Colaborador_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_Colaborador_DtAlteracao");
            builder.HasIndex(i => i.Cpf).HasName("UK_Colaborador_Cpf").IsUnique();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Colaborador");

        }

    }
}
