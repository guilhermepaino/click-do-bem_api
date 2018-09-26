using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SantaHelena.ClickDoBem.Data.Mappings.Credenciais;
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
            modelBuilder.ApplyConfiguration(new UsuarioSenhaMapping());

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
                        entry.Property("DataAlteracao").IsModified = false;
                        break;
                }
            }

            return base.SaveChanges();
        }

        #endregion

        #region DbSets

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<UsuarioSenha> UsuarioSenha { get; set; }

        #endregion

    }

}
