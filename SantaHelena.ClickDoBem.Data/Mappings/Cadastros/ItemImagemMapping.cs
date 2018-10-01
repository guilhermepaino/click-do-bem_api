using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class ItemImagemMapping : IEntityTypeConfiguration<ItemImagem>
    {

        public void Configure(EntityTypeBuilder<ItemImagem> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.ItemId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.NomeOriginal)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(c => c.Caminho)
               .HasColumnType("varchar(2000)")
               .IsRequired();

            builder.HasOne(d => d.Item)
                .WithMany(p => p.Imagens)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ItemImagem_Item");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("ItemImagem");

        }

    }
}
