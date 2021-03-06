﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;

namespace SantaHelena.ClickDoBem.Data.Mappings.Cadastros
{
    public class ItemMatchMapping : IEntityTypeConfiguration<ItemMatch>
    {

        public void Configure(EntityTypeBuilder<ItemMatch> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Data)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.UsuarioId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.NecessidadeId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.DoacaoId)
               .HasColumnType("char(36)")
               .IsRequired();

            builder.Property(c => c.TipoMatchId)
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(c => c.ValorFaixaId)
                .HasColumnType("char(36)")
                .IsRequired();

            builder.Property(c => c.Efetivado)
                .HasColumnType("bit");

            builder.HasOne(o => o.Usuario)
                .WithMany(d => d.Matches)
                .HasForeignKey(f => f.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ItemMatch_Usuario");

            builder.HasOne(o => o.ItemDoacao)
                .WithOne(d => d.MatchDoacao)
                .HasForeignKey<ItemMatch>(f => f.DoacaoId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ItemMatch_Doacao");

            builder.HasOne(o => o.ItemNecessidade)
                .WithOne(d => d.MatchNecessidade)
                .HasForeignKey<ItemMatch>(f => f.NecessidadeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ItemMatch_Necessidade");

            builder.HasOne(o => o.TipoMatch)
                .WithMany(d => d.Matches)
                .HasForeignKey(f => f.TipoMatchId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ItemMatch_TipoMatch");

            builder.HasOne(o => o.ValorFaixa)
                .WithMany(d => d.Matches)
                .HasForeignKey(f => f.ValorFaixaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ItemMatch_ValorFaixa");

            builder.HasIndex(i => new { i.NecessidadeId, i.DoacaoId }).HasName("UK_ItemMatch_ND").IsUnique();
            builder.HasIndex(i => i.Data).HasName("IX_ItemMatch_Data");
            builder.HasIndex(i => i.UsuarioId).HasName("IX_ItemMatch_Usuario");
            builder.HasIndex(i => i.DoacaoId).HasName("IX_ItemMatch_Doacao");
            builder.HasIndex(i => i.NecessidadeId).HasName("IX_ItemMatch_Necessidade");

            builder.Ignore(c => c.ValidationResult);
            builder.Ignore(c => c.CascadeMode);

            builder.ToTable("ItemMatch");

        }

    }
}