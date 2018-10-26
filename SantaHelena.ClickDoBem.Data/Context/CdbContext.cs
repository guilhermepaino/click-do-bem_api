using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Mappings.Cadastros;
using SantaHelena.ClickDoBem.Data.Mappings.Credenciais;
using SantaHelena.ClickDoBem.Domain.Entities.Cadastros;
using SantaHelena.ClickDoBem.Domain.Entities.Credenciais;

namespace SantaHelena.ClickDoBem.Data.Context
{

    /// <summary>
    /// Contexto de base de dados
    /// </summary>
    public class CdbContext : DbContext
    {

        #region Construtores

        public CdbContext(DbContextOptions<CdbContext> options) : base(options) { }

        #endregion

        #region Overrideds

        /// <summary>
        /// Configurando modelo
        /// </summary>
        /// <param name="modelBuilder">Builder do modelo</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configurando chaves estrangeiras
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            // Mapping das tabelas
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new UsuarioLoginMapping());
            modelBuilder.ApplyConfiguration(new UsuarioDadosMapping());
            modelBuilder.ApplyConfiguration(new UsuarioPerfilMapping());
            modelBuilder.ApplyConfiguration(new CategoriaMapping());
            modelBuilder.ApplyConfiguration(new DocumentoHabilitadoMapping());
            modelBuilder.ApplyConfiguration(new TipoItemMapping());
            modelBuilder.ApplyConfiguration(new ItemMapping());
            modelBuilder.ApplyConfiguration(new ItemImagemMapping());
            modelBuilder.ApplyConfiguration(new ItemMatchMapping());
            modelBuilder.ApplyConfiguration(new TipoMatchMapping());

        }

        /// <summary>
        /// Salva todas as alterações feitas neste contexto no banco de dados
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataInclusao") != null))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("DataInclusao").CurrentValue = DateTime.Now;
                        entry.Property("DataAlteracao").CurrentValue = null;
                        break;
                    case EntityState.Modified:
                        entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                        entry.Property("DataAlteracao").IsModified = true;
                        break;
                }
            }

            return base.SaveChanges();
        }

        #endregion

        #region DbSets

        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<UsuarioLogin> UsuarioLogin { get; set; }

        public virtual DbSet<UsuarioDados> UsuarioDados { get; set; }

        public virtual DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }

        public virtual DbSet<Categoria> Categoria { get; set; }

        public virtual DbSet<DocumentoHabilitado> DocumentoHabilitado { get; set; }

        public virtual DbSet<TipoItem> TipoItem { get; set; }

        public virtual DbSet<Item> Item { get; set; }

        public virtual DbSet<ItemImagem> ItemImagem { get; set; }

        public virtual DbSet<ItemMatch> ItemMatch { get; set; }

        public virtual DbSet<TipoMatch> TipoMatch { get; set; }

        #endregion

    }

}
