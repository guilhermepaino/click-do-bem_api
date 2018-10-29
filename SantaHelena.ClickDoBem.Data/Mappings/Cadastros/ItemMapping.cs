using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {

        public void Configure(EntityTypeBuilder<Item> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataInclusao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Titulo)
               .HasColumnType("varchar(50)")
               .IsRequired();

            builder.Property(c => c.Descricao)
               .HasColumnType("varchar(1000)")
               .IsRequired();

            builder.Property(c => c.TipoItemId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.CategoriaId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.UsuarioId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.Anonimo)
               .HasColumnType("bit");

            builder.Property(c => c.GeradoPorMatch)
                .HasColumnType("bit");

            builder.HasOne(o => o.TipoItem)
                .WithMany(d => d.Itens)
                .HasForeignKey(f => f.TipoItemId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Item_TipoItem");

            builder.HasOne(o => o.Categoria)
                .WithMany(o => o.Itens)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Item_Categoria");

            builder.HasOne(o => o.Usuario)
                .WithMany(o => o.Itens)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Item_Usuario");

            builder.HasIndex(i => i.DataInclusao).HasName("IX_Item_DtInclusao");
            builder.HasIndex(i => i.DataAlteracao).HasName("IX_Item_DtAlteracao");
            builder.HasIndex(i => i.Descricao).HasName("IX_Item_Descricao");
            builder.HasIndex(i => i.TipoItemId).HasName("IX_Item_TipoItemId");
            builder.HasIndex(i => i.CategoriaId).HasName("IX_Item_CategoriaId");
            builder.HasIndex(i => i.UsuarioId).HasName("IX_Item_UsuarioId");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("Item");

        }

    }
}
