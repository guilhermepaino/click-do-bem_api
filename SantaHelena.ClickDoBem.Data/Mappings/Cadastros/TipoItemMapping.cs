using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class TipoItemMapping : IEntityTypeConfiguration<TipoItem>
    {

        public void Configure(EntityTypeBuilder<TipoItem> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Descricao)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.HasIndex(i => i.DataInclusao).HasName("IX_TipoItem_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_TipoItem_DtAlteracao");
            builder.HasIndex(i => i.Descricao).HasName("UK_TipoItem_Descricao").IsUnique();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("TipoItem");

        }

    }
}
