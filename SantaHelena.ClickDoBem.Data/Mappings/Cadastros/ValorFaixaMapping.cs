using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class ValorFaixaMapping : IEntityTypeConfiguration<ValorFaixa>
    {

        public void Configure(EntityTypeBuilder<ValorFaixa> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(c => c.ValorInicial)
                .HasColumnType("decimal(16,2)")
                .IsRequired();

            builder.Property(c => c.ValorFinal)
                .HasColumnType("decimal(16,2)")
                .IsRequired();

            builder.Property(c => c.Inativo)
                .HasColumnType("bit");

            builder.HasIndex(i => i.Descricao).HasName("UK_ValorFaixa_Descricao");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("ValorFaixa");

        }

    }
}
