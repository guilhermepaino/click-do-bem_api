using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {

        public void Configure(EntityTypeBuilder<Categoria> builder)
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

            builder.Property(c => c.Pontuacao)
               .HasColumnType("int(5)")
               .IsRequired();

            builder.Property(c => c.GerenciadaRh)
                .HasColumnType("bit");

            builder.HasIndex(i => i.DataInclusao).HasName("IX_Categoria_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_Categoria_DtAlteracao");
            builder.HasIndex(i => i.Descricao).HasName("UK_Categoria_Descricao").IsUnique();

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Categoria");

        }

    }
}
