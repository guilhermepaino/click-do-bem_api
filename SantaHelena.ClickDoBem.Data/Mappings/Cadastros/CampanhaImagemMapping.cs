using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class CampanhaImagemMapping : IEntityTypeConfiguration<CampanhaImagem>
    {

        public void Configure(EntityTypeBuilder<CampanhaImagem> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CampanhaId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.Caminho)
               .HasColumnType("varchar(2000)")
               .IsRequired();

            builder.HasOne(o => o.Campanha)
                .WithOne(d => d.Imagem)
                .HasForeignKey<CampanhaImagem>(f => f.CampanhaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_CampanhaImagem_Campanha");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("CampanhaImagem");

        }

    }
}
