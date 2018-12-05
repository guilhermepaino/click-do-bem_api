using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class CampanhaMapping : IEntityTypeConfiguration<Campanha>
    {

        public void Configure(EntityTypeBuilder<Campanha> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Descricao)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(c => c.DataInicial)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataFinal)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Prioridade)
                .HasColumnType("int(1)")
                .IsRequired();


            builder.HasIndex(i => i.DataInclusao).HasName("IX_Campanha_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_Campanha_DtAlteracao");
            builder.HasIndex(i => i.Descricao).HasName("UK_Campanha_Descricao").IsUnique();
            builder.HasIndex(i => i.DataInicial).HasName("IX_Campanha_DtInicial");
            builder.HasIndex(i => i.DataFinal).HasName("IX_Campanha_DtFinal");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Campanha");

        }

    }
}
